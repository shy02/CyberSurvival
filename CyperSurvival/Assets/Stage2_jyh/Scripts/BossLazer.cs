using UnityEngine;

public class BossLazer : MonoBehaviour
{
    private Transform playerTransform;
    private Transform bossTransform;

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
        Vector3 direction = playerTransform.position - bossTransform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        transform.localScale = new Vector3(direction.magnitude / 2.5f, 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().GetDamage(10);
        }
    }

}
