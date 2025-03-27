using UnityEngine;
using System.Collections;

public class lv2enemy_1 : MonoBehaviour
{
    public float delay = 1f; // 첫 공격 대기 시간
    public float fireDelay = 0.2f; // 연속 발사 간격
    public Transform pos1; // 첫 번째 총알 발사 위치
    public Transform pos2; // 두 번째 총알 발사 위치
    public GameObject bulletPrefab; // 미사일 프리팹
    public float fireDistance = 5f; // 공격 범위
    public float spreadAngle = 15f; // 추가 총알의 확산 각도
    public float postAttackDelay = 5f; // 공격 후 쉬는 시간

    // 애니메이션 속도를 조정할 수 있는 변수
    public float animationSpeed = 1f; // 기본 애니메이션 속도

    public float followSpeed = 2f; // 플레이어를 따라가는 속도
    public float followDistance = 8f; // 플레이어가 이 범위 안에 들어오면 따라가기 시작

    private Transform player; // 플레이어의 Transform
    private Animator animator;
    private bool isIdle = true; // 현재 idle 상태인지
    private float lastAttackTime; // 마지막 공격 시간 기록
    private bool isFlipped = false; // x축 플립 여부

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        InvokeRepeating("TryAttack", delay, fireDelay);
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // 공격 범위 내에 있으면 공격
        if (distanceToPlayer <= fireDistance && Time.time >= lastAttackTime + postAttackDelay)
        {
            Attack();
        }

        // 플레이어가 일정 거리 이내에 있으면 따라가고, 그렇지 않으면 idle 상태로
        if (distanceToPlayer <= followDistance)
        {
            FollowPlayer();
        }
        else if (!animator.GetBool("attack"))
        {
            Idle();
        }
    }

    void Idle()
    {
        if (!isIdle)
        {
            animator.SetBool("attack", false);
            animator.speed = animationSpeed * 0.5f; // Idle 애니메이션 속도 조절
            isIdle = true;
        }
    }

    void Attack()
    {
        if (animator.GetBool("attack")) return; // 이미 공격 중이면 리턴

        isIdle = false;
        animator.SetBool("attack", true); // 공격 애니메이션 즉시 시작
        animator.speed = animationSpeed; // 공격 애니메이션 속도 조절

        // 애니메이션을 먼저 실행한 후 미사일 발사
        StartCoroutine(AttackSequence());
    }

    IEnumerator AttackSequence()
    {
        yield return StartCoroutine(FirePattern()); // 미사일 발사 (발사 동안 애니메이션 유지)

        // 미사일 발사가 끝나면 애니메이션 종료
        animator.SetBool("attack", false); // 공격 애니메이션 종료
        lastAttackTime = Time.time; // 마지막 공격 시간 기록

        // 미사일 발사가 끝난 후 idle 상태로 돌아감
        Idle();
    }

    IEnumerator FirePattern()
    {
        if (player == null) yield break;

        // 3번 발사 (pos1 -> pos2 -> pos1 -> pos2 ...)
        for (int i = 0; i < 3; i++)
        {
            FireBulletPattern(pos1);
            FireBulletPattern(pos2);
            yield return new WaitForSeconds(fireDelay);
        }

        yield return new WaitForSeconds(postAttackDelay); // 공격 후 쉬는 시간
    }

    void TryAttack()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= fireDistance && Time.time >= lastAttackTime + postAttackDelay)
        {
            Attack();
        }
    }

    void FireBulletPattern(Transform pos)
    {
        if (player == null) return;

        Vector3 direction = (player.position - pos.position).normalized;

        // 기본 한 발
        FireBulletFromPosition(pos, direction);

        // 확산된 추가 두 발
        FireBulletFromPosition(pos, Quaternion.Euler(0, 0, spreadAngle) * direction);
        FireBulletFromPosition(pos, Quaternion.Euler(0, 0, -spreadAngle) * direction);
    }

    void FireBulletFromPosition(Transform pos, Vector3 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, pos.position, Quaternion.identity);
        bullet.transform.right = direction;

        enemyBullet_1 bulletScript = bullet.GetComponent<enemyBullet_1>();
        if (bulletScript != null)
        {
            bulletScript.SetDirection(direction);
        }
    }

    void FollowPlayer()
    {
        // 플레이어의 x축 위치가 적의 x축 위치보다 작을 때만 따라가도록
        if (player.position.x < transform.position.x && !isFlipped)
        {
            Flip();
        }
        else if (player.position.x > transform.position.x && isFlipped)
        {
            Flip();
        }

        // 플레이어를 따라간다.
        Vector3 moveDirection = (player.position - transform.position).normalized;
        moveDirection.y = 0; // y축 이동은 없도록 설정
        transform.position += moveDirection * followSpeed * Time.deltaTime;
    }

    void Flip()
    {
        isFlipped = !isFlipped;
        Vector3 localScale = transform.localScale;
        localScale.x = -localScale.x; // x축 반전
        transform.localScale = localScale;
    }
}
