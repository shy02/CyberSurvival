using UnityEngine;
using TMPro;
using System.Collections; // 코루틴 사용을 위해 추가

public class DamageZone : MonoBehaviour
{
    public float noDamageTime = 10f; // 데미지를 받지 않는 시간
    public float damageInterval = 1f; // 데미지를 주는 간격
    public int damageAmount = 5; // 데미지량

    private float damageTimer = 0f;
    private float noDamageTimer = 0f;
    private bool playerInZone = false;

    private AudioSource audioSource;
    public AudioClip noDamageSound; // 10초 동안 재생될 효과음
    public AudioClip loopingDamageSound; // 10초 이후 루프될 효과음

    // 인스펙터에서 TMP 오브젝트를 직접 할당할 수 있게 public으로 설정
    public TextMeshProUGUI warningText; // TMP 오브젝트 참조 (TextMeshProUGUI로 수정)

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // warningText가 할당되지 않았다면 경고 메시지 출력
        if (warningText != null)
        {
            Debug.Log("Warning TMP 오브젝트를 찾았습니다!");
            warningText.gameObject.SetActive(false); // 처음에는 비활성화 상태로 두기
        }
        else
        {
            Debug.LogWarning("Warning TMP 오브젝트를 찾을 수 없습니다.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어가 데미지 존에 진입!");
            playerInZone = true;
            noDamageTimer = 0f;
            damageTimer = 0f; // 데미지 타이머 초기화

            PlayNoDamageSound();
            StartCoroutine(BlinkText(5f)); // TMP 깜빡이기 시작 (5초 동안)
        }
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            noDamageTimer += Time.deltaTime;

            if (noDamageTimer >= noDamageTime)
            {
                if (!audioSource.isPlaying || audioSource.clip != loopingDamageSound)
                {
                    PlayLoopingDamageSound();
                }

                damageTimer += Time.deltaTime;

                if (damageTimer >= damageInterval)
                {
                    other.GetComponent<Player>().TakeDamage(damageAmount);
                    Debug.Log($"플레이어가 {damageAmount} 데미지를 받음!");
                    damageTimer = 0f;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어가 데미지 존을 벗어남.");
            playerInZone = false;
            damageTimer = 0f;
            noDamageTimer = 0f;

            StopSound();
            StopCoroutine(BlinkText(2f)); // 깜빡이는 코루틴 정지
            if (warningText != null)
            {
                warningText.gameObject.SetActive(false); // 문구 숨기기
            }
        }
    }

    void PlayNoDamageSound()
    {
        if (audioSource != null && noDamageSound != null)
        {
            audioSource.clip = noDamageSound;
            audioSource.loop = false;
            audioSource.Play();
            Debug.Log("10초 동안 효과음 재생");
        }
    }

    void PlayLoopingDamageSound()
    {
        if (audioSource != null && loopingDamageSound != null)
        {
            audioSource.clip = loopingDamageSound;
            audioSource.loop = true;
            audioSource.Play();
            Debug.Log("데미지 루프 효과음 시작");
        }
    }

    void StopSound()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
            Debug.Log("모든 효과음 정지");
        }
    }

    IEnumerator BlinkText(float duration)
    {
        if (warningText == null) yield break;

        warningText.gameObject.SetActive(true);
        float elapsedTime = 0f;
        bool isVisible = true;

        while (elapsedTime < duration)
        {
            isVisible = !isVisible;
            warningText.alpha = isVisible ? 1f : 0f; // alpha 값을 조절하여 깜빡임 효과
            yield return new WaitForSeconds(0.5f); // 0.5초마다 깜빡임
            elapsedTime += 0.5f; // 0.5초마다 증가
        }

        warningText.gameObject.SetActive(false); // 5초 후 문구 숨김
    }
}
