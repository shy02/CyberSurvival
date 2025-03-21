using UnityEngine;

public class stage2_SpawnManager : MonoBehaviour
{

    [Header("���� ����")]
    public Transform Bosspos;
    public GameObject BossPrefab;
    [Header("�⺻���� ����")]
    public GameObject[] EnemyPrefab;
    public Transform[] Spawners;
    public GameObject SpawnEffect;

    //�⺻ �� ��ȯ
    public float spawnTime = 0.2f;
    float deltaTime = 0f;

    //���� ��ȯ
    bool isBossSpawn = false;


    void Start()
    {
        deltaTime = spawnTime;
    }

    void Update()
    {
        if (isBossSpawn == false)
        {
            deltaTime += Time.deltaTime;
            if (deltaTime >= spawnTime)
            {
                SpawnEnemy();
                deltaTime = 0f;
            }
        }
    }

    void SpawnEnemy()
    {
        while (true)
        {
            int random = Random.Range(0, EnemyPrefab.Length);
            int randomSpawner = Random.Range(0, Spawners.Length);
            if (EnemyPrefab[random] != null && Spawners[randomSpawner] != null)
            {
                Instantiate(EnemyPrefab[random], Spawners[randomSpawner].position, Quaternion.identity);
                GameObject effect = Instantiate(SpawnEffect, Spawners[randomSpawner].position, Quaternion.identity);
                Destroy(effect, 0.5f);
                break;
            }

            if (SpawnerCheck() == false)    //��� �����ʰ� ������
            {
                isBossSpawn = true;
                Invoke("BossSpawn", 5f);    //5�� �� ���� ��ȯ
                break;
            }
        }
    }

    bool SpawnerCheck()
    {
        bool is_spawner = false;

        for (int i = 0; i < Spawners.Length; i++)
        {
            if (Spawners[i] != null)
            {
                is_spawner = true;
            }
        }

        return is_spawner;

    }

    void BossSpawn()
    {
        if (isBossSpawn == false)
            return;

        Instantiate(BossPrefab, Bosspos.position, Quaternion.identity);
    }

}
