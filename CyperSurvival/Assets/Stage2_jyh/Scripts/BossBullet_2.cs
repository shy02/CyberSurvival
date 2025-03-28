using System.Data;
using UnityEngine;

public class BossBullet_2 : MonoBehaviour
{
    [SerializeField] private int damage;

    GameObject player;
    Vector3 moveDir = Vector3.zero;
    Vector3 rotation = Vector3.zero;
    float angle = 0;
    float moveSpeed = 7;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        //이동방향
        Vector3 dir = player.transform.position - transform.position;
        moveDir = dir.normalized;
        //캐릭터 바라보게하기
        angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
        rotation = new Vector3(0, 0, angle);
        transform.eulerAngles = rotation;
    }


    void Update()
    {
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.GetComponent<Player>().TakeDamage(damage);

            Destroy(gameObject);
        }
        if (collision.CompareTag("Wall"))
            Destroy(gameObject);
    }

}
