using UnityEngine;

public class ObstacleMoving2 : MonoBehaviour
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
                    // Y������ 8.5��ŭ �̵�
                    targetPosition = new Vector3(transform.position.x, startPosition.y + 8.5f, transform.position.z);
                    break;
                case 1:
                    // X������ 4.71��ŭ �̵�
                    targetPosition = new Vector3(transform.position.x + 4.71f, transform.position.y, transform.position.z);
                    break;
                case 2:
                    // Y������ -8��ŭ �̵�
                    targetPosition = new Vector3(transform.position.x, transform.position.y - 8.5f, transform.position.z);
                    break;
                case 3:
                    // X������ -4.71��ŭ �̵�
                    targetPosition = new Vector3(transform.position.x - 4.71f, transform.position.y, transform.position.z);
                    break;
            }

            // �̵� �ܰ� ������Ʈ
            currentPhase = (currentPhase + 1) % 4;
        }

        // ��ǥ ��ġ�� �̵�
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    }
}