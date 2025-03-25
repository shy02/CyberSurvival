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
                spawnArray[i] = enemy[enmNum];  // �������� ������ �����ؼ�
            }
                         

            foreach(GameObject spawn in spawnArray)
            {
                int posNum = Random.Range(0, spawnPosition.Length);
                Instantiate(spawn, spawnPosition[posNum].position, Quaternion.identity);    // ���� ���� ����Ʈ���� ���ʷ� ����
                yield return new WaitForSeconds(0.4f);
            }

            yield return new WaitForSeconds(spawnInterval);
        }
        
    }
}
