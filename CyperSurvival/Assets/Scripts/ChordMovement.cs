using UnityEngine;

public class ChordMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float shootInterval = 1f;
    private float shootTimer = 0f;

    private float radius = 5f;
    private Transform player;
    private Vector2 chordStart;
    private Vector2 chordEnd;
    private bool isMovingOnChord = false;

    private EnemyAttack enemyAttack;

    public void StartMovement()
    {
        CalculateChord();
        isMovingOnChord = true;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        enemyAttack = GetComponent<EnemyAttack>();
        radius = gameObject.GetComponent<EnemyAI>().attackRange;
    }

    private void Update()
    {
        if (isMovingOnChord)
        {
            MoveOnChord();
            ShootBullets();
        }
    }

    private void CalculateChord()
    {
        Vector2 dirToB = (transform.position - player.position).normalized;
        Vector2 perpDir = new Vector2(-dirToB.y, dirToB.x);

        chordStart = (Vector2)(player.position) + dirToB * radius + perpDir * (radius * 0.5f);
        chordEnd = (Vector2)(player.position) + dirToB * radius - perpDir * (radius * 1f);
    }

    private void MoveOnChord()
    {
        Vector2 target = chordEnd;
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target) < 0.1f)
        {
            isMovingOnChord = false;
        }
    }

    private void ShootBullets()
    {
        shootTimer += Time.deltaTime;

        if (shootTimer >= shootInterval)
        {
            enemyAttack.RangedAttack(); 
            shootTimer = 0f;
        }
    }
}
