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

    public AudioClip fireSound; // 🔹 발사 효과음 추가
    private AudioSource audioSource; // 🔹 오디오 소스
    public float fireVolume = 0.5f; // 🔹 볼륨 조절 (기본값 0.5)

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        InvokeRepeating("TryAttack", delay, fireDelay);

        audioSource = GetComponent<AudioSource>(); // 🔹 AudioSource 가져오기
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // 🔹 없으면 추가
        }
        audioSource.playOnAwake = false; // 🔹 자동 재생 방지
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= fireDistance && Time.time >= lastAttackTime + postAttackDelay)
        {
            Attack();
        }

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

        // 🔹 효과음 재생 (볼륨 조절 적용)
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
        moveDirection.y = 0;
        transform.position += moveDirection * followSpeed * Time.deltaTime;
    }

    void Flip()
    {
        isFlipped = !isFlipped;
        Vector3 localScale = transform.localScale;
        localScale.x = -localScale.x;
        transform.localScale = localScale;
    }
}