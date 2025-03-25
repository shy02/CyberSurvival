using UnityEngine;

public class Ultimate : MonoBehaviour
{
    public int playwerPower = 0;

    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Strings.tagBoss))
        {
            Attack(collision);
        }
    }

    private void Attack(Collider2D collision)
    {
        int damage = GameManager.DEAULT_POWER + (playwerPower * 10);
        collision.gameObject.GetComponent<EnemyDamage_3>().GetDamage(damage);
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
