using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public float speed = 5f; // 미사일 속도
    private Transform target;

    public void SetTarget(GameObject player)
    {
        target = player.transform;
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;

            // 2D 회전 수정: z축 회전만 필요
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);

            // 미사일 이동
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어와 충돌 시 미사일 제거
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

        if (collision.CompareTag("Edge"))
        {
            Destroy(gameObject);
        }

        if (collision.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
