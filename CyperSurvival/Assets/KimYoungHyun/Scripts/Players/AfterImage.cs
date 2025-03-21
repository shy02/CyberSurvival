using UnityEngine;
using System.Collections;

public class AfterImage : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color startColor;
    public float fadeSpeed = 5f; // 잔상이 사라지는 속도

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;
    }

    public void Initialize(Sprite sprite, Vector3 position)
    {
        transform.position = position;
        spriteRenderer.sprite = sprite;
        spriteRenderer.color = startColor;
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        Color color = spriteRenderer.color;
        while (color.a > 0)
        {
            color.a -= Time.deltaTime * fadeSpeed;
            spriteRenderer.color = color;
            yield return null;
        }
        Destroy(gameObject);
    }
}
