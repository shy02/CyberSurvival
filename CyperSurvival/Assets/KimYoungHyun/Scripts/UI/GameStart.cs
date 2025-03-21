using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    public Button button;

    private void Start()
    {
        if (button == null)
        {
            print("Button is not connected");
            return;
        }
        button.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("PlayerScene");
        print("Game Start!");
    }
}
