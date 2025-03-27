using UnityEngine;

public class missile_1 : MonoBehaviour
{
    public float speed = 3f;
    private Vector3 direction; // �̵� ����

    // �̻����� �÷��̾� ������ ���ϵ��� ����
    public void SetDirection(Vector3 dir)
    {
        direction = (dir - transform.position).normalized; // ������ �÷��̾� ��ġ�� ����

        // z�ุ ȸ��: x, y�� ����, z�� ȸ���ϵ��� ����
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // �÷��̾���� ������ angle�� ���
        transform.rotation = Quaternion.Euler(0, 0, angle); // x, y�� 0���� �����ϰ� z�� ȸ��
    }

    void Update()
    {
        // �̻��� �̵�
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnBecameInvisible()
    {
        // �̻����� ȭ�� ������ ������ ����
        Destroy(gameObject);
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
