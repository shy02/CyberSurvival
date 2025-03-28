using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss4Attack_3 : MonoBehaviour
{
    Transform Target; // 플레이어
    Transform Luncher;
    Transform bulletCage;

    [Header("Pattern 1 : ART REF&Settings")]
    [SerializeField] GameObject Artbullet;
    GameObject RushEffect;

    [Header("Pattern 2 : RushToPlayer REF&Settings")]
    [SerializeField] GameObject RushArea;
    bool CanRushDamage = false;

    [Header("Pattern 3 : CycleAttack REF&Settings")]
    [SerializeField] GameObject CycleBullet;

    [Header("Pattern 4 : CycleAttack REF&Settings")]
    [SerializeField] GameObject Redfield;
    [SerializeField] int bombCount = 5;
    [SerializeField] float bombSetDelay = 0.7f;

    [Header("When Play Down")]
    [SerializeField] GameObject DownEffect;

    private const float rushDistanceDivisor = 7f;
    private const float rushAddIncrement = 0.1f;
    private const float rushWaitTime = 0.05f;
    private const float rushMoveSpeed = 100f;
    private const float rushMoveWaitTime = 0.01f;
    private const float rushMaxTime = 4f;
    private const float cycleAttackRate = 6f;
    private const int cycleBulletCount = 30;
    private const float cycleWaitTime = 1f;
    private const float longLineAttackDuration = 60f;
    private const float longLineAttackSpawnInterval = 0.05f;
    private const float longLineAttackDurationDecrement = 0.5f;

    private void Start()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        Luncher = transform.GetChild(0);
        Target = StageManager_3.instance.player;
        Target = StageManager_3.instance.player;

        RushEffect = transform.GetChild(1).gameObject;
        RushEffect.SetActive(false);

        bulletCage = GetComponent<BossAttackManager_3>().bullet;
    }

    private void Update()
    {
        UpdateLuncherDir();
    }

    private void UpdateLuncherDir()
    {
        Vector3 dirvec = Target.position - Luncher.position;
        Luncher.right = -dirvec.normalized;
    }

    #region skill
    IEnumerator RushToPlayer()
    {
        RushArea.SetActive(true);
        float Distance = Vector3.Distance(Target.position, transform.position); // 플레이어와 보스 사이의 거리

        // 두개다 돌진 범위 껀데 초기값 저장해 놓는 용도 인스펙터에 표시할지는 고민중
        Vector3 nowscale = new Vector3(0, 0.25f, 0.25f);
        Vector3 nowpos = new Vector3(-0.3f, 0.2f, 0);

        GetComponent<EnemyMovement_3>().enabled = false; // 일단 따라가는 거 멈추기
        Stage3SoundManager.instace.StopWalkSound();

        RushArea.transform.localScale = nowscale; // 크기 초기화

        gameObject.GetComponent<Animator>().SetBool("Rush", true);
        Stage3SoundManager.instace.PlayBeforeRush();

        float add = 0; // 범위 표시 어디까지 할까
        RushDirObject area = RushArea.transform.GetChild(0).GetComponent<RushDirObject>();
        area.StartDrawRushArea();
        while (Distance / rushDistanceDivisor >= add)
        {
            if (area.GetComponent<RushDirObject>().Cango)
            {
                add += rushAddIncrement;
                nowscale.x = add;
                RushArea.transform.localScale = nowscale;
                RushArea.transform.localPosition = new Vector3(nowpos.x - add, nowpos.y, nowpos.z);
                yield return new WaitForSeconds(rushWaitTime);
            }
            else
            {
                break;
            }
        } // 돌진 범위 표시

        area.startdraw = false;
        Vector3 Goalpos = RushArea.transform.GetChild(0).position; // 돌진 어디까지 할까요
        RushArea.SetActive(false); // 돌진 범위 끄기
        Vector3 moveGaol = Goalpos - transform.position; // 돌진 위치를 바라보는 벡터

        RushEffect.SetActive(true);
        Stage3SoundManager.instace.PlayRushing();
        // 여기서 데미지 키기
        CanRushDamage = true;

        float Timer = 0f;
        while (Vector3.Distance(transform.position, Goalpos) > 0.5f)
        {
            if (Timer > rushMaxTime) break;
            transform.position = Vector3.MoveTowards(transform.position, Goalpos, rushMoveSpeed * Time.deltaTime);
            yield return new WaitForSeconds(rushMoveWaitTime);
            Timer += rushMoveWaitTime;
        } // 돌진!!!!!!

        Stage3SoundManager.instace.PlayRushBoom();
        gameObject.GetComponent<Animator>().SetBool("Rush", false);
        CanRushDamage = false;
        RushEffect.SetActive(false);

        yield return new WaitForSeconds(1f);
        GetComponent<EnemyMovement_3>().enabled = true; // 1초뒤에 다시 다가가는 함수 실행
    } // 대쉬하는 스킬

    private void OnTriggerStay2D(Collider2D collision) // 러쉬 데미지 주는
    {
        if (collision.CompareTag("Player") && CanRushDamage)
        {
            collision.GetComponent<Player>().TakeDamage(30);
        }
    }

    IEnumerator CycleAttack()
    {
        float attackRate = cycleAttackRate;
        int count = cycleBulletCount;
        float intervalAngle = 360 / count;
        float weightAngle = 0f;

        while (attackRate >= 1)
        {
            Stage3SoundManager.instace.CircleShot();
            for (int i = 0; i < count; i++)
            {
                GameObject clone = Instantiate(CycleBullet, transform.position, Quaternion.identity, bulletCage);
                float angle = (attackRate % 2 == 0) ? weightAngle + intervalAngle * i : weightAngle + intervalAngle * i + 90;
                float x = Mathf.Cos(angle * Mathf.Deg2Rad);
                float y = Mathf.Sin(angle * Mathf.Deg2Rad);
                clone.GetComponent<CycleAttackBullet>().Move(new Vector2(x, y));
            }

            weightAngle += 1;
            attackRate--;
            yield return new WaitForSeconds(cycleWaitTime);
        }
    } // 총알이 원형으로 나가는 스킬

    IEnumerator LockOnShot()
    {
        for (int i = 0; i < bombCount; i++)
        {
            Instantiate(Redfield, Target.position, Quaternion.identity, bulletCage);
            yield return new WaitForSeconds(bombSetDelay);
        }
    } // 플레이어에게 폭탄 떨어뜨리는 스킬

    IEnumerator LongLineAttack()
    {
        float duration = longLineAttackDuration;
        float spawnInterval = longLineAttackSpawnInterval;

        List<(float, float, float, float, bool)> attackPatterns = new List<(float, float, float, float, bool)>
        {
            (1, -1, 0, -10, true),   (1, -1, 0, -10, false),
            (-1, -1, -10, -10, false), (-1, -1, -10, -10, true),
            (-1,  1, -10,  0, true),  (-1,  1, -10,  0, false),
            (-1,  1, -10, -10, false), (-1,  1, -10, -10, true),
            (1,  1,  0, -10, true),   (1,  1,  0, -10, false),
            (1,  1, -10, -10, false), (1,  1, -10, -10, true),
            (1,  1, -10,  0, true),   (1,  1, -10,  0, false),
            (1, -1, -10, -10, false), (1, -1, -10, -10, true),
        };

        Stage3SoundManager.instace.PlayArtShot();
        while (duration > 0)
        {
            foreach (var (Mx, My, UX, UY, flip) in attackPatterns)
            {
                SpawnBullet(Mx, My, UX, UY, flip);
            }

            duration -= longLineAttackDurationDecrement;
            yield return new WaitForSeconds(spawnInterval);
        }
        Stage3SoundManager.instace.StopArtShot();
    } // 얘를 패턴 1로 탕탕탕

    void SpawnBullet(float Mx, float My, float UX, float UY, bool flip)
    {
        var bullet = Instantiate(Artbullet, transform.position, Quaternion.identity, bulletCage)
                     .GetComponent<LongLineAttack>();
        bullet.StartAttack(Mx, My, UX, UY, flip, transform);
    }
    #endregion skill

    public void StopAllPattern()
    {
        StopAllCoroutines();
        if (!GameManager.Instance.nowGameOver)
        {
            Instantiate(DownEffect, transform.position, Quaternion.identity);
            Stage3SoundManager.instace.electiric();
        }
        RushArea.SetActive(false);
        gameObject.GetComponent<Animator>().SetBool("Rush", false);
        RushEffect.SetActive(false);
        Stage3SoundManager.instace.StopArtShot();
    }

    #region ForUI
    public void Pattern1() { StartCoroutine(LongLineAttack()); }
    public void Pattern2() { StartCoroutine(RushToPlayer()); }
    public void Pattern3() { StartCoroutine(CycleAttack()); }
    public void Pattern4() { StartCoroutine(LockOnShot()); }
    #endregion
}