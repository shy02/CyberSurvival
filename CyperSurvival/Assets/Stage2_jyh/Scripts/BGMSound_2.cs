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
        //���� ������ �Ҹ� ��
        if (GameManager.Instance.nowNextStage == true || GameManager.Instance.PlayerHp <= 0)
        {
            audioSource.Stop();
        }

    }
}
