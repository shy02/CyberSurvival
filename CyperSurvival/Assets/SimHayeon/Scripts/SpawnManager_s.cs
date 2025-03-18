using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager_s : MonoBehaviour
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

    [Tooltip("������ Ǯ��������� ���� �ð�")]
    [SerializeField] float LimitedTime = 300f;//�ʴ���
    float LimitedTime_Max = 0;

    [Tooltip("������ ��ȯ�Ǵ� ����")]
    [SerializeField] float MobSpawnDelay = 3f;//�ʴ���


    List<Transform> Spawnposes = new List<Transform>();

    private void Start()
    {
        LimitedTime_Max = LimitedTime;
        for(int i = 0; i < transform.childCount; i++)
        {
            Spawnposes.Add(transform.GetChild(i));
        }

        StartCoroutine(StartTimerCount());
        StartCoroutine(StartMobSpawn());
    }
    IEnumerator StartMobSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(MobSpawnDelay);
            if (LimitedTime <= LimitedTime_Max / 3)//���� �Ĺ�
            {
                Debug.Log("���� �Ĺ�");
                int count = Random.Range(1, transform.childCount - 3);

                for (int i = 0; i < count; i++)
                {
                    int point = Random.Range(1, transform.childCount);
                    Instantiate(enemy3, Spawnposes[point].position, Quaternion.identity);
                }

            }

            if (LimitedTime <= LimitedTime_Max / 3 * 2)//���� �߹�
            {
                Debug.Log("���� �߹�");
                int count = Random.Range(1, transform.childCount - 3);

                for (int i = 0; i < count; i++)
                {
                    int point = Random.Range(1, transform.childCount);
                    Instantiate(enemy2, Spawnposes[point].position, Quaternion.identity);
                }
                yield return new WaitForSeconds(0.5f);
            }

            //���� �ʹ�
            for (int i = 0; i < transform.childCount; i++)
            {
                Instantiate(enemy1, Spawnposes[i].position, Quaternion.identity);
            }
        }
    }

    IEnumerator StartTimerCount()
    {
        if (LimitedTime > 0)
        {
            yield return new WaitForSeconds(1f);
            LimitedTime--;
            StartCoroutine(StartTimerCount());
        }
        else
        {
            StopAllCoroutines();
            //���� ��ȯ
            //Boss.GetComponent<Animator>().SetTrigger("Apear");
        }
    }
}
