using UnityEngine;
using System.Collections;

public class Spawner_1 : MonoBehaviour
{
    // 레벨 1 관련
    public GameObject level1MonsterPrefab;
    public int level1MonsterCount = 10;
    private int spawnedLevel1MonsterCount = 0; // 스폰된 레벨 1 몬스터 개수 추적

    // 레벨 2 관련
    public GameObject level2MonsterPrefab;
    public Transform[] level2SpawnPoints;
    public int level2MonsterCount = 7;
    private int spawnedLevel2MonsterCount = 0;
    private int level2MonstersAlive = 0; // 레벨 2 몬스터 생존 추적

    private bool isLevel2MonstersSpawned = false; // 레벨 2 몬스터가 스폰되었는지 여부
    private bool isBossSpawned = false; // 보스가 스폰되었는지 여부

    private float minX = -0.6f, maxX = 7.2f;
    private float minY = -14f, maxY = -5f;
    private float zPos = 1f;
    private Transform[] level1SpawnPoints;

    // 🎵 몬스터 생성 효과음 (레벨 1, 2 공통)
    public AudioClip spawnSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = spawnSound;
        audioSource.playOnAwake = false;

        GenerateLevel1SpawnPoints();
        StartCoroutine(SpawnLevel1Monsters());
    }

    void GenerateLevel1SpawnPoints()
    {
        level1SpawnPoints = new Transform[level1MonsterCount];
        for (int i = 0; i < level1MonsterCount; i++)
        {
            GameObject spawnPointObj = new GameObject("Level1SpawnPoint_" + i);
            Vector3 spawnPosition = new Vector3(
                Random.Range(minX, maxX),
                Random.Range(minY, maxY),
                zPos
            );
            spawnPointObj.transform.position = spawnPosition;
            level1SpawnPoints[i] = spawnPointObj.transform;
        }
    }

    IEnumerator SpawnLevel1Monsters()
    {
        for (int i = 0; i < level1MonsterCount; i++)
        {
            Transform spawnPoint = level1SpawnPoints[i];
            GameObject monster = Instantiate(level1MonsterPrefab, spawnPoint.position, Quaternion.identity);
            spawnedLevel1MonsterCount++; // 스폰된 몬스터 개수 증가

            // 🔥 몬스터가 죽었을 때 체크할 이벤트 추가
            monster.GetComponent<EnemyDamage_3>().OnDeath += () => {
                spawnedLevel1MonsterCount--; // 몬스터가 죽으면 스폰된 몬스터 개수 감소
            };

            PlaySpawnSound(spawnPoint.position);
            yield return new WaitForSeconds(2f); // 일정 시간 간격으로 몬스터 스폰
        }

        // 모든 레벨 1 몬스터가 죽었을 때 레벨 2 몬스터를 스폰
        while (spawnedLevel1MonsterCount > 0)
        {
            yield return null; // 레벨 1 몬스터가 죽을 때까지 기다림
        }

        // 레벨 1 몬스터가 모두 죽었으면 레벨 2 몬스터를 스폰
        StartCoroutine(SpawnLevel2Monsters());
    }

    IEnumerator SpawnLevel2Monsters()
    {
        if (isLevel2MonstersSpawned) yield break; // 이미 레벨 2 몬스터가 스폰된 경우 종료

        level2MonstersAlive = level2MonsterCount; // 레벨 2 몬스터 수 초기화
        isLevel2MonstersSpawned = true; // 레벨 2 몬스터 스폰 시작

        for (int i = 0; i < level2MonsterCount; i++)
        {
            Transform spawnPoint = level2SpawnPoints[Random.Range(0, level2SpawnPoints.Length)];
            GameObject monster = Instantiate(level2MonsterPrefab, spawnPoint.position, Quaternion.identity);
            spawnedLevel2MonsterCount++; // 스폰된 레벨 2 몬스터 개수 증가

            // 레벨 2 몬스터가 죽었을 때 이벤트 추가
            monster.GetComponent<EnemyDamage_3>().OnDeath += () => {
                level2MonstersAlive--; // 몬스터가 죽으면 살아있는 몬스터 수 감소
            };

            PlaySpawnSound(spawnPoint.position);
            yield return new WaitForSeconds(3f); // 일정 시간 간격으로 몬스터 스폰
        }

        // 레벨 2 몬스터가 모두 죽을 때까지 기다리기
        while (level2MonstersAlive > 0)
        {
            yield return null; // 레벨 2 몬스터가 죽을 때까지 기다림
        }

        // 모든 레벨 2 몬스터가 죽었을 때 보스를 스폰
        if (!isBossSpawned)
        {
            SpawnBoss(); // 보스 스폰
        }
    }

    void PlaySpawnSound(Vector3 position)
    {
        if (spawnSound != null && audioSource != null)
        {
            audioSource.volume = 4.0f;
            audioSource.transform.position = position;
            audioSource.Play();
        }
    }

    void SpawnBoss()
    {
        if (isBossSpawned) return; // 보스가 이미 소환되었으면 다시 소환하지 않음

        // 보스 스폰을 담당하는 함수
        GameObject bossSpawner = GameObject.Find("BossSpawner");
        if (bossSpawner != null)
        {
            bossSpawner.GetComponent<BossSpawner_1>().SpawnBoss();
            isBossSpawned = true; // 보스가 소환되었음을 기록
        }
        else
        {
            Debug.LogError("BossSpawner를 찾을 수 없습니다.");
        }
    }
}
