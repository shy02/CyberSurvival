using System.Collections;
using UnityEngine;

public class Boss4Attack : MonoBehaviour
{
    // 패턴1. 8자 세개를 부채모양으로 일정시간동안 발사
    // 패턴2. 플레이어가 멀리 있다면 플레이어와 거리 + 1 만큼 돌진, 경로에 플레이어가 있다면 데미지
    // 패턴3. O형 공격 여러개 발사 런처에서 멀어질 수록 속도가 느려짐 > 피하기 어려움
    // 패턴4. 플레이어의 위치에 빨간 발판이 생기며 일정 시간이후 불기둥이 솓음

    //8자 모양 만드는건 총알 건들고 부채모양 부터

    [SerializeField] GameObject EightBullet1;//8자모양그리면서 날아갈 총알중 하나
    [SerializeField] GameObject EightBullet2;//8자모양그리면서 날아갈 총알중 하나

    Transform Target;
    Transform EightBulletLuncher;


    private void Start()
    {
        EightBulletLuncher = transform.GetChild(0).GetChild(0);//패턴 1 런처
        StartCoroutine(EightBulletPattern());
    }


    IEnumerator EightBulletPattern()
    {
        Target = GetComponent<EnemyMovement>().player;
        Vector3 dir = Target.position - transform.position;
        //일단 E 자모양 으로 가는지 테스트
        Instantiate(EightBullet1, EightBulletLuncher.position, Quaternion.LookRotation(dir));
        yield return new WaitForSeconds(1f);//총알 발사 간격;
    }


}
