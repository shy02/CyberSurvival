using UnityEngine;
using System.Collections;

public class lv2enemy_1 : MonoBehaviour
{
    public float delay = 1f;
    public float fireDelay = 0.2f;
    public Transform pos1;
    public Transform pos2;
    public GameObject bulletPrefab;
    public float fireDistance = 5f;
    public float spreadAngle = 15f;
    public float postAttackDelay = 5f;
    public float animationSpeed = 1f;
    public float followSpeed = 2f;
    public float followDistance = 8f;

    private Transform player;
    private Animator animator;
    private bool isIdle = true;
    private float lastAttackTime;
    private bool isFlipped = false;
    private bool isGameOver = false;

    private SpriteRenderer spriteRenderer;
    public AudioClip fireSound;
    private AudioSource audioSource;
    public float fireVolume = 0.5f;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        InvokeRepeating("TryAttack", delay, fireDelay);

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (player == null) return;

        // 보스가 죽었거나 게임 오버 상태라면 발사 중지
        if (GameManager.Instance.PlayerHp <= 0 || GameManager.Instance.nowNextStage)
        {
            audioSource.Stop();
            StopAttack();  // 미사일 발사 중지
            return; // 미사일 발사 조건이 아닐 경우, 나머지 업데이트 코드 실행하지 않음
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= fireDistance && Time.time >= lastAttackTime + postAttackDelay)
        {
            Attack();
        }

        if (!animator.GetBool("attack"))
        {
            FollowPlayer();
        }

        if (!animator.GetBool("attack"))
        {
            Idle();
        }
    }

    void Idle()
    {
        if (!isIdle)
        {
            animator.SetBool("attack", false);
            animator.speed = animationSpeed * 0.5f;
            isIdle = true;
        }
    }

    void Attack()
    {
        if (animator.GetBool("attack")) return;

        isIdle = false;
        animator.SetBool("attack", true);
        animator.speed = animationSpeed;
        StartCoroutine(AttackSequence());
    }

    IEnumerator AttackSequence()
    {
        yield return StartCoroutine(FirePattern());
        animator.SetBool("attack", false);
        lastAttackTime = Time.time;
        Idle();
    }

    IEnumerator FirePattern()
    {
        if (player == null) yield break;

        for (int i = 0; i < 3; i++)
        {
            FireBulletPattern(pos1);
            FireBulletPattern(pos2);
            yield return new WaitForSeconds(fireDelay);
        }

        yield return new WaitForSeconds(postAttackDelay);
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

        FireBulletFromPosition(pos, direction);
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

        if (fireSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(fireSound, fireVolume);
        }
    }

    void FollowPlayer()
    {
        if (player.position.x < transform.position.x && !isFlipped)
        {
            Flip();
        }
        else if (player.position.x > transform.position.x && isFlipped)
        {
            Flip();
        }

        Vector3 moveDirection = (player.position - transform.position).normalized;
        moveDirection.x = player.position.x - transform.position.x;
        moveDirection.y = player.position.y - transform.position.y;

        Vector3 finalMoveDirection = new Vector3(moveDirection.x, moveDirection.y, 0).normalized;
        transform.position += finalMoveDirection * followSpeed * Time.deltaTime;
    }

    void Flip()
    {
        isFlipped = !isFlipped;

        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }

    // 게임 오버 또는 보스가 죽으면 미사일 발사 중지
    void StopAttack()
    {
        StopCoroutine("FirePattern"); // 공격 패턴 중지
        animator.SetBool("attack", false); // 애니메이션 중지
    }
}
