using UnityEngine;

public class EnemyMove_2 : MonoBehaviour
{
    GameObject player = null;

    //이동
    [SerializeField]private float moveSpeed = 1;
    Vector3 moveDir = Vector3.zero;
    private SpriteRenderer spriteRenderer;

    //데미지
    [SerializeField] private int damage;

    //isMove
    [HideInInspector]public bool canMove = true;

    void Start()
    {
        if (player == null)
            player = GameObject.FindWithTag("Player");

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
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

        if (canMove == true)
            transform.Translate(moveDir * moveSpeed * Time.deltaTime);
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
