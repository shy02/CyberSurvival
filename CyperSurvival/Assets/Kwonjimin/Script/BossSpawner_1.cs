using UnityEngine;

public class BossSpawner_1 : MonoBehaviour
{
    public GameObject bossPrefab; // 보스 프리팹
    public GameObject player; // 플레이어
    public Transform[] portalPositions; // 포탈 위치

    void Start()
    {
        // 플레이어 태그로 자동으로 플레이어 오브젝트를 찾음
        player = GameObject.FindWithTag("Player");

        if (player == null)
        {
            Debug.LogError("Player 태그를 가진 오브젝트를 찾을 수 없습니다.");
        }
    }

    public void SpawnBoss()
    {
        if (bossPrefab != null && player != null && portalPositions.Length > 0)
        {
            // 보스 객체 생성
            GameObject boss = Instantiate(bossPrefab, transform.position, Quaternion.identity);

            // 보스에 데이터를 전달 (플레이어와 포탈 위치를 설정)
            Boss_1 bossScript = boss.GetComponent<Boss_1>();
            if (bossScript != null)
            {
                bossScript.SetBossData(player, portalPositions);
            }
            else
            {
                Debug.LogError("Boss_1 스크립트가 보스 프리팹에 없습니다.");
            }
        }
        else
        {
            Debug.LogError("필수 변수들(bossPrefab, player, portalPositions)이 설정되지 않았습니다.");
        }
    }
}