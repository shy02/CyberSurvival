using UnityEngine;

public class missile_1 : MonoBehaviour
{
    public float speed = 5f; // �̻��� �ӵ�
    private Vector3 direction;

    void Start()
    {
        // �߻�� �̻����� �ʱ� ���� ���� (�÷��̾ ���� �̵�)
        direction = transform.up; // �̻����� �� ������ �÷��̾ ���ϵ��� ������
    }

    void Update()
    {
        // �̻����� ����ؼ� ������ ���� �̵�
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾�� �浹 �� �̻��� ����
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

        if (collision.CompareTag("Edge"))
        {
            Destroy(gameObject);
        }

        if (collision.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
