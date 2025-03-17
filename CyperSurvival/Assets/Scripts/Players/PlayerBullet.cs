using System.Threading;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float bulletSpeed = 10.0f;
    public Vector3 fireDirection;

    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.CompareTag("Monster"))
        //{
        //    // EnemyDamage.GetDamage();
        //}
    }

    void Update()
    {
        transform.position += fireDirection * bulletSpeed * Time.deltaTime;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
