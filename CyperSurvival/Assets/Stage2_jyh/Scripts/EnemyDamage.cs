using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] int Hp;

    public void GetDamage(int dmg) { Hp -= dmg; }

}
