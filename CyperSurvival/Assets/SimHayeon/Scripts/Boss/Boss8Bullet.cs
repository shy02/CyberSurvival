using System.Net.WebSockets;
using UnityEngine;

public class Boss8Bullet : MonoBehaviour
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
        //pos.x = Mathf.Cos(Time.time * 360 * Mathf.Deg2Rad);
        pos.x -= Speed* Time.deltaTime;
        //transform.position += transform.forward * Speed * Time.deltaTime;

        pos.y = Mathf.Sin((Time.time - StartTime) * 360 * Mathf.Deg2Rad) * Height * flipY ;

        transform.position = pos;

    }
}
