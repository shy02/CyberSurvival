using UnityEngine;

public class level1Spawn : MonoBehaviour
{
    public GameObject enemyPrefab; // 소환할 몬스터 프리팹
    public float spawnInterval = 3f; // 몬스터 소환 간격
    public int maxEnemies = 10; // 생성할 몬스터 수

    private float minX = -0.6f, maxX = 7.2f;
    private float minY = -14f, maxY = -5f;
    private float zPos = 1f;

    private int enemyCount = 0; // 현재 생성된 몬스터 수

    void Start()
    {
        // 일정 시간마다 몬스터 소환
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        // 최대 몬스터 수만큼 생성되었으면 소환 멈추기
        if (enemyCount >= maxEnemies)
        {
            CancelInvoke("SpawnEnemy"); // 더 이상 몬스터 생성하지 않음
            return;
        }

        Vector3 spawnPosition = new Vector3(
            Random.Range(minX, maxX), // X 좌표 랜덤 값
            Random.Range(minY, maxY), // Y 좌표 랜덤 값
            zPos                     // Z 좌표 고정 (1)
        );

        // 몬스터 생성
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        enemyCount++; // 생성된 몬스터 수 증가
    }
}
