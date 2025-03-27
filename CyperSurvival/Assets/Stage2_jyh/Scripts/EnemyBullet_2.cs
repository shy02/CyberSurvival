using UnityEngine;

public class EnemyBullet_2 : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float moveSpeed;
    private Vector3 direction;

     void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if(collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

    //가는 방향 설정
    public void SetDir(Vector3 dir)
    {
        direction = dir.normalized;

    }

}
