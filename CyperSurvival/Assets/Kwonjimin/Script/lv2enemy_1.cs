using UnityEngine;

public class lv2enemy_1 : MonoBehaviour
{
    public float delay = 1f;
    public Transform pos1; // ù ��° �Ѿ� �߻� ��ġ
    public Transform pos2; // �� ��° �Ѿ� �߻� ��ġ
    public GameObject bulletPrefab; // �̻��� ������
    public float fireDistance = 5f; // ���� ����

    private Transform player; // �÷��̾��� Transform
    private EnemyDamage_1 enemyDamage; // HP ���� ��ũ��Ʈ ����

    void Start()
    {
        enemyDamage = GetComponent<EnemyDamage_1>(); // HP ��ũ��Ʈ ��������
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        Invoke("CreateBullet", delay);
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= fireDistance)
        {
            Attack();
        }
        else
        {
            Idle();
        }
    }

    void Idle()
    {
        // �ִϸ��̼� ���� �ڵ� ����
    }

    void Attack()
    {
        // �ִϸ��̼� ���� �ڵ� ����
    }

    void CreateBullet()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < fireDistance)
        {
            // ù ��° �߻� ��ġ���� �Ѿ� �߻�
            FireBulletFromPosition(pos1);

            // �� ��° �߻� ��ġ���� �Ѿ� �߻�
            FireBulletFromPosition(pos2);
        }

        Invoke("CreateBullet", delay);
    }

    void FireBulletFromPosition(Transform pos)
    {
        if (player == null) return;

        Vector3 direction = (player.position - pos.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, pos.position, Quaternion.identity);
        bullet.transform.right = direction;

        enemyBullet_1 bulletScript = bullet.GetComponent<enemyBullet_1>();
        if (bulletScript != null)
        {
            bulletScript.SetDirection(direction);
        }
    }
}
