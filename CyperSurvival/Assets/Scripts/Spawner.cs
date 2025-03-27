using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPosition;
    [SerializeField] GameObject[] enemy;
    [SerializeField] GameObject enemyBike;
    [SerializeField] GameObject boss;
    [SerializeField] int count = 0;

    public float spawnInterval = 15f;

    private int a = 10, b = 15;
    private bool bikeSpawn = false;
    private bool bossSpawn = false;

    private void Start()
    {
        StartCoroutine("StartSpawn");
    }
    private void Update()
    {
        if (count >= 3) { bikeSpawn = true; a = 8; b = 10; }
        if (count >= 6) bossSpawn = true;
    }

    private IEnumerator StartSpawn()
    {
        yield return new WaitForSeconds(5.0f);
        StartCoroutine("SpawnEnemy");
        if (GameObject.FindGameObjectWithTag("Boss") != null) gameObject.SetActive(false);
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            int value = Random.Range(a, b);

            GameObject[] spawnArray = new GameObject[value];

            for (int i = 0; i < value; i++)
            {
                int enmNum = Random.Range(0, enemy.Length);
                spawnArray[i] = enemy[enmNum];
            }

            foreach (GameObject spawn in spawnArray)
            {
                int posNum = Random.Range(0, spawnPosition.Length);

                if (count >= 6)
                {
                    if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
                    {
                        Instantiate(boss, spawnPosition[0].position, Quaternion.identity);
                    }
                }
                else
                {
                    Instantiate(spawn, spawnPosition[posNum].position, Quaternion.identity);
                }
                yield return new WaitForSeconds(0.4f);
            }
            if (bikeSpawn && !bossSpawn)
            {
                Instantiate(enemyBike, spawnPosition[1].position, Quaternion.identity);
                Instantiate(enemyBike, spawnPosition[3].position, Quaternion.identity);
            }
            count++;
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
