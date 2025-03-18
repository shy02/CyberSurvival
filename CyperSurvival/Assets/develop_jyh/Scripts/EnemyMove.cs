using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    GameObject player = null;

    //¿Ãµø
    public float moveSpeed = 1;
    Vector3 moveDir = Vector3.zero;
    

    void Start()
    {
        if (player == null)
            player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        Vector3 vec = player.transform.position - transform.position;
        moveDir = vec.normalized;

        transform.Translate(moveDir * moveSpeed * Time.deltaTime);
    }
}
