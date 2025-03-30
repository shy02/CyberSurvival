using UnityEngine;

public class ObstacleMoving2_1 : MonoBehaviour
{
    public float moveSpeed = 2f;      // �̵� �ӵ�
    private Vector3 targetPosition;   // ��ǥ ��ġ
    private Vector3 startPosition;    // ���� ��ġ
    private int currentPhase = 0;     // ���� �ܰ� (Y �̵�, X �̵� ��)

    void Start()
    {
        // ���� ��ġ ���
        startPosition = transform.position;
        targetPosition = startPosition;  // ���� ��ǥ�� ���� ��ġ
    }

    void Update()
    {
        // ��ǥ ��ġ�� �̵�
        MoveObject();
    }

    void MoveObject()
    {
        // ���� �ܰ迡 ���� �̵�
        float step = moveSpeed * Time.deltaTime; // �� ������ �̵� �Ÿ�

        // ��ǥ ��ġ�� �������� �� �ܰ� ��ȯ
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // ���� �ܰ迡 �´� �̵� ���� ����
            switch (currentPhase)
            {
                case 0:
                    targetPosition = new Vector3(transform.position.x, startPosition.y + 6.9f, transform.position.z);
                    break;
                case 1:
                    targetPosition = new Vector3(transform.position.x + 3.8f, transform.position.y, transform.position.z);
                    break;
                case 2:
                    targetPosition = new Vector3(transform.position.x, transform.position.y - 6.9f, transform.position.z);
                    break;
                case 3:
                    targetPosition = new Vector3(transform.position.x - 3.8f, transform.position.y, transform.position.z);
                    break;
            }

            // �̵� �ܰ� ������Ʈ
            currentPhase = (currentPhase + 1) % 4;
        }

        // ��ǥ ��ġ�� �̵�
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    }
}