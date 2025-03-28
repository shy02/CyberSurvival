using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenManager : MonoBehaviour
{
    public List<string> SceneName = new List<string>();
    [SerializeField]int nowStage = 1; //0 = title

    public static ScenManager instance;
    [SerializeField] Transform player;
    [SerializeField] Vector3 Startpos;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
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
        if(nowStage == 5)
        {
            SceneManager.LoadScene("EndScene");
        }
        else {
            SceneManager.LoadScene(SceneName[nowStage]);
        }
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player.position = Startpos;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
