using UnityEngine;
using System.Collections; // IEnumerator 사용
using System.Collections.Generic; // List<> 사용

public class level2Spawner_1 : MonoBehaviour
{
    public GameObject monsterPrefab; // 소환할 몬스터 프리팹
    public Transform spawnPointsParent; // 스폰 포인트들의 부모 오브젝트
    public GameObject level1Spawner; // 게임에서 삭제될 때 스폰 시작하는 오브젝트
    private List<Transform> availableSpawnPoints = new List<Transform>();
    private Transform lastSpawnPoint = null;

    void Start()
    {
        // 스폰 포인트 초기화
        foreach (Transform child in spawnPointsParent)
        {
            availableSpawnPoints.Add(child);
        }

        // level1Spawner가 사라질 때 스폰 시작
        StartCoroutine(WaitForLevel1SpawnerDeletion());
    }

    IEnumerator WaitForLevel1SpawnerDeletion() // 여기서 'IEnumerator'는 System.Collections.IEnumerator 사용
    {
        while (level1Spawner != null)
        {
            yield return null; // 매 프레임마다 체크
        }
        StartCoroutine(SpawnMonstersWithDelay()); // 몬스터를 5초 간격으로 생성
    }

    IEnumerator SpawnMonstersWithDelay()
    {
        for (int i = 0; i < 5; i++) // 다섯 개 몬스터 생성
        {
            SpawnMonster();
            yield return new WaitForSeconds(5f); // 5초 대기 후 다음 몬스터 생성
        }

        // 몬스터 5개가 생성된 후 level2Spawner 오브젝트 삭제
        Destroy(gameObject);
    }

    void SpawnMonster()
    {
        if (availableSpawnPoints.Count == 0)
        {
            Debug.LogWarning("스폰 포인트가 없습니다!");
            return;
        }

        // 마지막 스폰 포인트를 제외한 랜덤 선택
        Transform randomSpawnPoint;
        do
        {
            randomSpawnPoint = availableSpawnPoints[Random.Range(0, availableSpawnPoints.Count)];
        } while (randomSpawnPoint == lastSpawnPoint && availableSpawnPoints.Count > 1);

        lastSpawnPoint = randomSpawnPoint;

        // 몬스터 생성
        Instantiate(monsterPrefab, randomSpawnPoint.position, Quaternion.identity);
    }
}