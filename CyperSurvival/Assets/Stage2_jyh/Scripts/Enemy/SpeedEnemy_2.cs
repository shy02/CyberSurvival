using UnityEngine;

public class SpeedEnemy_2 : MonoBehaviour
{
    //플레이어
    private GameObject player = null;

    //방향
    private Vector3 dir = Vector3.zero;

    //플레이어와의 거리
    private float distance = 0;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        dir = player.transform.position - transform.position;
        distance = dir.magnitude;

        //1.1f보다 가까우면 플레이어 반대로 이동 계속 박치기
        if (distance <= 1.1f)
        {
            transform.position += -dir * 2f;
        }
    }
}
