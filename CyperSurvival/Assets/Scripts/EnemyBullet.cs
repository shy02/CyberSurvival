using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage = 1;
    public float speed = 6.0f;

    private Transform player;
    private Vector2 moveDirection;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        moveDirection = (player.position - transform.position).normalized;

    }
    private void Update()
    {
        transform.position += (Vector3)moveDirection * speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerDamage>().GetDamage(damage);
            Destroy(gameObject);
        } else if (collision.CompareTag("Wall")) { Destroy(gameObject); }
    }
}
