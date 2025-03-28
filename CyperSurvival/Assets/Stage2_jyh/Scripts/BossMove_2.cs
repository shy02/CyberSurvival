using UnityEngine;

public class BossMove_2 : MonoBehaviour
{
    GameObject player = null;
    //�̵�
    [SerializeField] private float moveSpeed = 1;
    Vector3 moveDir = Vector3.zero;
    SpriteRenderer spriteRenderer;
    Animator anim;

    void Start()
    {
        if (player == null)
            player = GameObject.FindWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 vec = player.transform.position - transform.position;
        moveDir = vec.normalized;

        //������ �÷��̾ �ٶ󺸰� �ϱ�
        if (vec.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        //�̵�
        if(vec.magnitude > 5f)  //5f���� �ָ� �̵�
        {
            anim.SetBool("isMove", true);
            transform.Translate(moveDir * moveSpeed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("isMove", false);
        }

        
    }
}
