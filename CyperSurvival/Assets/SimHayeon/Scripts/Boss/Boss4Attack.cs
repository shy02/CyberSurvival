using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Boss4Attack : MonoBehaviour
{
    // 패턴1. 8자 세개를 부채모양으로 일정시간동안 발사
    // 패턴2. 플레이어가 멀리 있다면 플레이어와 거리 + 1 만큼 돌진, 경로에 플레이어가 있다면 데미지
    // 패턴3. O형 공격 여러개 발사 런처에서 멀어질 수록 속도가 느려짐 > 피하기 어려움
    // 패턴4. 플레이어의 위치에 빨간 발판이 생기며 일정 시간이후 불기둥이 솓음

    Transform Target;//플레이어
    Transform Luncher;
    float rotateSpeed = 1f;
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
    #endregion

    private void Start()
    {
        Luncher = transform.GetChild(0);
        Target = GetComponent<EnemyMovement>().player;

        EightBulletLuncher = transform.GetChild(0).GetChild(0);//패턴 1 런처

        StartCoroutine(CycleAttack());
    }

    private void Update()
    {
        Vector3 dirvec = Target.position - Luncher.position;

        Luncher.right = -dirvec.normalized;

    }

    //패턴1
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
    }
    //패턴2
    IEnumerator RushToPlayer()
    {

        RushArea.SetActive(true);
        float Distance = Vector3.Distance(Target.position,transform.position);//플레이어와 보스 사이의 거리
        Vector3 nowscale = new Vector3(0,0.25f,0.25f);
        Vector3 nowpos = new Vector3(-0.3f, 0.2f,0);
        GetComponent<EnemyMovement>().enabled = false;

        RushArea.transform.localScale = nowscale;
        Debug.Log(Distance);

        float add = 0;
        while(Distance/7 >= add)
        {
            add += 0.1f;
            nowscale.x = add;
            RushArea.transform.localScale = nowscale;
            RushArea.transform.localPosition = new Vector3(nowpos.x - add, nowpos.y, nowpos.z);
            yield return new WaitForSeconds(0.01f);
        }//돌진 범위 표시
        Vector3 Goalpos = RushArea.transform.GetChild(0).position;
        RushArea.SetActive(false);
        Vector3 moveGaol = Goalpos - transform.position;

        while (Vector3.Distance(transform.position, Goalpos) > 0.5f)
        {
            Debug.Log("실행");
            //transform.Translate(moveGaol.normalized * 300 *Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, Goalpos, 300 * Time.deltaTime);
            yield return new WaitForSeconds(0.01f);
            

        }

        yield return new WaitForSeconds(1f);
        GetComponent<EnemyMovement>().enabled = true;
    }
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
    }
    IEnumerator LockOnShot()
    {

        //플레이어 밑에 빨간 발판 소환
        Instantiate(Redfield, Target.position, Quaternion.identity);
        //몇번 반복
        yield return new WaitForSeconds(1f);
    }

    #region ForUI
    public void Pattern1() { StartCoroutine(EightBulletPattern()); }
    public void Pattern2() { StartCoroutine(RushToPlayer()); }
    public void Pattern3() { StartCoroutine(CycleAttack()); }
    public void Pattern4() { StartCoroutine(CycleAttack()); }
    #endregion
}
