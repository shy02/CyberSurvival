using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenManager : MonoBehaviour
{
    public List<string> SceneName = new List<string>();
    int nowStage = 0; //0 = title

    public static ScenManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else { Destroy(gameObject); }
    }

    public void GoNextStage()//���� ���������� ���� �Լ�
    {
        nowStage++;
        SceneManager.LoadScene(SceneName[nowStage]);
    }
}
