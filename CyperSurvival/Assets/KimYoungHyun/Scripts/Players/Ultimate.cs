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

        //if (!collision.CompareTag(Strings.tagPlayer))
        //{
        //    Destroy(gameObject);
        //}
    }

    private void Attack(Collider2D collision)
    {
        int damage = GameManager.DEAULT_POWER + (playwerPower * 10);
        // EnemyDamage.GetDamage();
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
