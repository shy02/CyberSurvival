using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    bool nowplay = false;
    public void GameOver()
    {
        GetComponent<Animator>().SetBool("Fade", true);
        if (!nowplay)
        {
            GetComponent<AudioSource>().Play();
            nowplay = true;
        }
    }
    public void rotateImage()
    {
        transform.GetChild(0).GetComponent<Animator>().SetBool("rotate", true);
    }

    public void ApearText()
    {
        transform.GetChild(0).GetComponent<Animator>().SetBool("apear", true);
    }
}
