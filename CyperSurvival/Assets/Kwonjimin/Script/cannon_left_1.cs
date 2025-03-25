using UnityEngine;

public class cannon_left_1 : MonoBehaviour
{
    public GameObject bulletPrefab; // 총알 프리팹
    public Transform pos; // 발사 위치 (자식 오브젝트)
    public float fireRate = 1f; // 발사 간격
    private float nextFireTime = 0f;

    private GameObject player; // 플레이어 참조 변수
    private Animator animator; // 애니메이터

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>(); // 애니메이터 가져오기
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
            if (player == null) return;
        }

        // 🎯 애니메이션 속도 조정
        if (animator != null)
            animator.speed = 0.5f;

        // 🎯 총알 발사 방향으로 회전
        RotateTowardsPlayer();

        // 🎯 일정 간격마다 총알 발사
        if (Time.time >= nextFireTime)
        {
            FireBullet();
            nextFireTime = Time.time + fireRate;
        }
    }

    // 🎯 플레이어 방향으로 회전
    void RotateTowardsPlayer()
    {
        Vector3 direction = player.transform.position - transform.position; // 🔹 부모 오브젝트(cannon_right_1) 기준
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 🔹 회전 보정 없이 플레이어 방향으로 자연스럽게 회전
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    // 🎯 총알 발사 함수
    void FireBullet()
    {
        if (player == null) return;

        // 🔹 pos에서 플레이어 방향으로 총알 발사
        Vector3 direction = (player.transform.position - pos.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, pos.position, Quaternion.identity);

        if (bullet.TryGetComponent<enemyBullet_1>(out var bulletScript))
        {
            bulletScript.SetDirection(direction);
        }
    }
}
