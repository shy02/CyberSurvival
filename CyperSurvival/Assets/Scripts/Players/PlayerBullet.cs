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
        //if (collision.CompareTag("Monster"))
        //{
        //    Attack(collision);
        //}

        Destroy(gameObject);
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
