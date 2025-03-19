using UnityEngine;

public class lv1Bullet : MonoBehaviour
{
    public float speed = 3f;
    private Vector3 direction; // 이동 방향

    // 미사일이 플레이어 방향을 향하도록 설정
    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;

        // z축만 회전: x, y는 고정, z만 회전하도록 설정
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // 플레이어와의 방향을 angle로 계산
        transform.rotation = Quaternion.Euler(0, 0, angle); // x, y는 0으로 고정하고 z만 회전
    }

    void Update()
    {
        // 미사일 이동
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnBecameInvisible()
    {
        // 미사일이 화면 밖으로 나가면 제거
        Destroy(gameObject);
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
    }
}
