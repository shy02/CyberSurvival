using System.Collections;
using UnityEngine;

public class BossPattern_2 : MonoBehaviour
{
    GameObject player;

    //보스 공격력
    public int Damage = 10;

    private LineRenderer lineRenderer;

    [Header("보스 패턴 쿨타임")]
    [SerializeField] private float attackcool1 = 1f;
    [SerializeField] private float attackcool2 = 3f;
    [SerializeField] private float sniperzoom = 2f;
    [SerializeField] private float attackcool3 = 5f;
    [SerializeField] private float attackcool4 = 10f;

    [Header("보스 패턴 참조")]
    [Header("패턴 1 (기본 공격)")]
    [SerializeField] private GameObject bullet;
    public Transform firePoint;
    [Header("패턴 2 (스나이퍼)")]
    [SerializeField] private GameObject sniper;
    [SerializeField] private GameObject sniperEffect;
    [Header("패턴 3 (부메랑)")]
    [SerializeField] private GameObject bumerang;
    [SerializeField] private int bumerangCount = 10;
    [Header("패턴 4 (레이저)")]
    [SerializeField] private GameObject laser;

    //오디오
    public AudioClip[] sounds;  //0번 기본공격, 1번 스나이퍼, 2번 부메랑, 3번 레이저


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
        SoundMgr_2.instance.OneShot(sounds[0],0.5f);    //기본공격
        GameObject go = Instantiate(bullet, firePoint.position, Quaternion.identity);
        BossBullet_2 bossbullet = go.GetComponent<BossBullet_2>();
        Invoke("Attack1", attackcool1);
    }

    void ZoomSniper()   //조즌
    {
        GameObject go = Instantiate(sniper, player.transform.position, Quaternion.identity);
        go.transform.parent = player.transform;

        Destroy(go, sniperzoom);
        Invoke("ShootSniper", sniperzoom);   //3초 후 발사
    }

    void ShootSniper()  //발사
    {
        SoundMgr_2.instance.OneShot(sounds[1],0.5f);    //스나이퍼
        GameObject go = Instantiate(sniperEffect, player.transform.position, Quaternion.identity);
        Destroy(go, 0.25f);

        Invoke("ZoomSniper", attackcool2);  //5초 후 다시 조준
    }

    void Attack3()
    {
        float radius = 5f; // 부메랑이 소환될 반지름 거리
        Vector3 center = transform.position; // 보스의 위치를 중심으로 설정

        for (int i = 0; i < bumerangCount; i++)
        {
            float angle = i * (360f / bumerangCount);   //각각의 각도
            float radian = angle * Mathf.Deg2Rad;   
            Vector3 spawnPosition = transform.position + new Vector3(Mathf.Cos(radian) * radius, Mathf.Sin(radian) * radius, 0);

            GameObject go = Instantiate(bumerang, spawnPosition, Quaternion.identity);
            Bumerang_2 bumerangScript = go.GetComponent<Bumerang_2>();
            bumerangScript.bossTransform = transform; // 보스의 Transform을 설정
            bumerangScript.SetInitialAngle(radian); // 초기 각도를 설정
        }

        SoundMgr_2.instance.OneShot(sounds[2], 0.5f); //부메랑 사운드

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
        SoundMgr_2.instance.OneShot(sounds[3], 0.5f);    //레이저사운드
        //스폰
        GameObject go = Instantiate(laser, startPosition + dir.normalized, Quaternion.identity);
        //발사위치 설정
        go.GetComponent<BossLazer_2>().SetDirection(endPosition, startPosition);
        Destroy(go, 0.3f);

        Invoke("Attack4", attackcool4);
    }


}
