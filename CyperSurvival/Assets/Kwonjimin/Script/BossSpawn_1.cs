using System.Collections;
using UnityEngine;

public class BossSpawn_1 : MonoBehaviour
{
    public GameObject bossPrefab;  // 보스 프리팹
    public Transform[] portalPositions;  // 포탈 위치들
    public GameObject level2Spawner;  // level2Spawner 변수
    public GameObject player;  // 플레이어

    private GameObject spawnedBoss;  // 생성된 보스

    void Start()
    {
        // level2Spawner가 사라지면 보스와 포탈을 활성화
        StartCoroutine(WaitForLevel2SpawnerDeletion());
    }

    IEnumerator WaitForLevel2SpawnerDeletion()
    {
        // level2Spawner가 null이 되거나 비활성화 될 때까지 기다림
        while (level2Spawner != null && level2Spawner.activeInHierarchy)
        {
            yield return null;  // 매 프레임마다 level2Spawner가 삭제되었는지 체크
        }

        // level2Spawner가 사라지면 보스와 포탈을 활성화
        SpawnBoss();
    }

    void SpawnBoss()
    {
        // 보스를 생성하고 포탈을 활성화
        spawnedBoss = Instantiate(bossPrefab, transform.position, Quaternion.identity);

        // Boss_1 스크립트가 활성화되어 있어야 포탈을 활성화할 수 있다.
        Boss_1 bossScript = spawnedBoss.GetComponent<Boss_1>();
        if (bossScript != null)
        {
            // 보스에 플레이어와 포탈 위치 전달
            bossScript.SetBossData(player, portalPositions);

            // 포탈을 모두 활성화
            foreach (Transform portal in portalPositions)
            {
                portal.gameObject.SetActive(true);
            }
        }
        else
        {
            Debug.LogError("Boss_1 스크립트가 보스 오브젝트에 없습니다.");
        }
    }
}
