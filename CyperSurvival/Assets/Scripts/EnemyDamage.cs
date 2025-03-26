using UnityEngine;

public class EnemyDamage_3 : MonoBehaviour
{
    [SerializeField] int Hp;

    public void GetDamage(int dmg) { Hp -= dmg; }

    private void Update()
    {
        if (Hp <= 0) Destroy(gameObject);
    }
}