using UnityEngine;
using UnityEngine.UI;

public class SpawnManager_2 : MonoBehaviour
{

    [Header("보스 스폰")]
    public Transform Bosspos;
    public GameObject BossPrefab;
    public Text WarningText;

    [Header("기본몬스터 스폰")]
    public GameObject[] EnemyPrefab;
    public Transform[] Spawners;
    public GameObject SpawnEffect;

    //기본 적 소환
    public float spawnTime = 0.2f;
    float deltaTime = 0f;

    //보스 소환
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

            if (SpawnerCheck() == false)    //모든 스포너가 없을때
            {
                isBossSpawn = true;
                TextWarning();
                Invoke("BossSpawn", 5f);    //5초 후 보스 소환
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

    //소환전 텍스트
    void TextWarning()
    {
        WarningText.gameObject.SetActive(true);
        Invoke("TextWarningOff", 5f);

    }
    void TextWarningOff()
    {
        WarningText.gameObject.SetActive(false);
    }


}
