using System.Collections;
using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public float attackRange = 5.0f;
    public float attackMaxRange = 7.0f;
    public float attackInterval = 2.0f; 

    private GameObject playerObject;
    private Transform player;
    private bool isAttacking = false; 
    private EnemyAttack enemyAttack;
    private Rigidbody2D rb;
    public bool ischord = false;

    private bool isFaceRight = true;
    public enum EnemyType { Melee, Ranged, Bike, Boss } 
    public EnemyType enemyType;

    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.transform;
        enemyAttack = GetComponent<EnemyAttack>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Flip();

        if (player == null || rb.bodyType == RigidbodyType2D.Kinematic) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (enemyType == EnemyType.Bike && ischord) return;

        if (enemyType != EnemyType.Melee && distance <= attackMaxRange && distance > attackRange)
        {
            if (!isAttacking) Attack();
            MoveTowardsPlayer();
        }
        else if (distance <= attackRange)
        {
            if (!isAttacking) Attack();
            if (enemyType != EnemyType.Melee) AwayFromPlayer();
        }
        
        if (enemyType == EnemyType.Melee && isAttacking) return;

        if (distance > attackRange) MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    private void AwayFromPlayer()
    {
        Vector2 direction = (transform.position - player.position).normalized; 
        transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
    }

    private void Attack()
    {
        if (enemyAttack == null) return;

        isAttacking = true;
        enemyAttack.ExecuteAttack(enemyType);

        float delay = (enemyType == EnemyType.Ranged) ? attackInterval : attackInterval;
        StartCoroutine(ResetAttack(delay));
    }

    private IEnumerator ResetAttack(float delay)
    {
        yield return new WaitForSeconds(delay);
        isAttacking = false;
    }

    private void Flip()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject.transform.position.x < transform.position.x && isFaceRight) 
        {
            isFaceRight = false;
            FlipCharacter();
        } else if (playerObject.transform.position.x > transform.position.x && !isFaceRight)
        {
            isFaceRight = true;
            FlipCharacter();
        }

    }
    private void FlipCharacter()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
