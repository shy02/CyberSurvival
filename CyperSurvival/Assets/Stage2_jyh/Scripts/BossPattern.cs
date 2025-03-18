using System.Collections;
using UnityEngine;

public class BossPattern : MonoBehaviour
{
    public enum BossState
    {
        move,
        attacking,
        grogy,
        death
    }

    BossState bossState = BossState.move; 

    GameObject player;
    float distance = 0;

    //패턴1 스나이퍼 타이밍에 맞춰서 구르기로 피해야함
    public GameObject sniper;
    float attack1cool = 5;

    //패턴2



    void Start()
    {
        player = GameObject.FindWithTag("Player");

        Invoke("Attack1", attack1cool);
    }

    void Update()
    {
        distance = (player.transform.position - transform.position).magnitude;

        if (distance > 6)
        {
            bossState = BossState.move;
            return;
        }


    }

    void Attack1()
    {
        GameObject go = Instantiate(sniper, player.transform.position, Quaternion.identity);
        go.transform.parent = player.transform;

        Destroy(go, 3);
        Invoke("TakeDamage", 3);
    }

}
