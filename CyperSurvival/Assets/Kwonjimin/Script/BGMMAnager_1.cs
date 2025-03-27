using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMMAnager_1 : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();

        // 씬이 로드될 때마다 배경음악을 정지하도록 이벤트 추가
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        // 보스가 죽으면 배경음악 정지
        if (GameManager.Instance.nowNextStage && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    // 씬 로드 시 배경음악 멈추기
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        audioSource.Stop();
    }

    // 객체가 파괴될 때 이벤트 해제
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}