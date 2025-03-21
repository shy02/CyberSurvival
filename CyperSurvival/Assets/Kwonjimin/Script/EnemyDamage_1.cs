using UnityEngine;

public class EnemyDamage_1 : MonoBehaviour
{
    [SerializeField] int Hp;

    public void GetDamage(int dmg)
    {
        Hp -= dmg;
        CheckDeath();
    }

    void CheckDeath()
    {
        if (Hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
