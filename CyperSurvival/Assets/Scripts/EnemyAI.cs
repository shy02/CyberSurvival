using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3.0f;      // 이동 속도
    public float attackRange = 5.0f;    // 공격을 시작할 최소 거리
    public float attackMaxRange = 7.0f; // 공격을 시작할 최대 거리
    public float attackInterval = 2.0f; // 공격 간격 (초)

    private Transform player;
    private bool isAttacking = false;   // 공격 중인지 확인
    private EnemyAttack enemyAttack;

    public enum EnemyType { Melee, Ranged, Bike, Boss } // 몹 타입
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

        // 공격하면서 이동
        if (enemyType != EnemyType.Melee && distance <= attackMaxRange && distance > attackRange)
        {
            if (!isAttacking) Attack();
            MoveTowardsPlayer();
        }
        // 공격 (Melee 타입 제외하고 가까워지면 멀어짐)
        else if (distance <= attackRange)
        {
            if (!isAttacking) Attack();
            if (enemyType != EnemyType.Melee) AwayFromPlayer();
        }
        // 이동
        
        if (enemyType == EnemyType.Melee && isAttacking) return;

        if (distance > attackRange) MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    private void AwayFromPlayer()
    {
        Vector2 direction = (transform.position - player.position).normalized; // 플레이어 반대 방향
        transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
    }

    private void Attack()
    {
        if (enemyAttack == null) return;

        isAttacking = true;
        enemyAttack.ExecuteAttack(enemyType);

        // 원거리 적은 공격 후에도 이동 가능
        float delay = (enemyType == EnemyType.Ranged) ? attackInterval : attackInterval;
        StartCoroutine(ResetAttack(delay));
    }

    private IEnumerator ResetAttack(float delay)
    {
        yield return new WaitForSeconds(delay);
        isAttacking = false;
    }
}
