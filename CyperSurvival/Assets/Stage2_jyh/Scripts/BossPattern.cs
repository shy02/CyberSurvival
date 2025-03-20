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

    //보스 공격력
    public int Damage = 10;

    //패턴1 기본 총알 발사
    public Transform shootPos;
    public GameObject bullet;
    float attackcool1 = 1;


    //패턴2 스나이퍼 타이밍에 맞춰서 구르기로 피해야함
    public GameObject sniper;
    public GameObject sniperEffect;
    float attackcool2 = 5;

    //패턴3 회전 부메랑
    public GameObject bumerang;
    float attackcool3 = 10;
    int bucount = 10;


    void Start()
    {
        player = GameObject.FindWithTag("Player");

        Invoke("Attack1", attackcool1);

        Invoke("Attack2", attackcool2);

        Invoke("Attack3", attackcool3);
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
        GameObject go = Instantiate(bullet, shootPos.position, Quaternion.identity);
        BossBullet bossbullet = go.GetComponent<BossBullet>();
        bossbullet.SetDamage(Damage);
        Invoke("Attack1", attackcool1);
    }

    void Attack2()
    {
        GameObject go = Instantiate(sniper, player.transform.position, Quaternion.identity);
        go.transform.parent = player.transform;

        Destroy(go, 3);
        Invoke("ShootSniper", 3);
    }

    void ShootSniper()
    {
        GameObject go = Instantiate(sniperEffect, player.transform.position, Quaternion.identity);
        Destroy(go, 0.25f);

        Player playerscirpt = player.GetComponent<Player>();

        playerscirpt.GetDamage(10);

        Invoke("Attack2", attackcool2);
    }

    void Attack3()
    {
        float radius = 5f; // 부메랑이 소환될 반지름 거리
        Vector3 center = transform.position; // 보스의 위치를 중심으로 설정

        for (int i = 0; i < bucount; i++)
        {
            float angle = i * (360f / bucount);
            float radian = angle * Mathf.Deg2Rad;
            Vector3 spawnPosition = new Vector3(
                center.x + radius * Mathf.Cos(radian),
                center.y + radius * Mathf.Sin(radian),
                center.z
            );

            GameObject go = Instantiate(bumerang, spawnPosition, Quaternion.identity);
            Bumerang bumerangScript = go.GetComponent<Bumerang>();
            bumerangScript.bossTransform = transform; // 보스의 Transform을 설정
            bumerangScript.SetInitialAngle(radian); // 초기 각도를 설정
        }

        Invoke("Attack3", attackcool3);
    }

}
