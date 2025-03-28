using UnityEngine;

public class BGMSound_2 : MonoBehaviour
{
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    void Start()
    {
        
    }

    void Update()
    {
        //보스 잡으면 소리 끔
        if ((GameManager.Instance.nowNextStage == true))
        {
            audioSource.Stop();
        }
    }
}
