using UnityEngine;

public class ObstacleMoving : MonoBehaviour
{
    public float moveDistance = 9f;  // �պ��� �Ÿ� (9 ����)
    public float speed = 2f;         // �̵� �ӵ�
    public bool moveRight = true;    // true�� ����, false�� �������� ����

    private Vector3 startPosition;   // ������Ʈ�� ���� ��ġ

    void Start()
    {
        // ������Ʈ�� ���� ��ġ�� ���� ��ġ�� ����
        startPosition = transform.position;
    }

    void Update()
    {
        // Mathf.PingPong�� ����Ͽ� �պ� ������ (0���� moveDistance���� �պ�)
        float xPosition = Mathf.PingPong(Time.time * speed, moveDistance);

        // moveRight�� true�� ��� ���������� �̵�, false�� ��� �������� �̵�
        if (!moveRight)
        {
            xPosition = moveDistance - xPosition;  // �������� �̵��ϴ� ������� ����
        }

        // ������Ʈ�� ��ġ�� ���� ��ġ���� �պ��ϵ��� ����
        transform.position = new Vector3(startPosition.x + xPosition, transform.position.y, transform.position.z);
    }
}