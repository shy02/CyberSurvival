using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    Animator anime;
    AudioSource audio;

    public bool nowplay = false;

    private void Awake()
    {
        anime = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }//���� ����

    public void GameOver()//���ӿ��� ������ ����
    {
        anime.SetBool("Fade", true);
        if (!nowplay)
        {
            audio.Play();
            nowplay = true;
        }
    }
    public void rotateImage()//�̹��� ������
    {
        transform.GetChild(0).GetComponent<Animator>().SetBool("rotate", true);
    }

    public void ApearText()//�ؽ�Ʈ ��Ÿ��
    {
        transform.GetChild(0).GetComponent<Animator>().SetBool("apear", true);
        Invoke(nameof(ApearTryAgain), 5.5f);
    }
    void ApearTryAgain()//�ؽ�Ʈ ��Ÿ��
    {
        transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
    }
}
