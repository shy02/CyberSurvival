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
    public void GetDamage(int dmg) { Hp -= (float)dmg; }
    public void GetDamage(float dmg) { Hp -= dmg; }

    bool death = false;

    private void Start()
    {
        Hp = maxHP;
    }
    private void Update()
    {
        if(Hp <= 0 && !death)
        {
            death = true;
            if (transform.name == "Boss")
            {
                ScenManager.instance.GoNextStage();
            }
            else
            {
                if(DeathEffect != null) Instantiate(DeathEffect, transform.position, Quaternion.identity); // 죽은 효과 있을때만 실행

                if (Items.Count > 1)//아이템 리스트에 아이템 있어야 실행
                {
                    int rate = Random.Range(0, 100);
                    if (rate <= ItemDropRate) CreateItem();
                }

                Destroy(gameObject);
            }
        }//체력이 0보다 작은데 죽지 않았을때

        if (transform.name.Contains("Boss") && BossHP != null) BossHP.value = Hp / maxHP; //슬라이더 있어야 적용
        else if(HpUI != null) HpUI.fillAmount = Hp / maxHP; //체력바 있어야 적용

    }

    private void CreateItem()
    {
        //스포너 아이템 생성X
        if (transform.name.Contains("Spawner"))
            return;

        int rand = Random.Range(0, Items.Count);
        Instantiate(Items[rand], transform.position, Quaternion.identity);
    }
}
