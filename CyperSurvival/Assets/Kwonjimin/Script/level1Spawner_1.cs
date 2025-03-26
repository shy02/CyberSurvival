using UnityEngine;
using System.Collections.Generic;

public class Level1Spawner_1 : MonoBehaviour
{
    public GameObject enemyPrefab; // 소환할 몬스터 프리팹
    public float spawnInterval = 3f; // 몬스터 소환 간격
    public int maxEnemies = 10; // 생성할 몬스터 수
    public float spawnRadius = 1.5f; // 몬스터 간 최소 거리

    private float minX = -0.6f, maxX = 7.2f;
    private float minY = -14f, maxY = -5f;
    private float zPos = 1f;

    private int enemyCount = 0; // 현재 생성된 몬스터 수
    private List<Vector3> spawnedPositions = new List<Vector3>(); // 생성된 몬스터 위치 저장 리스트

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
            Destroy(gameObject); // 스포너 객체 삭제
            return;
        }

        Vector3 spawnPosition;
        int attempts = 0;
        bool positionFound = false;

        do
        {
            spawnPosition = new Vector3(
                Random.Range(minX, maxX), // X 좌표 랜덤 값
                Random.Range(minY, maxY), // Y 좌표 랜덤 값
                zPos                     // Z 좌표 고정 (1)
            );

            // 해당 위치에 이미 몬스터가 있는지 확인
            if (!IsPositionOccupied(spawnPosition))
            {
                positionFound = true;
            }

            attempts++;

        } while (!positionFound && attempts < 10); // 최대 10번 시도 후 포기

        if (positionFound)
        {
            // 몬스터 생성
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            spawnedPositions.Add(spawnPosition); // 리스트에 위치 추가
            enemyCount++; // 생성된 몬스터 수 증가
        }
    }

    // 🎯 해당 위치에 몬스터가 있는지 확인하는 함수
    bool IsPositionOccupied(Vector3 position)
    {
        foreach (Vector3 existingPosition in spawnedPositions)
        {
            if (Vector3.Distance(position, existingPosition) < spawnRadius)
            {
                return true; // 너무 가까운 위치면 겹침 판정
            }
        }

        return false;
    }
}
