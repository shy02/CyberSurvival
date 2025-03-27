using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BossLazer_2 : MonoBehaviour
{
    private Transform playerTransform;
    private Transform bossTransform;
    Vector3 direction = Vector3.zero;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        bossTransform = GameObject.FindGameObjectWithTag("Boss").transform;
        RotateTowardsPlayer();
    }

    void Update()
    {

    }

    void RotateTowardsPlayer()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        transform.localScale = new Vector3(direction.magnitude / 3f, 1, 1);
    }

    public void SetDirection(Vector3 player, Vector3 Boss)
    {
        direction = player - Boss;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage(10);
        }
    }

}
