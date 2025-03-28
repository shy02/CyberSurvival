using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SpawnManager_s_3 : MonoBehaviour
{
    //1. ���� �ð��� ������ ���� �ð����� �ִ��� ����� ���� ��� ��ȭ�ؾ���
    //2. ���ѽð����� ���� �ð� �������� ��� ����Ʈ���� ���� ������V
    //3. ���ѽð��� ����Ǹ� ������ ����V

    //4. �ð� �ʹ� ������ �⺻ ���� ���V
    //5. �ð� �߹� ���ʹ� ��Ÿ� ���� ���� �Բ�
    //6. �ð� �Ĺ� ���ʹ� ��ġũ�� ü���� ���� ���� ���

    [Tooltip("�ð� �ʹݺ��� ����ϴ� �⺻ ��")]
    [SerializeField] GameObject enemy1;
    [Tooltip("�ð� �߹ݺ��� ����ϴ� ��Ÿ� ��")]
    [SerializeField] GameObject enemy2;
    [Tooltip("�ð� �Ĺݺ��� ����ϴ� ��Ŀ ��")]
    [SerializeField] GameObject enemy3;
    [Tooltip("�ð��� ��� ������ Ǯ������ ����")]
    [SerializeField] GameObject Boss;
    [SerializeField] GameObject BossUI;

    [Tooltip("������ Ǯ��������� ���� �ð�")]
    [SerializeField] float LimitedTime = 300f;//�ʴ���
    float LimitedTime_Max = 0;

    [Tooltip("������ ��ȯ�Ǵ� ����")]
    [SerializeField] float MobSpawnDelay = 3f;//�ʴ���


    List<Transform> Spawnposes = new List<Transform>();

    [SerializeField] Transform parent;//������ ���� �θ�
    

    private void Start()
    {
        InitializeSpawnPoints();
        StartCoroutine(StartTimerCount());
        StartCoroutine(StartMobSpawn());
    }

    private void InitializeSpawnPoints()
    {
        LimitedTime_Max = LimitedTime;
        for (int i = 0; i < transform.childCount; i++)
        {
            Spawnposes.Add(transform.GetChild(i));
        }
    }

    private void Update()
    {
        if (GameManager.Instance.nowNextStage && parent != null)
        {
            StopAllCoroutines();
            Destroy(parent.gameObject);
        }
    }

    #region �� �������� �ڷ�ƾ & �޼���
    IEnumerator StartMobSpawn()
    {
        while (!GameManager.Instance.nowGameOver)
        {
            yield return new WaitForSeconds(MobSpawnDelay);
            if (LimitedTime <= LimitedTime_Max / 3)//���� �Ĺ�
            {
                SpawnEnemies(enemy3);
            }

            if (LimitedTime <= LimitedTime_Max / 3 * 2)//���� �߹�
            {
                GameObject obj = SpawnEnemies(enemy2);
                BulletSetParent(obj.GetComponent<Enemy2_Shot_3>());
                
                yield return new WaitForSeconds(0.5f);
            }

            //���� �ʹ�
            for (int i = 0; i < transform.childCount; i++)
            {
                Instantiate(enemy1, Spawnposes[i].position, Quaternion.identity,parent);
            }
        }
    }

    private GameObject SpawnEnemies(GameObject Kind)
    {
        GameObject obj = null;
        int count = Random.Range(1, transform.childCount - 3);

        for (int i = 0; i < count; i++)
        {
            int point = Random.Range(1, transform.childCount);
            obj = Instantiate(Kind, Spawnposes[point].position, Quaternion.identity, parent);
        }

        return obj;
    }
    private void BulletSetParent(Enemy2_Shot_3 bulletparent)
    {
        if (bulletparent != null)
        {
            bulletparent.parent = this.parent;
        }
    }
    #endregion


    IEnumerator StartTimerCount()
    {
        while(LimitedTime > 0 && !GameManager.Instance.nowGameOver)
        {
            yield return new WaitForSeconds(1f);
            LimitedTime--;
        }

        {
            if (!GameManager.Instance.nowGameOver)
            {
                StopAllCoroutines();
                Boss.GetComponent<BossAttackManager_3>().StartAttack();
                //���� ��ȯ
                BossUI.SetActive(true);
                Boss.GetComponent<Animator>().SetBool("TurnOn", true);
            }
        }
    } // ������ȯ ������ ����ϴ� Ÿ�̸� �ڷ�ƾ

    public void BossTime()
    {
        LimitedTime = 0f;
    } // �ν����� ��ư�� ȣ���Լ�
}
