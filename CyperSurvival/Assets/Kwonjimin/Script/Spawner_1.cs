using UnityEngine;
using System.Collections;

public class Spawner_1 : MonoBehaviour
{
    // 레벨 1 관련
    public GameObject level1MonsterPrefab; // 레벨 1 몬스터 프리팹
    public int level1MonsterCount = 10; // 레벨 1에서 생성할 몬스터 수

    // 레벨 2 관련
    public GameObject level2MonsterPrefab; // 레벨 2 몬스터 프리팹
    public Transform[] level2SpawnPoints; // 레벨 2 몬스터 스폰 위치들
    public int level2MonsterCount = 7; // 레벨 2에서 생성할 몬스터 수

    // 레벨 2 몬스터가 스폰된 카운트
    private int spawnedLevel2MonsterCount = 0;

    // 레벨 1 스폰 위치 관련 변수
    private float minX = -0.6f, maxX = 7.2f;
    private float minY = -14f, maxY = -5f;
    private float zPos = 1f;
    private Transform[] level1SpawnPoints;

    void Start()
    {
        // 레벨 1 스폰 포인트 생성
        GenerateLevel1SpawnPoints();

        // 레벨 1 몬스터 생성 시작
        StartCoroutine(SpawnLevel1Monsters());
    }

    // 레벨 1 스폰 포인트 생성
    void GenerateLevel1SpawnPoints()
    {
        level1SpawnPoints = new Transform[level1MonsterCount];
        for (int i = 0; i < level1MonsterCount; i++)
        {
            GameObject spawnPointObj = new GameObject("Level1SpawnPoint_" + i);
            Vector3 spawnPosition = new Vector3(
                Random.Range(minX, maxX), // X 좌표 랜덤 값
                Random.Range(minY, maxY), // Y 좌표 랜덤 값
                zPos                     // Z 좌표 고정 (1)
            );
            spawnPointObj.transform.position = spawnPosition;
            level1SpawnPoints[i] = spawnPointObj.transform;
        }
    }

    // 레벨 1 몬스터 생성
    IEnumerator SpawnLevel1Monsters()
    {
        for (int i = 0; i < level1MonsterCount; i++)
        {
            Transform spawnPoint = level1SpawnPoints[i];
            Instantiate(level1MonsterPrefab, spawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(2f); // 2초 간격으로 몬스터 소환
        }

        // 레벨 1 몬스터들이 다 죽었으면 레벨 2 몬스터 생성
        StartCoroutine(SpawnLevel2Monsters());
    }

    // 레벨 2 몬스터 생성
    IEnumerator SpawnLevel2Monsters()
    {
        for (int i = 0; i < level2MonsterCount; i++)
        {
            Transform spawnPoint = level2SpawnPoints[Random.Range(0, level2SpawnPoints.Length)];
            Instantiate(level2MonsterPrefab, spawnPoint.position, Quaternion.identity);
            spawnedLevel2MonsterCount++; // 몬스터가 스폰되면 카운트 증가
            yield return new WaitForSeconds(3f); // 3초 간격으로 몬스터 소환
        }

        // 레벨 2 몬스터가 7마리가 스폰되면 보스 스폰
        if (spawnedLevel2MonsterCount >= 7)
        {
            // 보스 스폰 코드 호출
            GameObject bossSpawner = GameObject.Find("BossSpawner");
            if (bossSpawner != null)
            {
                bossSpawner.GetComponent<BossSpawner_1>().SpawnBoss();
            }
        }
    }
}