using UnityEngine;

public class stage2_EnemyTank : MonoBehaviour
{
    GameObject player = null;

    Vector3 moveDir = Vector3.zero;
    float distance = 0f;

    void Start()
    {
        if (player == null)
            player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        moveDir = player.transform.position - transform.position;
        distance = moveDir.magnitude;   

        if(distance < 5)
        {

        }
    }



}
