using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage_3 : MonoBehaviour
{
    [Header("Status&Effect")]
    [Tooltip("�� ��ũ��Ʈ�� �� �ִ� ������Ʈ�� �ִ� ü��")]
    [SerializeField] float maxHP;
    float Hp;
    [Tooltip("�� ��ũ��Ʈ�� �� �ִ� ������Ʈ�� �׾����� ������ ����Ʈ")]
    [SerializeField] GameObject DeathEffect;


    [Header("HpUI")]
    [Tooltip("�⺻ ���� UI, Image�� mode�� fill�� ����Ͽ� ��������ϴ�./ ä������ ������ �̹����� ��带 fill�� �ٲ� �� �־��ּ���")]
    [SerializeField] Image HpUI;
    [Tooltip("������ UI, �⺻ UI�� �����̴��� �̿��Ͽ� ��������ϴ�./ �����̴��� ����� �����̴��� �־��ּ���")]
    [SerializeField] Slider BossHP;

    [Header("Item")]
    [Tooltip("���� �׾��� �� ������ ������ ����Դϴ�, ��� ���� Ȯ���� �Ѱ��� ���ɴϴ�.")]
    [SerializeField] List<GameObject> Items = new List<GameObject>();
    [Tooltip("���� �׾��� �� �������� ������ Ȯ���Դϴ�.")]
    [SerializeField] int ItemDropRate = 30;

    [Header("About Next Scene")]
    [Tooltip("���� ������ �Ѿ���� �������� �̹��� UI�Դϴ�.")]
    [SerializeField] GameObject NextSceneUI;
    [Tooltip("���� ������ �Ѿ���� �������� �̹����� ����Ʈ �ؽ�Ʈ �����Դϴ�.")]
    [SerializeField] string perfect;

    public delegate void DeathDelegate(); // ��� �̺�Ʈ ��������Ʈ ����
    public event DeathDelegate OnDeath; // ��� �̺�Ʈ

    public void GetDamage(int dmg) { Hp -= (float)dmg; }
    public void GetDamage(float dmg) { Hp -= dmg; }

    public float GetHp() { return Hp; }

    bool death = false;

    private void Start()
    {
        Hp = maxHP;
    }

    private void Update()
    {
        if (Hp <= 0 && !death)
        {
            death = true;
            Debug.Log(transform.name);
            if (transform.CompareTag("Boss"))
            {
                //���⿡ �̹��� �ִϸ��̼� �߰�
                //ScenManager.instance.GoNextStage();
                NextStageAnime();
            }
            else
            {
                if (DeathEffect != null) Instantiate(DeathEffect, transform.position, Quaternion.identity); // ���� ȿ�� �������� ����

                if (Items.Count > 1) //������ ����Ʈ�� ������ �־�� ����
                {
                    int rate = Random.Range(0, 100);
                    if (rate <= ItemDropRate) CreateItem();
                }

                // OnDeath �̺�Ʈ ȣ��
                if (OnDeath != null) OnDeath.Invoke();

                Destroy(gameObject);
            }
        }

        if (transform.CompareTag("Boss") && BossHP != null) BossHP.value = Hp / maxHP; //�����̴� �־�� ����
        else if (HpUI != null) HpUI.fillAmount = Hp / maxHP; //ü�¹� �־�� ����
    }

    private void CreateItem()
    {
        //������ ������ ����X
        if (transform.name.Contains("Spawner"))
            return;

        int rand = Random.Range(0, Items.Count);
        Instantiate(Items[rand], transform.position, Quaternion.identity);
    }

    public void NextStageAnime()
    {
        GameManager.Instance.nowNextStage = true;
        GameObject ui = Instantiate(NextSceneUI);
        ui.GetComponent<NextSceneAnimation>().StartAnime();
        Destroy(gameObject);
    }
}
