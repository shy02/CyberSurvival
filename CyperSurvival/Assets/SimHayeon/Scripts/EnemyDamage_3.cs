using UnityEngine;

public class EnemyDamage_3 : MonoBehaviour
{
    [SerializeField] int Hp;

    public void GetDamage(int dmg) { Hp -= dmg; }

}
