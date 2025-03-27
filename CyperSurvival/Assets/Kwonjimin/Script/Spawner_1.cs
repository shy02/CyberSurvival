using UnityEngine;
using System.Collections;

public class Spawner_1 : MonoBehaviour
{
    // 레벨 1 관련
    public GameObject level1MonsterPrefab;
    public int level1MonsterCount = 10;

    // 레벨 2 관련
    public GameObject level2MonsterPrefab;
    public Transform[] level2SpawnPoints;
    public int level2MonsterCount = 7;

    private int spawnedLevel2MonsterCount = 0;

    private float minX = -0.6f, maxX = 7.2f;
    private float minY = -14f, maxY = -5f;
    private float zPos = 1f;
    private Transform[] level1SpawnPoints;

    // 🎵 몬스터 생성 효과음 (레벨 1, 2 공통)
    public AudioClip spawnSound;
    private AudioSource audioSource; // AudioSource 추가

    void Start()
    {
        // AudioSource 컴포넌트 추가
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = spawnSound; // 오디오 클립 설정
        audioSource.playOnAwake = false; // 게임 시작 시 자동으로 재생되지 않도록 설정

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
            Instantiate(level1MonsterPrefab, spawnPoint.position, Quaternion.identity);

            // 🎵 몬스터 생성 효과음 재생 (레벨 1)
            PlaySpawnSound(spawnPoint.position);

            yield return new WaitForSeconds(2f);
        }

        StartCoroutine(SpawnLevel2Monsters());
    }

    IEnumerator SpawnLevel2Monsters()
    {
        for (int i = 0; i < level2MonsterCount; i++)
        {
            Transform spawnPoint = level2SpawnPoints[Random.Range(0, level2SpawnPoints.Length)];
            Instantiate(level2MonsterPrefab, spawnPoint.position, Quaternion.identity);
            spawnedLevel2MonsterCount++;

            // 🎵 몬스터 생성 효과음 재생 (레벨 2)
            PlaySpawnSound(spawnPoint.position);

            yield return new WaitForSeconds(3f);
        }

        if (spawnedLevel2MonsterCount >= 7)
        {
            GameObject bossSpawner = GameObject.Find("BossSpawner");
            if (bossSpawner != null)
            {
                bossSpawner.GetComponent<BossSpawner_1>().SpawnBoss();
            }
        }
    }

    // 🎵 몬스터 생성 효과음 재생 함수
    void PlaySpawnSound(Vector3 position)
    {
        if (spawnSound != null && audioSource != null)
        {
            // 위치 지정 및 볼륨 설정 (볼륨을 4배로 설정)
            audioSource.volume = 4.0f;
            audioSource.transform.position = position;  // 소리 위치 설정
            audioSource.Play();  // 재생
        }
    }
}
