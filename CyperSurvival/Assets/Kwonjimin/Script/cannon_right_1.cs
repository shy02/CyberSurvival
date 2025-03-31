using UnityEngine;

public class cannon_right_1 : MonoBehaviour
{
    public GameObject bulletPrefab; // 총알 프리팹
    public Transform pos; // 발사 위치 (자식 오브젝트)
    public float fireRate = 1f; // 발사 간격
    private float nextFireTime = 0f;

    private GameObject player; // 플레이어 참조 변수
    private Animator animator; // 애니메이터

    public AudioClip fireSound; // 🔹 발사 효과음 추가
    private AudioSource audioSource; // 🔹 오디오 소스
    public float fireVolume = 0.3f; // 🔹 볼륨 조절

    private bool isGameOver = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>(); // 애니메이터 가져오기
        audioSource = GetComponent<AudioSource>(); // 🔹 AudioSource 가져오기

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // 🔹 없으면 추가
        }

        audioSource.playOnAwake = false; // 🔹 자동 재생 방지
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
            if (player == null) return;
        }

        // 게임이 다음 스테이지로 넘어가거나 게임 오버 상태일 때
        if (GameManager.Instance.nowNextStage || GameManager.Instance.nowGameOver)
        {
            if (animator != null)
                animator.speed = 0f; // 애니메이션 정지
            audioSource.Stop(); // 발사 음성도 정지
            return; // 발사 및 애니메이션 처리 중지
        }

        if (animator != null)
            animator.speed = 0.3f;

        RotateTowardsPlayer();

        if (Time.time >= nextFireTime)
        {
            FireBullet();
            nextFireTime = Time.time + fireRate;
        }
    }

    void RotateTowardsPlayer()
    {
        Vector3 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 180f);
    }

    void FireBullet()
    {
        if (player == null) return;
        // 게임이 다음 스테이지로 넘어가거나 게임 오버 상태일 때 발사 안 함
        if (GameManager.Instance.nowNextStage || GameManager.Instance.nowGameOver) return;

        // 🔹 효과음 재생 (볼륨 조절 적용)
        if (fireSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(fireSound, fireVolume);
        }

        Vector3 direction = (player.transform.position - pos.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, pos.position, Quaternion.identity);

        if (bullet.TryGetComponent<enemyBullet_1>(out var bulletScript))
        {
            bulletScript.SetDirection(direction);
        }
    }
}
