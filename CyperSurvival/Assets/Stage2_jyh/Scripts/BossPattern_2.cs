using System.Collections;
using UnityEngine;

public class BossPattern_2 : MonoBehaviour
{
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
    float attackcool3 = 7;
    int bucount = 10;

    //패턴4 레이저 발사
    public GameObject laser;
    float attackcool4 = 3;
    private LineRenderer lineRenderer;

    //오디오
    public AudioClip[] sounds;  //0번 기본공격, 1번 스나이퍼, 2번 레이저


    void Start()
    {
        player = GameObject.FindWithTag("Player");

        LineRendererSet();

        StartAttack();
    }

    void Update()
    {

    }

    void StartAttack()
    {
        Invoke("Attack1", attackcool1);
        Invoke("ZoomSniper", attackcool2);
        Invoke("Attack3", attackcool3);
        Invoke("Attack4", attackcool4);
    }

    void Attack1()
    {
        SoundMgr_2.instance.OneShot(sounds[0]);    //기본공격
        GameObject go = Instantiate(bullet, shootPos.position, Quaternion.identity);
        BossBullet_2 bossbullet = go.GetComponent<BossBullet_2>();
        bossbullet.SetDamage(Damage);
        Invoke("Attack1", attackcool1);
    }

    void ZoomSniper()   //조즌
    {
        GameObject go = Instantiate(sniper, player.transform.position, Quaternion.identity);
        go.transform.parent = player.transform;

        Destroy(go, 3);
        Invoke("ShootSniper", 3);   //3초 후 발사
    }

    void ShootSniper()  //발사
    {
        SoundMgr_2.instance.OneShot(sounds[1]);    //스나이퍼
        GameObject go = Instantiate(sniperEffect, player.transform.position, Quaternion.identity);
        Destroy(go, 0.25f);

        player.GetComponent<Player_2>().GetDamage(10);
        Invoke("ZoomSniper", attackcool2);  //5초 후 다시 조준
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
            Bumerang_2 bumerangScript = go.GetComponent<Bumerang_2>();
            bumerangScript.bossTransform = transform; // 보스의 Transform을 설정
            bumerangScript.SetInitialAngle(radian); // 초기 각도를 설정
        }

        Invoke("Attack3", attackcool3);
    }

    void Attack4()
    {
        StartCoroutine(ShowLaserWarning());
    }

    void LineRendererSet()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 2;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.enabled = false;
    }

    IEnumerator ShowLaserWarning()
    {
        float blinkDuration = 1f;
        float blinkInterval = 0.2f; //3번째 빨간색 돌고 바로 레이저 발사
        float elapsedTime = 0f;
        Vector3 startPosition = Vector3.zero;   //보스위치
        Vector3 endPosition = Vector3.zero;     //플레이어위치

        while (elapsedTime < blinkDuration)
        {
            startPosition = transform.position;
            endPosition = player.transform.position;

            lineRenderer.SetPosition(0, startPosition); //시작 지점
            lineRenderer.SetPosition(1, endPosition);   //끝 지점
            lineRenderer.enabled = !lineRenderer.enabled;   //On/Off

            yield return new WaitForSeconds(blinkInterval);
            elapsedTime += blinkInterval;
        }

        lineRenderer.enabled = false;   //다시 끄기

        
        Vector3 dir = endPosition - startPosition;

        //오디오 스폰
        SoundMgr_2.instance.OneShot(sounds[2]);    //레이저
        //스폰
        GameObject go = Instantiate(laser, startPosition + dir.normalized, Quaternion.identity);
        //발사위치 설정
        go.GetComponent<BossLazer_2>().SetDirection(endPosition, startPosition);
        Destroy(go, 0.3f);

        Invoke("Attack4", attackcool4);
    }


}
