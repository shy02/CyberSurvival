using UnityEngine;

public class Bumerang : MonoBehaviour
{
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && spriteRenderer.color.a > 0)
        {
            Player player = other.GetComponent<Player>();
            player.GetDamage(10);
        }
        if (other.CompareTag("Wall"))
        {
            SetTransparency(0f); // 투명하게 설정
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
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
