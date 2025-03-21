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

            // 미사일이 플레이어를 향해 회전하도록 설정 (z축 회전 제외)
            Quaternion rotation = Quaternion.LookRotation(direction); // 방향에 맞는 회전값 계산
            rotation.x = 0f; // x축 회전 제한
            rotation.z = 0f; // z축 회전 제한
            transform.rotation = rotation;

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
