using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player; // ���߿� ���� �Ŵ������� �����ϴ� ������� �ٲ� ����

    [Tooltip("���� �̵��ϴ� �ӵ�")]
    [SerializeField] float Speed;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void FixedUpdate()
    {
        Vector3 dir = player.transform.position - transform.position;
        transform.Translate(dir.normalized * Speed * Time.deltaTime);
    }
}
