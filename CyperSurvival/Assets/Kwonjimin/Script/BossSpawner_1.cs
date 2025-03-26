using UnityEngine;

public class BossSpawner_1 : MonoBehaviour
{
    public GameObject bossPrefab; // ���� ������
    public GameObject player; // �÷��̾�
    public Transform[] portalPositions; // ��Ż ��ġ

    public void SpawnBoss()
    {
        if (bossPrefab != null && player != null && portalPositions.Length > 0)
        {
            // ���� ��ü ����
            GameObject boss = Instantiate(bossPrefab, transform.position, Quaternion.identity);

            // ������ �����͸� ���� (�÷��̾�� ��Ż ��ġ�� ����)
            Boss_1 bossScript = boss.GetComponent<Boss_1>();
            if (bossScript != null)
            {
                bossScript.SetBossData(player, portalPositions);
            }
            else
            {
                Debug.LogError("Boss_1 ��ũ��Ʈ�� ���� �����տ� �����ϴ�.");
            }
        }
        else
        {
            Debug.LogError("�ʼ� ������(bossPrefab, player, portalPositions)�� �������� �ʾҽ��ϴ�.");
        }
    }
}