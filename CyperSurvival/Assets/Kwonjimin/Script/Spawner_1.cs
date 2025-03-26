using UnityEngine;
using System.Collections;

public class Spawner_1 : MonoBehaviour
{
    // ���� 1 ����
    public GameObject level1MonsterPrefab; // ���� 1 ���� ������
    public int level1MonsterCount = 10; // ���� 1���� ������ ���� ��

    // ���� 2 ����
    public GameObject level2MonsterPrefab; // ���� 2 ���� ������
    public Transform[] level2SpawnPoints; // ���� 2 ���� ���� ��ġ��
    public int level2MonsterCount = 7; // ���� 2���� ������ ���� ��

    // ���� 2 ���Ͱ� ������ ī��Ʈ
    private int spawnedLevel2MonsterCount = 0;

    // ���� 1 ���� ��ġ ���� ����
    private float minX = -0.6f, maxX = 7.2f;
    private float minY = -14f, maxY = -5f;
    private float zPos = 1f;
    private Transform[] level1SpawnPoints;

    void Start()
    {
        // ���� 1 ���� ����Ʈ ����
        GenerateLevel1SpawnPoints();

        // ���� 1 ���� ���� ����
        StartCoroutine(SpawnLevel1Monsters());
    }

    // ���� 1 ���� ����Ʈ ����
    void GenerateLevel1SpawnPoints()
    {
        level1SpawnPoints = new Transform[level1MonsterCount];
        for (int i = 0; i < level1MonsterCount; i++)
        {
            GameObject spawnPointObj = new GameObject("Level1SpawnPoint_" + i);
            Vector3 spawnPosition = new Vector3(
                Random.Range(minX, maxX), // X ��ǥ ���� ��
                Random.Range(minY, maxY), // Y ��ǥ ���� ��
                zPos                     // Z ��ǥ ���� (1)
            );
            spawnPointObj.transform.position = spawnPosition;
            level1SpawnPoints[i] = spawnPointObj.transform;
        }
    }

    // ���� 1 ���� ����
    IEnumerator SpawnLevel1Monsters()
    {
        for (int i = 0; i < level1MonsterCount; i++)
        {
            Transform spawnPoint = level1SpawnPoints[i];
            Instantiate(level1MonsterPrefab, spawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(2f); // 2�� �������� ���� ��ȯ
        }

        // ���� 1 ���͵��� �� �׾����� ���� 2 ���� ����
        StartCoroutine(SpawnLevel2Monsters());
    }

    // ���� 2 ���� ����
    IEnumerator SpawnLevel2Monsters()
    {
        for (int i = 0; i < level2MonsterCount; i++)
        {
            Transform spawnPoint = level2SpawnPoints[Random.Range(0, level2SpawnPoints.Length)];
            Instantiate(level2MonsterPrefab, spawnPoint.position, Quaternion.identity);
            spawnedLevel2MonsterCount++; // ���Ͱ� �����Ǹ� ī��Ʈ ����
            yield return new WaitForSeconds(3f); // 3�� �������� ���� ��ȯ
        }

        // ���� 2 ���Ͱ� 7������ �����Ǹ� ���� ����
        if (spawnedLevel2MonsterCount >= 7)
        {
            // ���� ���� �ڵ� ȣ��
            GameObject bossSpawner = GameObject.Find("BossSpawner");
            if (bossSpawner != null)
            {
                bossSpawner.GetComponent<BossSpawner_1>().SpawnBoss();
            }
        }
    }
}