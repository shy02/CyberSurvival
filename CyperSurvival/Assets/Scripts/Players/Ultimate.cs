using UnityEngine;

public class Ultimate : MonoBehaviour
{
    public int playwerPower = 0;

    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.CompareTag(Strings.tagMonster))
        //{
        //    Attack(collision);
        //}
    }

    private void Attack(Collider2D collision)
    {
        int attack = 100 * playwerPower;
        // EnemyDamage.GetDamage();
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
