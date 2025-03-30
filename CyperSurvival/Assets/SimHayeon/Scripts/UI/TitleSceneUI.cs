using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneUI : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Stage3");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
