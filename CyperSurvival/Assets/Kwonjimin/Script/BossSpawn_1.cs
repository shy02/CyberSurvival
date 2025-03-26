using System.Collections;
using UnityEngine;

public class BossSpawn_1 : MonoBehaviour
{
    public GameObject bossPrefab;  // ���� ������
    public Transform[] portalPositions;  // ��Ż ��ġ��
    public GameObject level2Spawner;  // level2Spawner ����
    public GameObject player;  // �÷��̾�

    private GameObject spawnedBoss;  // ������ ����

    void Start()
    {
        // level2Spawner�� ������� ������ ��Ż�� Ȱ��ȭ
        StartCoroutine(WaitForLevel2SpawnerDeletion());
    }

    IEnumerator WaitForLevel2SpawnerDeletion()
    {
        // level2Spawner�� null�� �ǰų� ��Ȱ��ȭ �� ������ ��ٸ�
        while (level2Spawner != null && level2Spawner.activeInHierarchy)
        {
            yield return null;  // �� �����Ӹ��� level2Spawner�� �����Ǿ����� üũ
        }

        // level2Spawner�� ������� ������ ��Ż�� Ȱ��ȭ
        SpawnBoss();
    }

    void SpawnBoss()
    {
        // ������ �����ϰ� ��Ż�� Ȱ��ȭ
        spawnedBoss = Instantiate(bossPrefab, transform.position, Quaternion.identity);

        // Boss_1 ��ũ��Ʈ�� Ȱ��ȭ�Ǿ� �־�� ��Ż�� Ȱ��ȭ�� �� �ִ�.
        Boss_1 bossScript = spawnedBoss.GetComponent<Boss_1>();
        if (bossScript != null)
        {
            // ������ �÷��̾�� ��Ż ��ġ ����
            bossScript.SetBossData(player, portalPositions);

            // ��Ż�� ��� Ȱ��ȭ
            foreach (Transform portal in portalPositions)
            {
                portal.gameObject.SetActive(true);
            }
        }
        else
        {
            Debug.LogError("Boss_1 ��ũ��Ʈ�� ���� ������Ʈ�� �����ϴ�.");
        }
    }
}
