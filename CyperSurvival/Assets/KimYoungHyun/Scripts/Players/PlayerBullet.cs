using System.Threading;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float bulletSpeed = 10.0f;
    public int playwerPower = 0;
    public Vector3 fireDirection;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position += fireDirection * bulletSpeed * Time.deltaTime;
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
        int damage = GameManager.DEAULT_POWER + (playwerPower*10);
        collision.gameObject.GetComponent<EnemyDamage_3>().GetDamage(damage);
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
