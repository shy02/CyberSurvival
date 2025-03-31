using UnityEngine;

public class PortalTrigger_1 : MonoBehaviour
{
    public AudioClip portalSound; // 포탈 효과음
    public float triggerDistance = 5f; // 효과음이 들리는 최대 거리
    private Transform player;
    private AudioSource audioSource;

    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;

        // AudioSource 설정
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = portalSound;
        audioSource.spatialBlend = 1f; // 3D 효과 적용
        audioSource.rolloffMode = AudioRolloffMode.Logarithmic; // 거리 감쇠 방식을 Logarithmic로 설정 (소리가 점차적으로 작아짐)

        audioSource.minDistance = triggerDistance / 2f; // 소리가 100% 들리는 범위
        audioSource.maxDistance = triggerDistance * 2f; // 소리가 점점 줄어드는 최대 거리 (좀 더 멀리 설정)
        audioSource.playOnAwake = false;

        // 기본 볼륨을 더 키우기 (0.0f ~ 1.0f)
        audioSource.volume = 1.0f; // 최대 볼륨 설정
    }

    void Update()
    {
        if ((GameManager.Instance.nowNextStage || GameManager.Instance.nowGameOver))
        {
            audioSource.Stop();
        }

        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);

            // 포탈이 가까워지면 소리가 재생되도록
            if (distance <= audioSource.minDistance && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
            // 너무 멀어지면 소리가 멈추도록
            else if (distance > audioSource.maxDistance && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}