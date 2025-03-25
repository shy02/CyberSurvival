using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPosition;
    [SerializeField] GameObject[] enemy;

    public float spawnInterval = 15;

    private void Start()
    {
        StartCoroutine("StartSpawn");
    }

    private IEnumerator StartSpawn()
    {
        yield return new WaitForSeconds(5.0f);
        StartCoroutine("SpawnEnemy");
    }
    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            int value = Random.Range(10, 15);

            GameObject[] spawnArray = new GameObject[value];

            for (int i = 0; i < value; i++) 
            {
                int enmNum = Random.Range(0, enemy.Length);
                spawnArray[i] = enemy[enmNum];  // 랜덤으로 조합을 저장해서
            }
                         

            foreach(GameObject spawn in spawnArray)
            {
                int posNum = Random.Range(0, spawnPosition.Length);
                Instantiate(spawn, spawnPosition[posNum].position, Quaternion.identity);    // 랜덤 스폰 포인트에서 차례로 스폰
                yield return new WaitForSeconds(0.4f);
            }

            yield return new WaitForSeconds(spawnInterval);
        }
        
    }
}
