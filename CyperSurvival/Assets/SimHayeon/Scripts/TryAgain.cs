using UnityEngine;
using UnityEngine.SceneManagement;

public class TryAgain : MonoBehaviour
{
    public void Click_tryagain()
    {
        GameManager.Instance.ResetGame();
        SceneManager.LoadScene("TitleScene");
        
        if (SceneManager.GetActiveScene().name != "EndScene")
        {
            transform.parent.parent.parent.GetComponent<AudioSource>().Stop();
            transform.parent.parent.parent.GetComponent<GameOverUI>().nowplay = false;
            //transform.parent.parent.parent.gameObject.SetActive(false);
            transform.parent.parent.parent.GetComponent<Animator>().SetBool("Fade", false);
            transform.parent.parent.GetComponent<Animator>().SetBool("rotate", false);
            transform.parent.GetComponent<Animator>().SetBool("apear", false);
            gameObject.SetActive(false);
            ScenManager.instance.Reset();
        }
    }
}
