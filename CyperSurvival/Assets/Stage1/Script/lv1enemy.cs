using UnityEngine;

public class lv1enemy : MonoBehaviour
{
    public int HP = 100;
    public float speed = 3f;
    public float delay = 1f;
    public Transform pos; // 총알 발사 위치
    public GameObject bulletPrefab; // 미사일 프리팹
    public float fireDistance = 5f; // 발사 거리 기준

    void Start()
    {
        // 첫 번째 총알 발사 예약
        Invoke("CreateBullet", delay);
    }

    void CreateBullet()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // 플레이어 찾기
        if (player == null)
        {
            Debug.LogWarning("🚨 플레이어를 찾을 수 없습니다!");
            return;
        }

        // 플레이어와의 거리 체크
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < fireDistance)
        {
            // 미사일 방향 계산
            Vector3 direction = (player.transform.position - pos.position).normalized;

            // 미사일 생성 및 방향 설정
            GameObject bullet = Instantiate(bulletPrefab, pos.position, Quaternion.identity);
            bullet.transform.right = direction; // 미사일이 플레이어를 향하게 회전

            // 미사일에 방향 정보 전달
            lv1Bullet bulletScript = bullet.GetComponent<lv1Bullet>();
            if (bulletScript != null)
            {
                bulletScript.SetDirection(direction);
            }
        }

        // 일정 시간 후 다시 발사 시도
        Invoke("CreateBullet", delay);
    }
}
