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
         pos.x += Speed * Time.deltaTime;// �׳� �������� �����ϴ� 
         //transform.position += transform.forward * Speed * Time.deltaTime;

         pos.y = Mathf.Sin((Time.time - StartTime) * 360 * Mathf.Deg2Rad) * flipY;

         transform.position = pos;*/
        pos += -transform.right * Speed * Time.deltaTime;
        transform.position = pos;
        /*Vector3 pos = transform.position;

        // �ٶ󺸴� �������� ���� (Z�� ���� �̵�)
        pos += transform.forward * Speed * Time.deltaTime;

        // �¿�� ��鸮�鼭 �̵� (�������)
        float zigzag = Mathf.Sin((Time.time - StartTime) * 5f) * 0.5f; // ���� �ֱ� & ũ�� ����
        pos += transform.right * zigzag;  // ������Ʈ�� ������ ������ �������� �¿� �̵�

        transform.position = pos;*/
    }//����

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
