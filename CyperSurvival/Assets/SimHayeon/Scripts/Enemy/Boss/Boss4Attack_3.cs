using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss4Attack_3 : MonoBehaviour
{
    // ����1. 8�� ������ ��ä������� �����ð����� �߻�
    // ����2. �÷��̾ �ָ� �ִٸ� �÷��̾�� �Ÿ� + 1 ��ŭ ����, ��ο� �÷��̾ �ִٸ� ������
    // ����3. O�� ���� ������ �߻� ��ó���� �־��� ���� �ӵ��� ������ > ���ϱ� �����
    // ����4. �÷��̾��� ��ġ�� ���� ������ ����� ���� �ð����� �ұ���� ����

    Transform Target;//�÷��̾�
    Transform Luncher;
    /*float rotateSpeed = 1f;
    #region ����1
    //8�� ��� ����°� �Ѿ� �ǵ�� ��ä��� ����
    [Header("Pattern 1 : 8 REF & Settings")]
    [SerializeField] GameObject EightBullet1;//8�ڸ��׸��鼭 ���ư� �Ѿ��� �ϳ�
    [SerializeField] GameObject EightBullet2;//8�ڸ��׸��鼭 ���ư� �Ѿ��� �ϳ�
    [Tooltip("������� �������� ���� �� ���ΰ�")]
    [SerializeField] float angle;
    [Tooltip("���� �������� ��� ����ΰ� (��, ���Ʒ� ��Ī�̱� ������ 2 = 4 ���� ���)")]
    [SerializeField] int Count8;
    [Tooltip("���ʵ��� ����ΰ�")]
    [SerializeField] float MaxTime8;
    [Tooltip("8�� ���� �߻� ����")]
    [SerializeField] float Delay8;

    Transform EightBulletLuncher;
    #endregion
    */

    [Header("Pattern 1 : ART REF&Settings")]
    [SerializeField] GameObject Artbullet;
    GameObject RushEffect;
    #region ����2
    [Header("Pattern 2 : RushToPlayer REF&Settings")]
    [SerializeField] GameObject RushArea;
    #endregion
    #region ����3
    [Header("Pattern 3 : CycleAttack REF&Settings")]
    [SerializeField] GameObject CycleBullet;
    #endregion
    #region ����4
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

        //EightBulletLuncher = transform.GetChild(0).GetChild(0);//���� 1 ��ó
    }

    private void Update()
    {
        Vector3 dirvec = Target.position - Luncher.position;

        Luncher.right = -dirvec.normalized;

    }


    /*//����1
    IEnumerator EightBulletPattern()
    {
        Vector3 dir = Target.position - transform.position;
        float Timer = 0;
        // /���

        //���ڸ��
        while (Timer < MaxTime8) {
            Instantiate(EightBullet1, EightBulletLuncher.position, Quaternion.identity);
            Instantiate(EightBullet2, EightBulletLuncher.position, Quaternion.identity);
            yield return new WaitForSeconds(Delay8);//�Ѿ� �߻� ����;
            Timer += Delay8;
        }
    }*/
    //����2
    IEnumerator RushToPlayer()
    {

        RushArea.SetActive(true);
        float Distance = Vector3.Distance(Target.position,transform.position);//�÷��̾�� ���� ������ �Ÿ�

        //�ΰ��� ���� ���� ���� �ʱⰪ ������ ���� �뵵 �ν����Ϳ� ǥ�������� �����
        Vector3 nowscale = new Vector3(0,0.25f,0.25f);
        Vector3 nowpos = new Vector3(-0.3f, 0.2f,0);

        GetComponent<EnemyMovement_3>().enabled = false;//�ϴ� ���󰡴� �� ���߱�
        Stage3SoundManager.instace.StopWalkSound();

        RushArea.transform.localScale = nowscale;//ũ�� �ʱ�ȭ

        gameObject.GetComponent<Animator>().SetBool("Rush", true);
        Stage3SoundManager.instace.PlayBeforeRush();
        float add = 0;//���� ǥ�� ������ �ұ�
        while(Distance/7 >= add)
        {
            add += 0.1f;
            nowscale.x = add;
            RushArea.transform.localScale = nowscale;
            RushArea.transform.localPosition = new Vector3(nowpos.x - add, nowpos.y, nowpos.z);
            yield return new WaitForSeconds(0.05f);
        }//���� ���� ǥ��

        Vector3 Goalpos = RushArea.transform.GetChild(0).position;//���� ������ �ұ��

        RushArea.SetActive(false);//���� ���� ����

        Vector3 moveGaol = Goalpos - transform.position;//���� ��ġ�� �ٶ󺸴� ����

        RushEffect.SetActive(true);
        Stage3SoundManager.instace.PlayRushing();
        while (Vector3.Distance(transform.position, Goalpos) > 0.5f)
        {
            Debug.Log("����");
            //transform.Translate(moveGaol.normalized * 300 *Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, Goalpos, 100 * Time.deltaTime);
            yield return new WaitForSeconds(0.01f);
            
        } //����!!!!!!
        Stage3SoundManager.instace.PlayRushBoom();
        gameObject.GetComponent<Animator>().SetBool("Rush", false);

        RushEffect.SetActive(false);

        yield return new WaitForSeconds(1f);
        GetComponent<EnemyMovement_3>().enabled = true;//1�ʵڿ� �ٽ� �ٰ����� �Լ� ����
    }//�Ϸ�
    //����3
    IEnumerator CycleAttack()
    {
        float attackRate = 6;

        int count = 30; //��������

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
    }//�Ϸ�
    IEnumerator LockOnShot()
    {
        for (int i = 0; i < bombCount; i++)
        {
            //�÷��̾� �ؿ� ���� ���� ��ȯ
            Instantiate(Redfield, Target.position, Quaternion.identity);
            //��� �ݺ�
            yield return new WaitForSeconds(bombSetDelay);
        }
    }//�Ϸ�

    #region ����1 ��Ÿ
    IEnumerator LongLineAttack()
    {
        float duration = 60f; // 60�� ���� ����
        float spawnInterval = 0.05f; // źȯ ���� ����

        // ���� ���� ���� (Mx, My, UX, UY, flip)
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
    /*float sencond = 60f;//30��
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
    } // �긦 ���� 1�� ������
    void SpawnBullet(float Mx, float My, float UX, float UY, bool flip)
    {
        var bullet = Instantiate(Artbullet, transform.position, Quaternion.identity)
                     .GetComponent<LongLineAttack>();
        bullet.StartAttack(Mx, My, UX, UY, flip, transform);
    }
    //�Ѱ� �� �߰�������?
    #endregion // �Ϸ�



    #region ForUI
    public void Pattern1() { StartCoroutine(LongLineAttack());}
    public void Pattern2() { StartCoroutine(RushToPlayer()); }
    public void Pattern3() { StartCoroutine(CycleAttack()); }
    public void Pattern4() { StartCoroutine(LockOnShot()); }
    #endregion


}
