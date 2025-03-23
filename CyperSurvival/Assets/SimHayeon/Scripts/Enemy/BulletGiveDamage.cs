using UnityEngine;

public class BulletGiveDamage : MonoBehaviour
{
    [SerializeField] int Damage = 50;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage(Damage);
            Destroy(gameObject);
        }
    }
}
