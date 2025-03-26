using UnityEngine;

public class BossMove_2 : MonoBehaviour
{
    GameObject player = null;

    //이동
    public float moveSpeed = 1;
    Vector3 moveDir = Vector3.zero;
    bool isMove = false;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

        if (player == null)
            player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        MoveAnimation();

        Vector3 vec = player.transform.position - transform.position;

        //이미지 반전
        if(vec.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if (vec.magnitude > 5)
        {
            isMove = true;
            moveDir = vec.normalized;
            transform.Translate(moveDir * moveSpeed * Time.deltaTime);
        }
        else
        {
            isMove = false;
        }
    }

    void MoveAnimation()
    {
        anim.SetBool("isMove", isMove);
    }

}
