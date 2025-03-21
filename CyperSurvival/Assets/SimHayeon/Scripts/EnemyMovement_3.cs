using UnityEngine;

public class EnemyMovement_3 : MonoBehaviour
{
    public Transform player; // ���߿� ���� �Ŵ������� �����ϴ� ������� �ٲ� ����

    [Tooltip("���� �̵��ϴ� �ӵ�")]
    [SerializeField] float Speed;

    private void Start()
    {
        player = StageManager_3.instance.player;
    }
    void FixedUpdate()
    {
        Vector3 dir = player.transform.position - transform.position;
        transform.Translate(dir.normalized * Speed * Time.deltaTime);
    }
}
