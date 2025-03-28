using System.Collections;
using UnityEngine;

public class Boss_1 : MonoBehaviour
{
    public GameObject missilePrefab;
    public GameObject homingMissilePrefab;
    public GameObject bossObject;

    public float speed = 3f;
    public float homingMissileCooldown = 5f;
    private float nextHomingMissileTime = 0f;

    public float portalMissileCooldown = 3f;
    private float nextPortalMissileTime = 0f;

    public float portalShotDelay = 0.3f;
    public int missileCount = 3;

    private Animator animator;
    private GameObject player;
    private Transform[] portalPositions;

    // 🔹 사운드 관련 변수 추가
    public AudioClip missileFireSound; // 일반 미사일 효과음
    public AudioClip homingMissileFireSound; // 호밍 미사일 효과음
    public float missileVolume = 1.0f; // 일반 미사일 볼륨
    public float homingMissileVolume = 1.0f; // 호밍 미사일 볼륨
    private AudioSource audioSource; // 오디오 소스

    void Start()
    {
        animator = bossObject.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // 없으면 추가
        }

        audioSource.volume = 1.0f; // 기본 볼륨은 1.0f로 설정
    }

    void Update()
    {
        if (player == null || portalPositions == null || portalPositions.Length == 0)
        {
            return;
        }

        FollowPlayer();

        if (Time.time > nextPortalMissileTime)
        {
            StartCoroutine(FirePortalMissiles());
            nextPortalMissileTime = Time.time + portalMissileCooldown;
        }

        if (Time.time > nextHomingMissileTime)
        {
            LaunchHomingMissile();
            nextHomingMissileTime = Time.time + homingMissileCooldown;
        }

        FlipTowardsPlayer();
    }

    public void SetBossData(GameObject player, Transform[] portalPositions)
    {
        this.player = player;
        this.portalPositions = portalPositions;
    }

    void FollowPlayer()
    {
        if (player != null)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    IEnumerator FirePortalMissiles()
    {
        for (int i = 0; i < missileCount; i++)
        {
            foreach (Transform portal in portalPositions)
            {
                if (portal == null) continue;

                GameObject missile = Instantiate(missilePrefab, portal.position, Quaternion.identity);
                missile_1 missileScript = missile.GetComponent<missile_1>();

                if (missileScript != null)
                    missileScript.SetDirection(player.transform.position);
                else
                    Debug.LogError("미사일 프리팹에 missile_1 스크립트가 없습니다!");

                // 🔹 미사일 발사 효과음 (일반 미사일 소리만 두 배로 설정)
                PlaySound(missileFireSound, missileVolume * 2);  // 여기서 볼륨을 두 배로 설정

                yield return new WaitForSeconds(portalShotDelay);
            }
        }
        yield return new WaitForSeconds(3f);
    }

    void LaunchHomingMissile()
    {
        animator.SetBool("attack", true);

        GameObject missile = Instantiate(homingMissilePrefab, transform.position, Quaternion.identity);
        missile.GetComponent<HomingMissile>().SetTarget(player);

        // 🔹 호밍 미사일 발사 효과음 (호밍 미사일 볼륨 적용)
        PlaySound(homingMissileFireSound, homingMissileVolume);

        Invoke("StopAttackAnimation", 0.2f);
    }

    void StopAttackAnimation()
    {
        animator.SetBool("attack", false);
    }

    void FlipTowardsPlayer()
    {
        if (player != null)
        {
            Vector3 playerPos = player.transform.position;

            if (transform.position.x < playerPos.x)
            {
                if (transform.localScale.x < 0)
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                if (transform.localScale.x > 0)
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
    }

    // 🔹 사운드 재생 함수 (볼륨 조절 가능)
    void PlaySound(AudioClip clip, float volume)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip, volume);
        }
    }
}
