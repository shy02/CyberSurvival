using System.Collections;
using UnityEditor;
using UnityEngine;

public class BossAttackManager_3 : MonoBehaviour
{
    [SerializeField] float AttackDelay = 3f;
    [SerializeField] float DistanceToPlayer = 5f;

    Boss4Attack_3 bossAttack;
    public Transform Player = null;

    private void Start()
    {
        GetComponent<EnemyMovement_3>().enabled = false;
        GetComponent<Enemy2_Shot_3>().enabled = false;
        bossAttack = GetComponent<Boss4Attack_3>();
        Player = StageManager_3.instance.player;
        if(Player == null)
        {
            while(Player != null)
            {
                Player = StageManager_3.instance.player;
            }
        }
    }

    public void StartAttack()
    {
        GetComponent<EnemyMovement_3>().enabled = true;
        GetComponent<Enemy2_Shot_3>().enabled = true;
        StartCoroutine(UseBossSkill());
    }

    IEnumerator UseBossSkill()
    {
        while (true)
        {
            int index = Random.Range(0, 3);

            switch (index)
            {
                case 0://1번 패턴
                    bossAttack.Pattern1();
                    break;
                case 1://3번 패턴
                    bossAttack.Pattern3();
                    break;
                case 2://4번 패턴
                    bossAttack.Pattern4();
                    break;
            }
            float dis = (Player.position - transform.position).sqrMagnitude; // 거리^2
            if (dis > DistanceToPlayer * DistanceToPlayer)
            {
                bossAttack.Pattern2();
            }
            yield return new WaitForSeconds(AttackDelay);
        }

    }
}
