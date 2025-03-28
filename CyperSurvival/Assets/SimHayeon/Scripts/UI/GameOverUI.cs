using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    Animator anime;
    AudioSource audio;

    bool nowplay = false;

    private void Awake()
    {
        anime = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }//선언만 있음

    public void GameOver()//게임오버 됐을때 실행
    {
        anime.SetBool("Fade", true);
        if (!nowplay)
        {
            audio.Play();
            nowplay = true;
        }
    }
    public void rotateImage()//이미지 돌리기
    {
        transform.GetChild(0).GetComponent<Animator>().SetBool("rotate", true);
    }

    public void ApearText()//텍스트 나타남
    {
        transform.GetChild(0).GetComponent<Animator>().SetBool("apear", true);
    }
}
