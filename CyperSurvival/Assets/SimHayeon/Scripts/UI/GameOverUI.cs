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
        Debug.Log("���� over");
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
    }
    public void ApearTryAgain()
    {
        Invoke("Apear", 4.7f);
    }
    private void Apear()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
