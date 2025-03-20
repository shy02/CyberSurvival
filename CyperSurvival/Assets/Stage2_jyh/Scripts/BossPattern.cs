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

    //���� ���ݷ�
    public int Damage = 10;

    //����1 �⺻ �Ѿ� �߻�
    public Transform shootPos;
    public GameObject bullet;
    float attackcool1 = 1;


    //����2 �������� Ÿ�ֿ̹� ���缭 ������� ���ؾ���
    public GameObject sniper;
    public GameObject sniperEffect;
    float attackcool2 = 5;

    //����3 ȸ�� �θ޶�
    public GameObject bumerang;
    float attackcool3 = 10;
    int bucount = 15;


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
        for(int i = 0; i < bucount; i++)
        {
            GameObject go = Instantiate(bumerang, transform.position, Quaternion.Euler(0, i * (360 / bucount), 0));
            //GameObject go = Instantiate(bumerang, transform.position, Quaternion.identity);
        }

        Invoke("Attack3", attackcool3);
    }

}
