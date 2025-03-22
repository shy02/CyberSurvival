using System.Net.WebSockets;
using UnityEngine;

public class Boss8Bullet_3 : MonoBehaviour
{
    [SerializeField] float Speed = 1f;
    [SerializeField] float Height = 1f;
    [SerializeField] int flipY;

    float StartTime;
    Vector3 pos;
    private void Start()
    {
        pos = transform.position;
        StartTime = Time.time;
    }
    void Update()
    {
        
        //Left8();

    }

    public void OnMove()
    {

    }

    private void Left8()
    {
        /* //pos.x = Mathf.Cos(Time.time * 360 * Mathf.Deg2Rad);
         pos.x += Speed * Time.deltaTime;// 그냥 왼쪽으로 직진하는 
         //transform.position += transform.forward * Speed * Time.deltaTime;

         pos.y = Mathf.Sin((Time.time - StartTime) * 360 * Mathf.Deg2Rad) * flipY;

         transform.position = pos;*/
        pos += -transform.right * Speed * Time.deltaTime;
        transform.position = pos;
        /*Vector3 pos = transform.position;

        // 바라보는 방향으로 직진 (Z축 기준 이동)
        pos += transform.forward * Speed * Time.deltaTime;

        // 좌우로 흔들리면서 이동 (지그재그)
        float zigzag = Mathf.Sin((Time.time - StartTime) * 5f) * 0.5f; // 진동 주기 & 크기 조절
        pos += transform.right * zigzag;  // 오브젝트의 오른쪽 방향을 기준으로 좌우 이동

        transform.position = pos;*/
    }//보류

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
