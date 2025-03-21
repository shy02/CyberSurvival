using UnityEngine;

public class lv2enemy_1 : MonoBehaviour
{
    public float delay = 1f;
    public Transform pos1; // 첫 번째 총알 발사 위치
    public Transform pos2; // 두 번째 총알 발사 위치
    public GameObject bulletPrefab; // 미사일 프리팹
    public float fireDistance = 5f; // 공격 범위

    private Transform player; // 플레이어의 Transform
    private EnemyDamage_1 enemyDamage; // HP 관리 스크립트 참조

    void Start()
    {
        enemyDamage = GetComponent<EnemyDamage_1>(); // HP 스크립트 가져오기
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        Invoke("CreateBullet", delay);
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= fireDistance)
        {
            Attack();
        }
        else
        {
            Idle();
        }
    }

    void Idle()
    {
        // 애니메이션 관련 코드 제거
    }

    void Attack()
    {
        // 애니메이션 관련 코드 제거
    }

    void CreateBullet()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < fireDistance)
        {
            // 첫 번째 발사 위치에서 총알 발사
            FireBulletFromPosition(pos1);

            // 두 번째 발사 위치에서 총알 발사
            FireBulletFromPosition(pos2);
        }

        Invoke("CreateBullet", delay);
    }

    void FireBulletFromPosition(Transform pos)
    {
        if (player == null) return;

        Vector3 direction = (player.position - pos.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, pos.position, Quaternion.identity);
        bullet.transform.right = direction;

        enemyBullet_1 bulletScript = bullet.GetComponent<enemyBullet_1>();
        if (bulletScript != null)
        {
            bulletScript.SetDirection(direction);
        }
    }
}
