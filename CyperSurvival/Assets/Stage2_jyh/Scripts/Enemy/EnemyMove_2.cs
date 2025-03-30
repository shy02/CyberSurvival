using UnityEngine;
using UnityEngine.AI;

public class EnemyMove_2 : MonoBehaviour
{
    GameObject player = null;

    //이동
    [SerializeField] private float moveSpeed = 1;
    Vector3 moveDir = Vector3.zero;
    private SpriteRenderer spriteRenderer;

    //데미지
    [SerializeField] private int damage;

    //isMove
    [HideInInspector] public bool canMove = true;

    //죽을때 사운드 재생
    [SerializeField] private AudioClip deathSound;
    EnemyDamage_3 enemyDamage_3;

    //네비게이션
    NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }


        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyDamage_3 = GetComponent<EnemyDamage_3>();

    }

    void Update()
    {
        if ((deathSound != null))
        {
            if (enemyDamage_3.GetHp() <= 0)
            {
                SoundMgr_2.instance.OneShot(deathSound, 0.5f);
                return;
            }
        }

        Vector3 vec = player.transform.position - transform.position;
        moveDir = vec.normalized;

        //좌우반전
        if (vec.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        //이동
        if (canMove == true)
            agent.SetDestination(player.transform.position);
        //멈춤
        else
            agent.SetDestination(transform.position);

        //보스잡거나 플레이어가 죽으면 사라짐
        if(GameManager.Instance.nowNextStage == true || GameManager.Instance.PlayerHp <= 0)
        {
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(damage);
        }
    }

    public void StopMove()
    {
        canMove = false;
    }

    public void StartMove()
    {
        canMove = true;
    }

}
