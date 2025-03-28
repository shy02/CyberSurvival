using System.Collections;
using UnityEngine;

public class BossPattern_2 : MonoBehaviour
{
    GameObject player;

    //���� ���ݷ�
    public int Damage = 10;

    private LineRenderer lineRenderer;

    [Header("���� ���� ��Ÿ��")]
    [SerializeField] private float attackcool1 = 1f;
    [SerializeField] private float attackcool2 = 3f;
    [SerializeField] private float sniperzoom = 2f;
    [SerializeField] private float attackcool3 = 5f;
    [SerializeField] private float attackcool4 = 10f;

    [Header("���� ���� ����")]
    [Header("���� 1 (�⺻ ����)")]
    [SerializeField] private GameObject bullet;
    public Transform firePoint;
    [Header("���� 2 (��������)")]
    [SerializeField] private GameObject sniper;
    [SerializeField] private GameObject sniperEffect;
    [Header("���� 3 (�θ޶�)")]
    [SerializeField] private GameObject bumerang;
    [SerializeField] private int bumerangCount = 10;
    [Header("���� 4 (������)")]
    [SerializeField] private GameObject laser;

    //�����
    public AudioClip[] sounds;  //0�� �⺻����, 1�� ��������, 2�� �θ޶�, 3�� ������


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
        SoundMgr_2.instance.OneShot(sounds[0],0.5f);    //�⺻����
        GameObject go = Instantiate(bullet, firePoint.position, Quaternion.identity);
        BossBullet_2 bossbullet = go.GetComponent<BossBullet_2>();
        Invoke("Attack1", attackcool1);
    }

    void ZoomSniper()   //����
    {
        GameObject go = Instantiate(sniper, player.transform.position, Quaternion.identity);
        go.transform.parent = player.transform;

        Destroy(go, sniperzoom);
        Invoke("ShootSniper", sniperzoom);   //3�� �� �߻�
    }

    void ShootSniper()  //�߻�
    {
        SoundMgr_2.instance.OneShot(sounds[1],0.5f);    //��������
        GameObject go = Instantiate(sniperEffect, player.transform.position, Quaternion.identity);
        Destroy(go, 0.25f);

        Invoke("ZoomSniper", attackcool2);  //5�� �� �ٽ� ����
    }

    void Attack3()
    {
        float radius = 5f; // �θ޶��� ��ȯ�� ������ �Ÿ�
        Vector3 center = transform.position; // ������ ��ġ�� �߽����� ����

        for (int i = 0; i < bumerangCount; i++)
        {
            float angle = i * (360f / bumerangCount);   //������ ����
            float radian = angle * Mathf.Deg2Rad;   
            Vector3 spawnPosition = transform.position + new Vector3(Mathf.Cos(radian) * radius, Mathf.Sin(radian) * radius, 0);

            GameObject go = Instantiate(bumerang, spawnPosition, Quaternion.identity);
            Bumerang_2 bumerangScript = go.GetComponent<Bumerang_2>();
            bumerangScript.bossTransform = transform; // ������ Transform�� ����
            bumerangScript.SetInitialAngle(radian); // �ʱ� ������ ����
        }

        SoundMgr_2.instance.OneShot(sounds[2], 0.5f); //�θ޶� ����

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
        float blinkInterval = 0.2f; //3��° ������ ���� �ٷ� ������ �߻�
        float elapsedTime = 0f;
        Vector3 startPosition = Vector3.zero;   //������ġ
        Vector3 endPosition = Vector3.zero;     //�÷��̾���ġ

        while (elapsedTime < blinkDuration)
        {
            startPosition = transform.position;
            endPosition = player.transform.position;

            lineRenderer.SetPosition(0, startPosition); //���� ����
            lineRenderer.SetPosition(1, endPosition);   //�� ����
            lineRenderer.enabled = !lineRenderer.enabled;   //On/Off

            yield return new WaitForSeconds(blinkInterval);
            elapsedTime += blinkInterval;
        }

        lineRenderer.enabled = false;   //�ٽ� ����

        
        Vector3 dir = endPosition - startPosition;

        //����� ����
        SoundMgr_2.instance.OneShot(sounds[3], 0.5f);    //����������
        //����
        GameObject go = Instantiate(laser, startPosition + dir.normalized, Quaternion.identity);
        //�߻���ġ ����
        go.GetComponent<BossLazer_2>().SetDirection(endPosition, startPosition);
        Destroy(go, 0.3f);

        Invoke("Attack4", attackcool4);
    }


}
