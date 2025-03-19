using UnityEngine;

public class level1Spawn : MonoBehaviour
{
    public GameObject enemyPrefab; // ��ȯ�� ���� ������
    public float spawnInterval = 3f; // ���� ��ȯ ����
    public int maxEnemies = 10; // ������ ���� ��

    private float minX = -0.6f, maxX = 7.2f;
    private float minY = -14f, maxY = -5f;
    private float zPos = 1f;

    private int enemyCount = 0; // ���� ������ ���� ��

    void Start()
    {
        // ���� �ð����� ���� ��ȯ
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        // �ִ� ���� ����ŭ �����Ǿ����� ��ȯ ���߱�
        if (enemyCount >= maxEnemies)
        {
            CancelInvoke("SpawnEnemy"); // �� �̻� ���� �������� ����
            return;
        }

        Vector3 spawnPosition = new Vector3(
            Random.Range(minX, maxX), // X ��ǥ ���� ��
            Random.Range(minY, maxY), // Y ��ǥ ���� ��
            zPos                     // Z ��ǥ ���� (1)
        );

        // ���� ����
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        enemyCount++; // ������ ���� �� ����
    }
}
