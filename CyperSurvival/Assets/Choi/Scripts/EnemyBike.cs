using UnityEngine;
using System.Collections;

public class EnemyBike : MonoBehaviour
{
    private Transform player;
    public float detectionRange = 5f;
    public float dashSpeed = 10f;
    public float dashDuration = 0.2f;
    public float wanderTime = 3f;
    public float wanderSpeed = 2f;
    public float chaseSpeed = 3.5f;

    public GameObject bulletPrefab;
    private Transform firePoint;

    private bool isFaceRight = true;
    private GameObject playerObject;
    private Rigidbody2D rb;
    private bool isDashing = false;
    private bool isWandering = false;
    private bool chasePlayer = true;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerObject = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
        firePoint = transform;
    }

    private void Update()
    {
        Flip();

        if (player == null || isDashing || isWandering) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange && chasePlayer)
        {
            Vector2 dashDirection = GetDashDirection(player.position);
            StartCoroutine(DashAndWander(dashDirection));
        }
        else if (chasePlayer)
        {
            ChasePlayer();
        }
    }

    private Vector2 GetDashDirection(Vector2 playerPos)
    {
        Vector2 direction = (transform.position - (Vector3)playerPos).normalized;

        if (direction.y > 0.7f)
        {
            if (direction.x > 0.7f) return new Vector2(-1, 1);
            else if (direction.x < -0.7f) return new Vector2(1, 1);
            else return Vector2.left;
        }
        else if (direction.y < -0.7f)
        {
            if (direction.x > 0.7f) return new Vector2(1, -1);
            else if (direction.x < -0.7f) return new Vector2(-1, -1);
            else return Vector2.right;
        }
        else
        {
            if (direction.x > 0.7f) return Vector2.up;
            else return Vector2.down;
        }
    }

    private IEnumerator DashAndWander(Vector2 dashDirection)
    {
        isDashing = true;
        chasePlayer = false;
        rb.linearVelocity = dashDirection * dashSpeed;
        animator.SetTrigger("isDash");

        StartCoroutine(SpawnBulletDuringDash(dashDirection));

        yield return new WaitForSeconds(dashDuration);
        rb.linearVelocity = Vector2.zero;
        isDashing = false;

        StartCoroutine(WanderAround());
    }

    private IEnumerator SpawnBulletDuringDash(Vector2 direction)
    {
        float bulletSpawnInterval = 0.1f;
        float elapsedTime = 0f;

        while (elapsedTime < dashDuration)
        {
            SpawnBullet(direction);
            elapsedTime += bulletSpawnInterval;
            yield return new WaitForSeconds(bulletSpawnInterval);
        }
    }

    private IEnumerator WanderAround()
    {
        isWandering = true;

        float wanderStartTime = Time.time;
        while (Time.time < wanderStartTime + wanderTime)
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            rb.linearVelocity = randomDirection * wanderSpeed;
            yield return new WaitForSeconds(0.5f);
        }

        rb.linearVelocity = Vector2.zero;
        isWandering = false;
        chasePlayer = true;
    }

    private void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * chaseSpeed;
    }

    private void SpawnBullet(Vector2 direction)
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        }
    }

    private void Flip()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject.transform.position.x < transform.position.x && isFaceRight)
        {
            isFaceRight = false;
            FlipCharacter();
        }
        else if (playerObject.transform.position.x > transform.position.x && !isFaceRight)
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
