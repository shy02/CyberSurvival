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

            // 2D ȸ�� ����: z�� ȸ���� �ʿ�
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);

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
