using UnityEngine;
using System.Collections;

public class Spawner_1 : MonoBehaviour
{
    public GameObject level1MonsterPrefab;
    public int level1MonsterCount = 10;
    private int spawnedLevel1MonsterCount = 0;

    public GameObject level2MonsterPrefab;
    public Transform[] level2SpawnPoints;
    public int level2MonsterCount = 7;
    private int spawnedLevel2MonsterCount = 0;
    private int level2MonstersAlive = 0;

    private bool isLevel2MonstersSpawned = false;
    private bool isBossSpawned = false;
    private bool stopSpawning = false; // 🔹 스폰 중지 여부 추가

    private float minX = -0.6f, maxX = 7.2f;
    private float minY = -14f, maxY = -5f;
    private float zPos = 1f;
    private Transform[] level1SpawnPoints;

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

    void Update()
    {
        if (GameManager.Instance.nowNextStage || GameManager.Instance.nowGameOver)
        {
            audioSource.Stop();
            stopSpawning = true; // 🔹 몬스터 스폰 중지
        }
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
            if (stopSpawning) yield break; // 🔹 스폰 중지 체크

            Transform spawnPoint = level1SpawnPoints[i];
            GameObject monster = Instantiate(level1MonsterPrefab, spawnPoint.position, Quaternion.identity);
            spawnedLevel1MonsterCount++;

            monster.GetComponent<EnemyDamage_3>().OnDeath += () => {
                spawnedLevel1MonsterCount--;
            };

            PlaySpawnSound(spawnPoint.position);
            yield return new WaitForSeconds(2f);
        }

        while (spawnedLevel1MonsterCount > 0)
        {
            if (stopSpawning) yield break; // 🔹 스폰 중지 체크
            yield return null;
        }

        StartCoroutine(SpawnLevel2Monsters());
    }

    IEnumerator SpawnLevel2Monsters()
    {
        if (isLevel2MonstersSpawned || stopSpawning) yield break; // 🔹 스폰 중지 체크

        level2MonstersAlive = level2MonsterCount;
        isLevel2MonstersSpawned = true;

        for (int i = 0; i < level2MonsterCount; i++)
        {
            if (stopSpawning) yield break; // 🔹 스폰 중지 체크

            Transform spawnPoint = level2SpawnPoints[Random.Range(0, level2SpawnPoints.Length)];
            GameObject monster = Instantiate(level2MonsterPrefab, spawnPoint.position, Quaternion.identity);
            spawnedLevel2MonsterCount++;

            monster.GetComponent<EnemyDamage_3>().OnDeath += () => {
                level2MonstersAlive--;
            };

            PlaySpawnSound(spawnPoint.position);
            yield return new WaitForSeconds(3f);
        }

        while (level2MonstersAlive > 0)
        {
            if (stopSpawning) yield break; // 🔹 스폰 중지 체크
            yield return null;
        }

        if (!isBossSpawned)
        {
            SpawnBoss();
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
        if (isBossSpawned || stopSpawning) return; // 🔹 스폰 중지 체크

        GameObject bossSpawner = GameObject.Find("BossSpawner");
        if (bossSpawner != null)
        {
            bossSpawner.GetComponent<BossSpawner_1>().SpawnBoss();
            isBossSpawned = true;
        }
        else
        {
            Debug.LogError("BossSpawner를 찾을 수 없습니다.");
        }
    }
}
