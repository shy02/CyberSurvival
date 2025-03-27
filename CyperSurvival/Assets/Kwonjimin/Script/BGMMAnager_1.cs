using UnityEngine;

public class BGMMAnager_1 : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    void Update()
    {
        // 🎯 보스가 죽으면 배경음악 정지
        if (GameManager.Instance.nowNextStage && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
