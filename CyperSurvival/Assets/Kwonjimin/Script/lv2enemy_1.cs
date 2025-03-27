using UnityEngine;
using System.Collections;

public class lv2enemy_1 : MonoBehaviour
{
    public float delay = 1f; // ù ���� ��� �ð�
    public float fireDelay = 0.2f; // ���� �߻� ����
    public Transform pos1; // ù ��° �Ѿ� �߻� ��ġ
    public Transform pos2; // �� ��° �Ѿ� �߻� ��ġ
    public GameObject bulletPrefab; // �̻��� ������
    public float fireDistance = 5f; // ���� ����
    public float spreadAngle = 15f; // �߰� �Ѿ��� Ȯ�� ����
    public float postAttackDelay = 5f; // ���� �� ���� �ð�

    // �ִϸ��̼� �ӵ��� ������ �� �ִ� ����
    public float animationSpeed = 1f; // �⺻ �ִϸ��̼� �ӵ�

    public float followSpeed = 2f; // �÷��̾ ���󰡴� �ӵ�
    public float followDistance = 8f; // �÷��̾ �� ���� �ȿ� ������ ���󰡱� ����

    private Transform player; // �÷��̾��� Transform
    private Animator animator;
    private bool isIdle = true; // ���� idle ��������
    private float lastAttackTime; // ������ ���� �ð� ���
    private bool isFlipped = false; // x�� �ø� ����

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        InvokeRepeating("TryAttack", delay, fireDelay);
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // ���� ���� ���� ������ ����
        if (distanceToPlayer <= fireDistance && Time.time >= lastAttackTime + postAttackDelay)
        {
            Attack();
        }

        // �÷��̾ ���� �Ÿ� �̳��� ������ ���󰡰�, �׷��� ������ idle ���·�
        if (distanceToPlayer <= followDistance)
        {
            FollowPlayer();
        }
        else if (!animator.GetBool("attack"))
        {
            Idle();
        }
    }

    void Idle()
    {
        if (!isIdle)
        {
            animator.SetBool("attack", false);
            animator.speed = animationSpeed * 0.5f; // Idle �ִϸ��̼� �ӵ� ����
            isIdle = true;
        }
    }

    void Attack()
    {
        if (animator.GetBool("attack")) return; // �̹� ���� ���̸� ����

        isIdle = false;
        animator.SetBool("attack", true); // ���� �ִϸ��̼� ��� ����
        animator.speed = animationSpeed; // ���� �ִϸ��̼� �ӵ� ����

        // �ִϸ��̼��� ���� ������ �� �̻��� �߻�
        StartCoroutine(AttackSequence());
    }

    IEnumerator AttackSequence()
    {
        yield return StartCoroutine(FirePattern()); // �̻��� �߻� (�߻� ���� �ִϸ��̼� ����)

        // �̻��� �߻簡 ������ �ִϸ��̼� ����
        animator.SetBool("attack", false); // ���� �ִϸ��̼� ����
        lastAttackTime = Time.time; // ������ ���� �ð� ���

        // �̻��� �߻簡 ���� �� idle ���·� ���ư�
        Idle();
    }

    IEnumerator FirePattern()
    {
        if (player == null) yield break;

        // 3�� �߻� (pos1 -> pos2 -> pos1 -> pos2 ...)
        for (int i = 0; i < 3; i++)
        {
            FireBulletPattern(pos1);
            FireBulletPattern(pos2);
            yield return new WaitForSeconds(fireDelay);
        }

        yield return new WaitForSeconds(postAttackDelay); // ���� �� ���� �ð�
    }

    void TryAttack()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= fireDistance && Time.time >= lastAttackTime + postAttackDelay)
        {
            Attack();
        }
    }

    void FireBulletPattern(Transform pos)
    {
        if (player == null) return;

        Vector3 direction = (player.position - pos.position).normalized;

        // �⺻ �� ��
        FireBulletFromPosition(pos, direction);

        // Ȯ��� �߰� �� ��
        FireBulletFromPosition(pos, Quaternion.Euler(0, 0, spreadAngle) * direction);
        FireBulletFromPosition(pos, Quaternion.Euler(0, 0, -spreadAngle) * direction);
    }

    void FireBulletFromPosition(Transform pos, Vector3 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, pos.position, Quaternion.identity);
        bullet.transform.right = direction;

        enemyBullet_1 bulletScript = bullet.GetComponent<enemyBullet_1>();
        if (bulletScript != null)
        {
            bulletScript.SetDirection(direction);
        }
    }

    void FollowPlayer()
    {
        // �÷��̾��� x�� ��ġ�� ���� x�� ��ġ���� ���� ���� ���󰡵���
        if (player.position.x < transform.position.x && !isFlipped)
        {
            Flip();
        }
        else if (player.position.x > transform.position.x && isFlipped)
        {
            Flip();
        }

        // �÷��̾ ���󰣴�.
        Vector3 moveDirection = (player.position - transform.position).normalized;
        moveDirection.y = 0; // y�� �̵��� ������ ����
        transform.position += moveDirection * followSpeed * Time.deltaTime;
    }

    void Flip()
    {
        isFlipped = !isFlipped;
        Vector3 localScale = transform.localScale;
        localScale.x = -localScale.x; // x�� ����
        transform.localScale = localScale;
    }
}
