using UnityEngine;

public class Bumerang_2 : MonoBehaviour
{
    [SerializeField] private int damage;

    public float speed = 1f;
    public Transform bossTransform; // 보스의 Transform을 참조
    private float angle = 0f;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        if (bossTransform != null)
        {
            Vector3 centerPosition = bossTransform.position; // 보스의 위치를 중심으로 설정
            angle += speed * Time.deltaTime;
            float x = Mathf.Cos(angle) * 5f;
            float y = Mathf.Sin(angle) * 5f;
            transform.position = centerPosition + new Vector3(x, y, 0);
        }
    }

    public void SetInitialAngle(float initialAngle)
    {
        angle = initialAngle;
    }



    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && spriteRenderer.color.a > 0)
        {
            collider.GetComponent<Player>().TakeDamage(damage);
        }
        if (collider.CompareTag("Wall"))
        {
            SetTransparency(0f); // 투명하게 설정
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Wall"))
        {
            SetTransparency(1f); // 다시 보이게 설정
        }
    }

    private void SetTransparency(float alpha)
    {
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }
}
