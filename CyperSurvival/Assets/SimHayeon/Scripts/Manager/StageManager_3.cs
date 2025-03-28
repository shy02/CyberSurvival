using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager_3 : MonoBehaviour
{
    //보스가 스킬들을 어느때 사용하는지
    //다른 스킬들이 함계 참조하는 것들 여기서 가져다 쓰기

    public static StageManager_3 instance = null;
    public Transform player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (instance == null) { instance = this; }
        else { Destroy(this.gameObject); }

    } //싱글톤


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)//씬이 불러와질때 실행
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    



}
