using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneUI : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Stage4");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
