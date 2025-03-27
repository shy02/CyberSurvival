using UnityEngine;
using UnityEngine.Scripting;

public class Boss3Core : MonoBehaviour
{
    [SerializeField] float Speed;
    [SerializeField] float radius;
    [SerializeField] float angle = 0f;
    [SerializeField] int Hp = 5;

    SpriteRenderer render;

    private void Start()
    {
        render = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (Hp <= 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            angle += Speed * Time.deltaTime;
            float x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;

            float y = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;

            transform.localPosition = new Vector2(x, y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // 플레이어 총알에 맞는다면
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            render.color = Color.red;
            Hp -= 1;//5번 맞으면 깨짐

            Invoke("OriginColor", 0.5f);
        }
    }

    private void OriginColor()
    {
        render.color = Color.white;
    }

    public void resetCore()
    {
        Hp = 5;
    }
}
