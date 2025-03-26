using System.Collections;
using UnityEditor;
using UnityEngine;

public class BossAttackManager_3 : MonoBehaviour
{
    [SerializeField] float AttackDelay = 3f;
    [SerializeField] float DistanceToPlayer = 5f;
    [SerializeField] float DownTime = 100f;//기절해 있는 시간
    [SerializeField] Transform cores;
    public Transform bullet;

    Boss4Attack_3 bossAttack;
    public Transform Player = null;

    bool CanAttack = false;

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

    private void Update()
    {
        int count = 0;
        for (int i = 0; i < cores.childCount; i++)
        {
            if (!cores.GetChild(i).gameObject.activeSelf)
            {
                count++;
            }
        }

        if (count == cores.childCount && CanAttack)
        {
            StopAttack();
        }

        if (GameManager.Instance.nowGameOver || GameManager.Instance.nowNextStage)
        {
            StopAttack();
        }
    }

    public void StartAttack()
    {
        CanAttack = true;
        cores.gameObject.SetActive(true);
        ActiveCores();
        GetComponent<EnemyMovement_3>().enabled = true;
        GetComponent<Enemy2_Shot_3>().enabled = true;
        GetComponent<Boss4Attack_3>().enabled = true;
        StartCoroutine(UseBossSkill());
    }

    private void StopAttack()
    {
        CanAttack = false;
        GetComponent<EnemyMovement_3>().enabled = false;
        GetComponent<Enemy2_Shot_3>().enabled = false;
        GetComponent<Boss4Attack_3>().StopAllPattern();
        GetComponent<Boss4Attack_3>().enabled = false;
        StopAllCoroutines();

        foreach (Transform child in bullet)
        {
            Destroy(child.gameObject);
        }

        Invoke("StartAttack", DownTime);
    }

    private void ActiveCores()
    {
        for(int i = 0; i < cores.childCount; i++)
        {
            cores.GetChild(i).gameObject.SetActive(true);
            cores.GetChild(i).GetComponent<Boss3Core>().resetCore();
        }
    }

    IEnumerator UseBossSkill()
    {
        while (CanAttack && !GameManager.Instance.nowGameOver && !GameManager.Instance.nowNextStage)
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
            if (CanAttack) yield return new WaitForSeconds(AttackDelay);
            else break;
        }

    }
}
