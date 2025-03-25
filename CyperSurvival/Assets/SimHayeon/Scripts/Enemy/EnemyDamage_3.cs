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
            if (transform.name == "Boss")
            {
                death = true;
                ScenManager.instance.GoNextStage();
            }
            else
            {
                death = true;
                Instantiate(DeathEffect, transform.position, Quaternion.identity);
                int rate = Random.Range(0, 100);
                if(rate <= ItemDropRate) CreateItem();
                Destroy(gameObject);
            }
        }

        if (transform.name == "Boss") BossHP.value = Hp / maxHP;
        else HpUI.fillAmount = Hp / maxHP;

    }

    private void CreateItem()
    {
        int rand = Random.Range(0, Items.Count);
        Instantiate(Items[rand], transform.position, Quaternion.identity);
    }
}
