using UnityEngine;

public class lv1enemy_1 : MonoBehaviour
{
    public float delay = 1f;
    public Transform pos; // 총알 발사 위치
    public GameObject bulletPrefab; // 미사일 프리팹
    public float fireDistance = 5f; // 공격 범위

    private Transform player; // 플레이어의 Transform
    private Animator animator;

    public AudioClip fireSound; // 🔹 발사 효과음 추가
    private AudioSource audioSource; // 🔹 오디오 소스
    public float fireVolume = 0.5f; // 🔹 볼륨 조절 (기본값 0.5)

    private bool isAttacking = false; // 공격 중인지 여부 확인

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        audioSource = GetComponent<AudioSource>(); // 🔹 AudioSource 가져오기
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // 🔹 없으면 추가
        }
        audioSource.playOnAwake = false; // 🔹 자동 재생 방지

        Invoke("CreateBullet", delay);
    }

    void Update()
    {
        if (player == null) return;

        // 보스가 죽었거나 게임 오버일 경우 미사일 발사 멈추기
        if (GameManager.Instance.PlayerHp <= 0 || GameManager.Instance.nowNextStage || GameManager.Instance.nowGameOver)
        {
            StopAttack(); // 공격 중지
            return; // 그 외 로직을 실행하지 않음
        }

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
        animator.SetBool("attack", false);
    }

    void Attack()
    {
        if (!isAttacking) // 공격 중이지 않으면 공격 시작
        {
            isAttacking = true;
            animator.SetBool("attack", true);
            Invoke("CreateBullet", delay); // 미사일 발사 시작
        }
    }

    void StopAttack()
    {
        // 미사일 발사 멈추기
        isAttacking = false;
        animator.SetBool("attack", false);
        CancelInvoke("CreateBullet"); // `CreateBullet` 호출 중지
        audioSource.Stop(); // 발사 효과음 멈추기
    }

    void CreateBullet()
    {
        if (player == null || !isAttacking) return; // 플레이어나 공격 상태가 아니면 총알 생성 안함

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < fireDistance)
        {
            Vector3 direction = (player.position - pos.position).normalized;
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

        // 계속해서 발사하려면 `Invoke`로 반복 호출
        if (isAttacking)
        {
            Invoke("CreateBullet", delay);
        }
    }
}