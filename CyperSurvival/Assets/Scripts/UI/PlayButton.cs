using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
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
        GameManager.Instance.UIBackground.SetActive(false);
        GameManager.Instance.PlayButton.gameObject.SetActive(false);
        GameManager.Instance.GameOver.SetActive(true);

        GameManager.Instance.HpBarImage.gameObject.SetActive(true);
        GameManager.Instance.PowerImage.gameObject.SetActive(true);
        GameManager.Instance.DefenceImage.gameObject.SetActive(true);

        GameManager.Instance.isGameRunning = true;

        print("Game Start!");
    }
}
