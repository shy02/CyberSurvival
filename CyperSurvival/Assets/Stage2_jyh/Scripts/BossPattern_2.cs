using System.Collections;
using UnityEngine;

public class BossPattern_2 : MonoBehaviour
{
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
    float attackcool3 = 7;
    int bucount = 10;

    //����4 ������ �߻�
    public GameObject laser;
    float attackcool4 = 3;
    private LineRenderer lineRenderer;

    //�����
    public AudioClip[] sounds;  //0�� �⺻����, 1�� ��������, 2�� ������


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
        SoundMgr_2.instance.OneShot(sounds[0]);    //�⺻����
        GameObject go = Instantiate(bullet, shootPos.position, Quaternion.identity);
        BossBullet_2 bossbullet = go.GetComponent<BossBullet_2>();
        bossbullet.SetDamage(Damage);
        Invoke("Attack1", attackcool1);
    }

    void ZoomSniper()   //����
    {
        GameObject go = Instantiate(sniper, player.transform.position, Quaternion.identity);
        go.transform.parent = player.transform;

        Destroy(go, 3);
        Invoke("ShootSniper", 3);   //3�� �� �߻�
    }

    void ShootSniper()  //�߻�
    {
        SoundMgr_2.instance.OneShot(sounds[1]);    //��������
        GameObject go = Instantiate(sniperEffect, player.transform.position, Quaternion.identity);
        Destroy(go, 0.25f);

        player.GetComponent<Player_2>().GetDamage(10);
        Invoke("ZoomSniper", attackcool2);  //5�� �� �ٽ� ����
    }

    void Attack3()
    {
        float radius = 5f; // �θ޶��� ��ȯ�� ������ �Ÿ�
        Vector3 center = transform.position; // ������ ��ġ�� �߽����� ����

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
            bumerangScript.bossTransform = transform; // ������ Transform�� ����
            bumerangScript.SetInitialAngle(radian); // �ʱ� ������ ����
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
        SoundMgr_2.instance.OneShot(sounds[2]);    //������
        //����
        GameObject go = Instantiate(laser, startPosition + dir.normalized, Quaternion.identity);
        //�߻���ġ ����
        go.GetComponent<BossLazer_2>().SetDirection(endPosition, startPosition);
        Destroy(go, 0.3f);

        Invoke("Attack4", attackcool4);
    }


}
