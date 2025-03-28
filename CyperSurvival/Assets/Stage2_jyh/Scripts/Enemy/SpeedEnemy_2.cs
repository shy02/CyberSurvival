using UnityEngine;

public class SpeedEnemy_2 : MonoBehaviour
{
    //�÷��̾�
    private GameObject player = null;

    //����
    private Vector3 dir = Vector3.zero;

    //�÷��̾���� �Ÿ�
    private float distance = 0;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        dir = player.transform.position - transform.position;
        distance = dir.magnitude;

        //0.5f���� ������ �÷��̾� �ݴ�� �̵� ��� ��ġ��
        if (distance <= 1.05f)
        {
            transform.position += -dir * 2f;
        }
    }
}
