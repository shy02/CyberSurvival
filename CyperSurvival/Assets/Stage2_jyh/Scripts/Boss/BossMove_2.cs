using UnityEngine;
using UnityEngine.AI;

public class BossMove_2 : MonoBehaviour
{
    GameObject player = null;
    //이동
    [SerializeField] private float moveSpeed = 1;
    Vector3 moveDir = Vector3.zero;
    SpriteRenderer spriteRenderer;
    Animator anim;

    //네비게이션
    NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        if (player == null)
            player = GameObject.FindWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

    }

    void Start()
    {
       
    }

    void Update()
    {
        Vector3 vec = player.transform.position - transform.position;
        moveDir = vec.normalized;

        //보스가 플레이어를 바라보게 하기
        if (vec.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        //이동
        if(vec.magnitude > 5f)  //5f보다 멀면 이동
        {
            anim.SetBool("isMove", true);
            agent.destination = player.transform.position;
        }
        else
        {
            anim.SetBool("isMove", false);
            agent.destination = transform.position;
        }

        
    }
}
