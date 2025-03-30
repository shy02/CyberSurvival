using UnityEngine;

public class Player_2 : MonoBehaviour
{
    [SerializeField] int Hp;

    public void GetDamage(int dmg) { Hp -= dmg; }

    private void Update()
    {
        if (Hp <= 0) Destroy(gameObject);
    }
}
