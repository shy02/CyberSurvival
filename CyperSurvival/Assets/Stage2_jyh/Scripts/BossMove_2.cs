using UnityEngine;

public class BossMove_2 : MonoBehaviour
{
    GameObject player = null;
    SpriteRenderer spriteRenderer = null;
    //¿Ãµø
    [SerializeField] private float moveSpeed = 1;
    Vector3 moveDir = Vector3.zero;


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

        if (vec.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        transform.Translate(moveDir * moveSpeed * Time.deltaTime);
    }
}
