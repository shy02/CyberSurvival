using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Vector3 BossSpawn = Vector3.zero;
    public GameObject BossPrefab;



     void Start()
    {
        Instantiate(BossPrefab, BossSpawn, Quaternion.identity);
    }

    void Update()
    {
        
    }
}
