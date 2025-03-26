using System.Collections;
using UnityEngine;

public class BossPattern_2 : MonoBehaviour
{
    GameObject player;

    //����1 �⺻ �Ѿ� �߻�
    


    //����2 �������� Ÿ�ֿ̹� ���缭 ������� ���ؾ���
    


    //����3 ȸ�� �θ޶�
    

    int bucount = 10;

    //����4 ������ �߻�
    

    private LineRenderer lineRenderer;

    [Header("���� �߻� ������Ʈ")]
    public GameObject fireBall;
    public GameObject sniper;
    public GameObject sniperEffect;
    public GameObject bumerang;
    public GameObject thunder;

    [Header("���� ��Ÿ��")]
    [SerializeField] private float attackcool1 = 1;
    [SerializeField] private float attackcool2 = 1;
    [SerializeField] private float attackcool3 = 1;
    [SerializeField] private float attackcool4 = 1;

    //���� ���� �ִϸ��̼�
    Animator anim;

    //�����
    public AudioClip[] sounds;  //0�� �⺻����, 1�� ��������, 2�� ������


    void Start()
    {
        player = GameObject.FindWithTag("Player");

        anim = GetComponent<Animator>();

        LineRendererSet();

        StartAttack();
    }

    void Update()
    {

    }

    void AttackAnim()
    {
        anim.SetTrigger("Attack");
    }

    void StartAttack()
    {
        //Invoke("fireBall", attackcool1);
        //Invoke("ZoomSniper", attackcool2);
        //Invoke("Attack3", attackcool3);
        Invoke("Thunder", attackcool4);
    }

    void FireBall()
    {
        SoundMgr_2.instance.OneShot(sounds[0]);    //�⺻����
        GameObject go = Instantiate(fireBall, transform.position, Quaternion.identity);
        FireBall_2 fireball = go.GetComponent<FireBall_2>();
        AttackAnim();
        Invoke("fireBall", attackcool1);
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
        AttackAnim();
        Invoke("Attack3", attackcool3);
    }

    void Thunder()
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
        SoundMgr_2.instance.OneShot(sounds[2]);    //Thunder
        //����
        GameObject go = Instantiate(thunder, startPosition + dir.normalized, Quaternion.identity);
        //�߻���ġ ����
        go.GetComponent<Thunder_2>().SetDirection(endPosition, startPosition);
        Destroy(go, 0.3f);

        Invoke("Thunder", attackcool4);
    }


}
