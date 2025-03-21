using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    GameObject player = null;

    //ÀÌµ¿
    public float moveSpeed = 1;
    Vector3 moveDir = Vector3.zero;


    void Start()
    {
        if (player == null)
            player = GameObject.FindWithTag("Player");
    }

    void Update()
    {

        if (this.gameObject.tag == "Boss")
        {
            Vector3 vec = player.transform.position - transform.position;

            if (vec.magnitude > 6)
            {
                moveDir = vec.normalized;
                transform.Translate(moveDir * moveSpeed * Time.deltaTime);
            }
        }
        else
        {
            Vector3 vec = player.transform.position - transform.position;
            moveDir = vec.normalized;

            transform.Translate(moveDir * moveSpeed * Time.deltaTime);
        }

    }
}
