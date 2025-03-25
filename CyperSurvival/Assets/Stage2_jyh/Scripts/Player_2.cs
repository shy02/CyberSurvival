using UnityEngine;

public class Player_2 : MonoBehaviour
{
    float x;
    float y;
    Vector3 moveDir = Vector3.zero;
    public float moveSpeed = 3;
    int HP = 100;

    //스포너 데미지 주기
    GameObject[] gameObjects;
    Spawner_2[] spawners;

    void Start()
    {
        gameObjects = GameObject.FindGameObjectsWithTag("Spawner");
        spawners = new Spawner_2[gameObjects.Length];
        for (int i = 0; i < spawners.Length; i++)
        {
            spawners[i] = gameObjects[i].GetComponent<Spawner_2>();
        }
    }

    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        Vector3 vec = new Vector3(x, y, 0);
        moveDir = vec.normalized;

        transform.Translate(moveDir * moveSpeed * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            DamageSpawner();
        }

    }

    public void GetDamage(int damage)
    {
        HP -= damage;
        Debug.Log("player : " + HP);
    }

    void DamageSpawner()
    {
        while (true)
        {
            int random = Random.Range(0, spawners.Length);

            if (spawners[random] != null)
            {
                spawners[random].TakeDamage(10);
                break;
            }
        }
    }


}
