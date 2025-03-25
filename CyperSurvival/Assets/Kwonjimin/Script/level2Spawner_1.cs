using UnityEngine;
using System.Collections; // IEnumerator ���
using System.Collections.Generic; // List<> ���

public class level2Spawner_1 : MonoBehaviour
{
    public GameObject monsterPrefab; // ��ȯ�� ���� ������
    public Transform spawnPointsParent; // ���� ����Ʈ���� �θ� ������Ʈ
    public GameObject level1Spawner; // ���ӿ��� ������ �� ���� �����ϴ� ������Ʈ
    private List<Transform> availableSpawnPoints = new List<Transform>();
    private Transform lastSpawnPoint = null;

    void Start()
    {
        // ���� ����Ʈ �ʱ�ȭ
        foreach (Transform child in spawnPointsParent)
        {
            availableSpawnPoints.Add(child);
        }

        // level1Spawner�� ����� �� ���� ����
        StartCoroutine(WaitForLevel1SpawnerDeletion());
    }

    IEnumerator WaitForLevel1SpawnerDeletion() // ���⼭ 'IEnumerator'�� System.Collections.IEnumerator ���
    {
        while (level1Spawner != null)
        {
            yield return null; // �� �����Ӹ��� üũ
        }
        StartCoroutine(SpawnMonstersWithDelay()); // ���͸� 5�� �������� ����
    }

    IEnumerator SpawnMonstersWithDelay()
    {
        for (int i = 0; i < 5; i++) // �ټ� �� ���� ����
        {
            SpawnMonster();
            yield return new WaitForSeconds(5f); // 5�� ��� �� ���� ���� ����
        }

        // ���� 5���� ������ �� level2Spawner ������Ʈ ����
        Destroy(gameObject);
    }

    void SpawnMonster()
    {
        if (availableSpawnPoints.Count == 0)
        {
            Debug.LogWarning("���� ����Ʈ�� �����ϴ�!");
            return;
        }

        // ������ ���� ����Ʈ�� ������ ���� ����
        Transform randomSpawnPoint;
        do
        {
            randomSpawnPoint = availableSpawnPoints[Random.Range(0, availableSpawnPoints.Count)];
        } while (randomSpawnPoint == lastSpawnPoint && availableSpawnPoints.Count > 1);

        lastSpawnPoint = randomSpawnPoint;

        // ���� ����
        Instantiate(monsterPrefab, randomSpawnPoint.position, Quaternion.identity);
    }
}