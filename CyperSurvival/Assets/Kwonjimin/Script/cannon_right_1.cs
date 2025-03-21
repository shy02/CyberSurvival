using UnityEngine;

public class cannon__1 : MonoBehaviour
{
    public GameObject bulletPrefab; // 총알 프리팹
    public Transform firePoint; // 발사 위치 (빨간 점)
    public float fireRate = 1f; // 발사 간격
    private float nextFireTime = 0f;

    private GameObject player; // 플레이어 참조 변수
    private Animator animator; // Animator 컴포넌트

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
            if (player == null) return;
        }

        // 애니메이션 속도 조정
        animator.speed = 0.5f;

        // 플레이어를 향해 회전
        RotateTowardsPlayer();

        // 주기적으로 총알을 발사
        if (Time.time >= nextFireTime)
        {
            FireBullet();
            nextFireTime = Time.time + fireRate;
        }
    }

    // 총알이 나가는 방향과 동일하게 회전
    void RotateTowardsPlayer()
    {
        // firePoint에서 player 방향으로 계산
        Vector3 direction = player.transform.position - firePoint.position;

        // 방향 벡터의 각도를 계산 (라디안 -> 각도 변환)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 각도를 -90도에서 90도 사이로 제한 (위아래 180도만 회전)
        angle = Mathf.Clamp(angle, -90f, 90f);

        // 오브젝트와 발사 위치(firePoint)를 동일한 방향으로 회전
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    // 총알 발사 함수
    void FireBullet()
    {
        if (player == null) return;

        // firePoint에서 플레이어 방향으로 총알을 발사
        Vector3 direction = (player.transform.position - firePoint.position).normalized;

        // 총알을 firePoint 위치에서 발사
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // 총알 스크립트에 방향 설정
        if (bullet.TryGetComponent<enemyBullet_1>(out var bulletScript))
        {
            bulletScript.SetDirection(direction);
        }
    }
}
