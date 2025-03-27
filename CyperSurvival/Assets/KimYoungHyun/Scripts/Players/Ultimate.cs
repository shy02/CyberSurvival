using UnityEngine;

public class Ultimate : MonoBehaviour
{
    public int playwerPower = 0;

    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Strings.tagBoss) || collision.CompareTag(Strings.tagEnemy))
        {
            Attack(collision);
        }
    }

    private void Attack(Collider2D collision)
    {
        int damage = GameManager.DEFAULT_ULTIMATE_POWER + playwerPower;
        collision.gameObject.GetComponent<EnemyDamage_3>().GetDamage(damage);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
