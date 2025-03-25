using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3.0f;      // �̵� �ӵ�
    public float attackRange = 5.0f;    // ������ ������ �ּ� �Ÿ�
    public float attackMaxRange = 7.0f; // ������ ������ �ִ� �Ÿ�
    public float attackInterval = 2.0f; // ���� ���� (��)

    private Transform player;
    private bool isAttacking = false;   // ���� ������ Ȯ��
    private EnemyAttack enemyAttack;

    public enum EnemyType { Melee, Ranged, Bike, Boss } // �� Ÿ��
    public EnemyType enemyType;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        enemyAttack = GetComponent<EnemyAttack>();
    }

    private void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        // �����ϸ鼭 �̵�
        if (enemyType != EnemyType.Melee && distance <= attackMaxRange && distance > attackRange)
        {
            if (!isAttacking) Attack();
            MoveTowardsPlayer();
        }
        // ���� (Melee Ÿ�� �����ϰ� ��������� �־���)
        else if (distance <= attackRange)
        {
            if (!isAttacking) Attack();
            if (enemyType != EnemyType.Melee) AwayFromPlayer();
        }
        // �̵�
        
        if (enemyType == EnemyType.Melee && isAttacking) return;

        if (distance > attackRange) MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    private void AwayFromPlayer()
    {
        Vector2 direction = (transform.position - player.position).normalized; // �÷��̾� �ݴ� ����
        transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
    }

    private void Attack()
    {
        if (enemyAttack == null) return;

        isAttacking = true;
        enemyAttack.ExecuteAttack(enemyType);

        // ���Ÿ� ���� ���� �Ŀ��� �̵� ����
        float delay = (enemyType == EnemyType.Ranged) ? attackInterval : attackInterval;
        StartCoroutine(ResetAttack(delay));
    }

    private IEnumerator ResetAttack(float delay)
    {
        yield return new WaitForSeconds(delay);
        isAttacking = false;
    }
}
