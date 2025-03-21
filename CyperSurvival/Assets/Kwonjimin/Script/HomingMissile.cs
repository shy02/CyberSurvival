using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public float speed = 5f; // �̻��� �ӵ�
    private Transform target;

    public void SetTarget(GameObject player)
    {
        target = player.transform;
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;

            // �̻����� �÷��̾ ���� ȸ���ϵ��� ���� (z�� ȸ�� ����)
            Quaternion rotation = Quaternion.LookRotation(direction); // ���⿡ �´� ȸ���� ���
            rotation.x = 0f; // x�� ȸ�� ����
            rotation.z = 0f; // z�� ȸ�� ����
            transform.rotation = rotation;

            // �̻��� �̵�
            transform.position += direction * speed * Time.deltaTime;
        }
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
