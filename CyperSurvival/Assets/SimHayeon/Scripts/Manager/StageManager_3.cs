using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager_3 : MonoBehaviour
{
    //������ ��ų���� ����� ����ϴ���
    //�ٸ� ��ų���� �԰� �����ϴ� �͵� ���⼭ ������ ����

    public static StageManager_3 instance = null;
    public Transform player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (instance == null) { instance = this; }
        else { Destroy(this.gameObject); }

    } //�̱���


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)//���� �ҷ������� ����
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    



}
