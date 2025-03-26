using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage_3 : MonoBehaviour
{
    [SerializeField] float maxHP;
    float Hp;
    [SerializeField] GameObject DeathEffect;
    [SerializeField] Image HpUI;
    [SerializeField] Slider BossHP;
    [SerializeField] List<GameObject> Items = new List<GameObject>();
    [SerializeField] int ItemDropRate = 30;
    public delegate void DeathDelegate(); // ��� �̺�Ʈ ��������Ʈ ����
    public event DeathDelegate OnDeath; // ��� �̺�Ʈ

    public void GetDamage(int dmg) { Hp -= (float)dmg; }
    public void GetDamage(float dmg) { Hp -= dmg; }

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
            if (transform.name == "Boss")
            {
                ScenManager.instance.GoNextStage();
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

        if (transform.name == "Boss" && BossHP != null) BossHP.value = Hp / maxHP; //�����̴� �־�� ����
        else if (HpUI != null) HpUI.fillAmount = Hp / maxHP; //ü�¹� �־�� ����
    }

    private void CreateItem()
    {
        int rand = Random.Range(0, Items.Count);
        Instantiate(Items[rand], transform.position, Quaternion.identity);
    }
}
