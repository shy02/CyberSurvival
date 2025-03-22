using UnityEngine;

public class EnemyDamage_3 : MonoBehaviour
{
    [SerializeField] float Hp;
    [SerializeField] GameObject DeathEffect;
    public void GetDamage(int dmg) { Hp -= (float)dmg; }
    public void GetDamage(float dmg) { Hp -= dmg; }

    bool death = false;
    private void Update()
    {
        if(Hp <= 0 && !death)
        {
            death = true;
            Instantiate(DeathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
