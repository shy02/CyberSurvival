using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager_s : MonoBehaviour
{
    //1. 제한 시간이 존재함 제한 시간동안 최대한 잡몹을 많이 잡아 강화해야함
    //2. 제한시간동안 일정 시간 간격으로 모든 포인트에서 적이 스폰됨V
    //3. 제한시간이 종료되면 스폰만 멈춤V

    //4. 시간 초반 까지는 기본 몹이 출몰V
    //5. 시간 중반 부터는 장거리 공격 몹이 함께
    //6. 시간 후반 부터는 덩치크고 체력이 높은 몹이 출몰

    [Tooltip("시간 초반부터 출몰하는 기본 몹")]
    [SerializeField] GameObject enemy1;
    [Tooltip("시간 중반부터 출몰하는 장거리 몹")]
    [SerializeField] GameObject enemy2;
    [Tooltip("시간 후반부터 출몰하는 탱커 몹")]
    [SerializeField] GameObject enemy3;
    [Tooltip("시간이 모두 지나고 풀려나는 보스")]
    [SerializeField] GameObject Boss;

    [Tooltip("보스가 풀려나기까지 남은 시간")]
    [SerializeField] float LimitedTime = 300f;//초단위
    float LimitedTime_Max = 0;

    [Tooltip("적들이 소환되는 간격")]
    [SerializeField] float MobSpawnDelay = 3f;//초단위


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
            if (LimitedTime <= LimitedTime_Max / 3)//게임 후반
            {
                Debug.Log("게임 후반");
                int count = Random.Range(1, transform.childCount - 3);

                for (int i = 0; i < count; i++)
                {
                    int point = Random.Range(1, transform.childCount);
                    Instantiate(enemy3, Spawnposes[point].position, Quaternion.identity);
                }

            }

            if (LimitedTime <= LimitedTime_Max / 3 * 2)//게임 중반
            {
                Debug.Log("게임 중반");
                int count = Random.Range(1, transform.childCount - 3);

                for (int i = 0; i < count; i++)
                {
                    int point = Random.Range(1, transform.childCount);
                    Instantiate(enemy2, Spawnposes[point].position, Quaternion.identity);
                }
                yield return new WaitForSeconds(0.5f);
            }

            //게임 초반
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
            //보스 소환
            //Boss.GetComponent<Animator>().SetTrigger("Apear");
        }
    }
}
