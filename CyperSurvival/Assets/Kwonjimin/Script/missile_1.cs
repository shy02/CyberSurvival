using UnityEngine;

public class missile_1 : MonoBehaviour
{
    public float speed = 5f; // 미사일 속도
    private Vector3 direction;

    void Start()
    {
        // 발사된 미사일의 초기 방향 설정 (플레이어를 향해 이동)
        direction = transform.up; // 미사일의 위 방향이 플레이어를 향하도록 설정됨
    }

    void Update()
    {
        // 미사일이 계속해서 방향을 따라 이동
        transform.position += direction * speed * Time.deltaTime;
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
