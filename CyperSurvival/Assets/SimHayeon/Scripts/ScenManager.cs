using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenManager : MonoBehaviour
{
    public List<string> SceneName = new List<string>();
    int nowStage = 1; //0 = title

    public static ScenManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else { Destroy(gameObject); }
    }
    private void Start()
    {

        SceneManager.LoadScene("TitleScene");
    }

    public void GoNextStage()//다음 스테이지로 가는 함수
    {
        nowStage++;
        SceneManager.LoadScene(SceneName[nowStage]);
    }
}
