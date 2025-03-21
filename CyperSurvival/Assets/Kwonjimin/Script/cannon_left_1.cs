using UnityEngine;

public class cannon_left_1 : MonoBehaviour
{
    public GameObject bulletPrefab; // �Ѿ� ������
    public Transform firePoint; // �߻� ��ġ
    public float fireRate = 1f; // �߻� ����
    private float nextFireTime = 0f;

    private GameObject player; // �÷��̾� ���� ����
    private Animator animator; // Animator ������Ʈ

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>(); // Animator ������Ʈ ��������
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
            if (player == null) return;
        }

        // �ִϸ��̼� �ӵ� ����
        animator.speed = 0.5f;

        // ���� �Ѿ��� ������ ����� ���� �������� ȸ��
        RotateTowardsPlayer();

        if (Time.time >= nextFireTime)
        {
            FireBullet();
            nextFireTime = Time.time + fireRate;
        }
    }

    // �Ѿ��� ������ ����� �����ϰ� ���Ͱ� ȸ��
    void RotateTowardsPlayer()
    {
        Vector3 direction = player.transform.position - firePoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // ������ -90������ 90�� ���̷� ���� (���Ʒ� 180���� ȸ��)
        angle = Mathf.Clamp(angle, -90f, 90f);

        // ���Ϳ� �߻� ��ġ(firePoint)�� ���� �������� ȸ��
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    // �Ѿ� �߻� �Լ�
    void FireBullet()
    {
        if (player == null) return;

        // firePoint�� �������� �Ѿ��� �߻�
        Vector3 direction = (player.transform.position - firePoint.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        if (bullet.TryGetComponent<enemyBullet_1>(out var bulletScript))
        {
            bulletScript.SetDirection(direction);
        }
    }
}
