using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneUI : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
