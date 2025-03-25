using UnityEngine;
using UnityEngine.UI;
public class EnemyDamage_3 : MonoBehaviour
{
    [SerializeField] float maxHP;
    float Hp;
    [SerializeField] GameObject DeathEffect;
    [SerializeField] Image HpUI;
    [SerializeField] Slider BossHP;
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
            Instantiate(DeathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (transform.name == "Boss") BossHP.value = Hp / maxHP;
        else HpUI.fillAmount = Hp / maxHP;

    }


}
