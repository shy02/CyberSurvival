using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public float noDamageTime = 10f; // 데미지를 주지 않는 시간 (초)
    public float damageInterval = 1f; // 데미지를 주는 시간 간격 (초)
    public int damageAmount = 5; // 데미지 양
    private float damageTimer = 0f;
    private float noDamageTimer = 0f; // 데미지를 주지 않는 타이머
    private GameObject player;

    private AudioSource audioSource; // AudioSource 변수 추가
    public AudioClip zoneSound; // 구역에 들어왔을 때 재생할 효과음

    private BoxCollider2D zoneCollider;

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // AudioSource 컴포넌트 가져오기
        zoneCollider = GetComponent<BoxCollider2D>(); // DamageZone의 콜라이더 가져오기
    }

    void Update()
    {
        // 플레이어 객체가 없으면 찾기 시작
        if (player == null)
        {
            player = GameObject.FindWithTag("Player"); // 씬 내에서 "Player" 태그를 가진 객체 찾기
            if (player != null)
            {
                Debug.Log("플레이어 객체 찾음!");
                PlayZoneSound(); // 효과음 재생
            }
        }

        // 플레이어가 감지되면 Overlap 사용
        if (player != null)
        {
            // Overlap을 사용하여 플레이어가 구역 안에 있는지 확인
            Collider2D[] hitColliders = Physics2D.OverlapBoxAll(zoneCollider.bounds.center, zoneCollider.bounds.size, 0f, LayerMask.GetMask("Player"));

            // hitColliders 배열에서 플레이어가 있는지 확인
            foreach (Collider2D collider in hitColliders)
            {
                Debug.Log($"충돌한 객체: {collider.gameObject.name}"); // 어떤 객체가 충돌했는지 확인

                if (collider.CompareTag("Player"))
                {
                    Debug.Log("플레이어가 구역 안에 있음"); // 플레이어가 구역 안에 있을 때 확인
                    noDamageTimer += Time.deltaTime;

                    if (noDamageTimer >= noDamageTime)
                    {
                        damageTimer += Time.deltaTime;

                        if (damageTimer >= damageInterval)
                        {
                            player.GetComponent<Player>().TakeDamage(damageAmount);
                            damageTimer = 0f;
                        }
                    }
                }
            }

            // 구역에 플레이어가 없는 경우 디버그
            if (hitColliders.Length == 0)
            {
                Debug.Log("플레이어가 구역에 없음");
            }
        }
    }

    void PlayZoneSound()
    {
        if (audioSource != null && zoneSound != null)
        {
            audioSource.clip = zoneSound;
            audioSource.Play();
            Debug.Log("효과음 재생됨");
        }
    }
}