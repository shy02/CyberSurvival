using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss4Attack_3 : MonoBehaviour
{
    // 패턴1. 8자 세개를 부채모양으로 일정시간동안 발사
    // 패턴2. 플레이어가 멀리 있다면 플레이어와 거리 + 1 만큼 돌진, 경로에 플레이어가 있다면 데미지
    // 패턴3. O형 공격 여러개 발사 런처에서 멀어질 수록 속도가 느려짐 > 피하기 어려움
    // 패턴4. 플레이어의 위치에 빨간 발판이 생기며 일정 시간이후 불기둥이 솓음

    Transform Target;//플레이어
    Transform Luncher;
    /*float rotateSpeed = 1f;
    #region 패턴1
    //8자 모양 만드는건 총알 건들고 부채모양 부터
    [Header("Pattern 1 : 8 REF & Settings")]
    [SerializeField] GameObject EightBullet1;//8자모양그리면서 날아갈 총알중 하나
    [SerializeField] GameObject EightBullet2;//8자모양그리면서 날아갈 총알중 하나
    [Tooltip("어느정도 간격으로 기울어 질 것인가")]
    [SerializeField] float angle;
    [Tooltip("한쪽 방향으로 몇개를 쏠것인가 (단, 위아래 대칭이기 때문에 2 = 4 임을 명심)")]
    [SerializeField] int Count8;
    [Tooltip("몇초동안 쏠것인가")]
    [SerializeField] float MaxTime8;
    [Tooltip("8자 패턴 발사 간격")]
    [SerializeField] float Delay8;

    Transform EightBulletLuncher;
    #endregion
    */

    [Header("Pattern 1 : ART REF&Settings")]
    [SerializeField] GameObject Artbullet;
    GameObject RushEffect;
    #region 패턴2
    [Header("Pattern 2 : RushToPlayer REF&Settings")]
    [SerializeField] GameObject RushArea;
    #endregion
    #region 패턴3
    [Header("Pattern 3 : CycleAttack REF&Settings")]
    [SerializeField] GameObject CycleBullet;
    #endregion
    #region 패턴4
    [Header("Pattern 4 : CycleAttack REF&Settings")]
    [SerializeField] GameObject Redfield;
    [SerializeField] int bombCount = 5;
    [SerializeField] float bombSetDelay = 0.7f;
    #endregion

    private void Start()
    {
        Luncher = transform.GetChild(0);
        Target = StageManager_3.instance.player;
        if (Target == null)
        {
            while (Target != null)
            {
                Target = StageManager_3.instance.player;
            }
        }

        RushEffect = transform.GetChild(1).gameObject;
        RushEffect.SetActive(false);

        //EightBulletLuncher = transform.GetChild(0).GetChild(0);//패턴 1 런처
    }

    private void Update()
    {
        Vector3 dirvec = Target.position - Luncher.position;

        Luncher.right = -dirvec.normalized;

    }


    /*//패턴1
    IEnumerator EightBulletPattern()
    {
        Vector3 dir = Target.position - transform.position;
        float Timer = 0;
        // /모양

        //일자모양
        while (Timer < MaxTime8) {
            Instantiate(EightBullet1, EightBulletLuncher.position, Quaternion.identity);
            Instantiate(EightBullet2, EightBulletLuncher.position, Quaternion.identity);
            yield return new WaitForSeconds(Delay8);//총알 발사 간격;
            Timer += Delay8;
        }
    }*/
    //패턴2
    IEnumerator RushToPlayer()
    {

        RushArea.SetActive(true);
        float Distance = Vector3.Distance(Target.position,transform.position);//플레이어와 보스 사이의 거리

        //두개다 돌진 범위 껀데 초기값 저장해 놓는 용도 인스펙터에 표시할지는 고민중
        Vector3 nowscale = new Vector3(0,0.25f,0.25f);
        Vector3 nowpos = new Vector3(-0.3f, 0.2f,0);

        GetComponent<EnemyMovement_3>().enabled = false;//일단 따라가는 거 멈추기
        Stage3SoundManager.instace.StopWalkSound();

        RushArea.transform.localScale = nowscale;//크기 초기화

        gameObject.GetComponent<Animator>().SetBool("Rush", true);
        Stage3SoundManager.instace.PlayBeforeRush();
        float add = 0;//범위 표시 어디까지 할까
        while(Distance/7 >= add)
        {
            add += 0.1f;
            nowscale.x = add;
            RushArea.transform.localScale = nowscale;
            RushArea.transform.localPosition = new Vector3(nowpos.x - add, nowpos.y, nowpos.z);
            yield return new WaitForSeconds(0.05f);
        }//돌진 범위 표시

        Vector3 Goalpos = RushArea.transform.GetChild(0).position;//돌진 어디까지 할까요

        RushArea.SetActive(false);//돌진 범위 끄기

        Vector3 moveGaol = Goalpos - transform.position;//돌진 위치를 바라보는 벡터

        RushEffect.SetActive(true);
        Stage3SoundManager.instace.PlayRushing();
        while (Vector3.Distance(transform.position, Goalpos) > 0.5f)
        {
            Debug.Log("실행");
            //transform.Translate(moveGaol.normalized * 300 *Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, Goalpos, 100 * Time.deltaTime);
            yield return new WaitForSeconds(0.01f);
            
        } //돌진!!!!!!
        Stage3SoundManager.instace.PlayRushBoom();
        gameObject.GetComponent<Animator>().SetBool("Rush", false);

        RushEffect.SetActive(false);

        yield return new WaitForSeconds(1f);
        GetComponent<EnemyMovement_3>().enabled = true;//1초뒤에 다시 다가가는 함수 실행
    }//완료
    //패턴3
    IEnumerator CycleAttack()
    {
        float attackRate = 6;

        int count = 30; //생성갯수

        float intervalAngle = 360 / count;
        float weightAngle = 0f;

        while (attackRate >= 1)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject clone = Instantiate(CycleBullet, transform.position, Quaternion.identity);
                float angle = (attackRate%2 == 0) ? weightAngle + intervalAngle * i : weightAngle + intervalAngle * i + 90;

                float x = Mathf.Cos(angle * Mathf.Deg2Rad);

                float y = Mathf.Sin(angle * Mathf.Deg2Rad);

                clone.GetComponent<CycleAttackBullet>().Move(new Vector2(x, y));
            }

            weightAngle += 1;
            attackRate--;
            yield return new WaitForSeconds(1f);
        }
    }//완료
    IEnumerator LockOnShot()
    {
        for (int i = 0; i < bombCount; i++)
        {
            //플레이어 밑에 빨간 발판 소환
            Instantiate(Redfield, Target.position, Quaternion.identity);
            //몇번 반복
            yield return new WaitForSeconds(bombSetDelay);
        }
    }//완료

    #region 패턴1 대타
    IEnumerator LongLineAttack()
    {
        float duration = 60f; // 60초 동안 실행
        float spawnInterval = 0.05f; // 탄환 생성 간격

        // 공격 패턴 정의 (Mx, My, UX, UY, flip)
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

        while (duration > 0)
        {
            foreach (var (Mx, My, UX, UY, flip) in attackPatterns)
            {
                SpawnBullet(Mx, My, UX, UY, flip);
            }

            duration -= 0.5f;
            yield return new WaitForSeconds(spawnInterval);
        }
    /*float sencond = 60f;//30초
    while(sencond > 0){
        Instantiate(testbullet, transform.position, Quaternion.identity).GetComponent<LongLineAttack>().StartAttack(1, -1, 0, -10, true, transform);
        Instantiate(testbullet, transform.position, Quaternion.identity).GetComponent<LongLineAttack>().StartAttack(1, -1, 0, -10, false, transform);

        Instantiate(testbullet, transform.position, Quaternion.identity).GetComponent<LongLineAttack>().StartAttack(-1, -1, -10, -10, false, transform);
        Instantiate(testbullet, transform.position, Quaternion.identity).GetComponent<LongLineAttack>().StartAttack(-1, -1, -10, -10, true, transform);

        Instantiate(testbullet, transform.position, Quaternion.identity).GetComponent<LongLineAttack>().StartAttack(-1, 1, -10, 0, true, transform);
        Instantiate(testbullet, transform.position, Quaternion.identity).GetComponent<LongLineAttack>().StartAttack(-1, 1, -10, 0, false, transform);

        Instantiate(testbullet, transform.position, Quaternion.identity).GetComponent<LongLineAttack>().StartAttack(-1, 1, -10, -10, false, transform);
        Instantiate(testbullet, transform.position, Quaternion.identity).GetComponent<LongLineAttack>().StartAttack(-1, 1, -10, -10, true, transform);

        Instantiate(testbullet, transform.position, Quaternion.identity).GetComponent<LongLineAttack>().StartAttack(1, 1, 0, -10, true, transform);
        Instantiate(testbullet, transform.position, Quaternion.identity).GetComponent<LongLineAttack>().StartAttack(1, 1, 0, -10, false, transform);

        Instantiate(testbullet, transform.position, Quaternion.identity).GetComponent<LongLineAttack>().StartAttack(1, 1, -10, -10, false, transform);
        Instantiate(testbullet, transform.position, Quaternion.identity).GetComponent<LongLineAttack>().StartAttack(1, 1, -10, -10, true, transform);

        Instantiate(testbullet, transform.position, Quaternion.identity).GetComponent<LongLineAttack>().StartAttack(1, 1, -10, 0, true, transform);
        Instantiate(testbullet, transform.position, Quaternion.identity).GetComponent<LongLineAttack>().StartAttack(1, 1, -10, 0, false, transform);

        Instantiate(testbullet, transform.position, Quaternion.identity).GetComponent<LongLineAttack>().StartAttack(1, -1, -10, -10, false, transform);
        Instantiate(testbullet, transform.position, Quaternion.identity).GetComponent<LongLineAttack>().StartAttack(1, -1, -10, -10, true, transform);

        sencond -= 0.5f;
        yield return new WaitForSeconds(0.05f);
    }*/
    } // 얘를 패턴 1로 탕탕탕
    void SpawnBullet(float Mx, float My, float UX, float UY, bool flip)
    {
        var bullet = Instantiate(Artbullet, transform.position, Quaternion.identity)
                     .GetComponent<LongLineAttack>();
        bullet.StartAttack(Mx, My, UX, UY, flip, transform);
    }
    //한개 더 추가할지도?
    #endregion // 완료



    #region ForUI
    public void Pattern1() { StartCoroutine(LongLineAttack());}
    public void Pattern2() { StartCoroutine(RushToPlayer()); }
    public void Pattern3() { StartCoroutine(CycleAttack()); }
    public void Pattern4() { StartCoroutine(LockOnShot()); }
    #endregion


}
