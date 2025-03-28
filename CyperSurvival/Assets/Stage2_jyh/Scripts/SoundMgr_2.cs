using UnityEngine;

public class SoundMgr_2 : MonoBehaviour
{
    public static SoundMgr_2 instance;
    AudioSource audioSource;

    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void OneShot(AudioClip audioClip, float volume)
    {
        //사운드 조절
        audioSource.volume = volume;
        audioSource.PlayOneShot(audioClip);
        
    }

    private void Update()
    {
        //보스 잡으면 소리 끔
        if ((GameManager.Instance.nowNextStage == true))
        {
            audioSource.volume = 0;
        }
    }

}
