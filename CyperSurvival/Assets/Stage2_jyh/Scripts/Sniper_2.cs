using System.Collections;
using UnityEngine;

public class Sniper_2 : MonoBehaviour
{
    //색상 전환에 걸리는 시간
    [SerializeField]
    float lerpTime = 0.1f;

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        StartCoroutine("ColorLerpLoop");
    }

    void Update()
    {

    }

    //색상 전환 루프 코루틴
    IEnumerator ColorLerpLoop()
    {
        while (true)
        {
            yield return StartCoroutine(ColorLerp(new Color(0,0,0,0), Color.red));
            yield return StartCoroutine(ColorLerp(Color.red, new Color(0, 0, 0, 0)));
        }
    }

    //색상 전환 코루틴
    IEnumerator ColorLerp(Color startColor, Color endColor)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / lerpTime;
            spriteRenderer.color = Color.Lerp(startColor, endColor, percent);
            yield return null;
        }
    }

}
