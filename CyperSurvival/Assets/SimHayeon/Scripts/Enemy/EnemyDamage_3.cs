using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage_3 : MonoBehaviour
{
    [Header("Status&Effect")]
    [Tooltip("이 스크립트가 들어가 있는 오브젝트의 최대 체력")]
    [SerializeField] float maxHP;
    float Hp;
    [Tooltip("이 스크립트가 들어가 있는 오브젝트가 죽었을때 나오는 이펙트")]
    [SerializeField] GameObject DeathEffect;


    [Header("HpUI")]
    [Tooltip("기본 적의 UI, Image의 mode중 fill을 사용하여 만들었습니다./ 채워지는 색깔의 이미지의 모드를 fill로 바꾼 후 넣어주세요")]
    [SerializeField] Image HpUI;
    [Tooltip("보스의 UI, 기본 UI중 슬라이더를 이용하여 만들었습니다./ 슬라이더를 만들고 슬라이더를 넣어주세요")]
    [SerializeField] Slider BossHP;

    [Header("Item")]
    [Tooltip("적이 죽었을 때 떨구는 아이템 목록입니다, 모두 같은 확률로 한개만 나옵니다.")]
    [SerializeField] List<GameObject> Items = new List<GameObject>();
    [Tooltip("적이 죽었을 때 아이템을 떨구는 확률입니다.")]
    [SerializeField] int ItemDropRate = 30;

    [Header("About Next Scene")]
    [Tooltip("다음 씬으로 넘어가기전 보여지는 이미지 UI입니다.")]
    [SerializeField] GameObject NextSceneUI;
    [Tooltip("다음 씬으로 넘어가기전 보여지는 이미지의 이펙트 텍스트 내용입니다.")]
    [SerializeField] string perfect;

    public delegate void DeathDelegate(); // 사망 이벤트 델리게이트 선언
    public event DeathDelegate OnDeath; // 사망 이벤트

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
                //여기에 이미지 애니메이션 추가
                //ScenManager.instance.GoNextStage();
                NextStageAnime();
            }
            else
            {
                if (DeathEffect != null) Instantiate(DeathEffect, transform.position, Quaternion.identity); // 죽은 효과 있을때만 실행

                if (Items.Count > 1) //아이템 리스트에 아이템 있어야 실행
                {
                    int rate = Random.Range(0, 100);
                    if (rate <= ItemDropRate) CreateItem();
                }

                // OnDeath 이벤트 호출
                if (OnDeath != null) OnDeath.Invoke();

                Destroy(gameObject);
            }
        }

        if (transform.CompareTag("Boss") && BossHP != null) BossHP.value = Hp / maxHP; //슬라이더 있어야 적용
        else if (HpUI != null) HpUI.fillAmount = Hp / maxHP; //체력바 있어야 적용
    }

    private void CreateItem()
    {
        //스포너 아이템 생성X
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
