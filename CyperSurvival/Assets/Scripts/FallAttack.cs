using System.Collections;
using UnityEngine;

public class FallAttack : MonoBehaviour
{
    public float riseHeight = 5f;  // 솟구치는 높이
    public float riseSpeed = 10f;  // 상승 속도
    public float fallSpeed = 15f;  // 하강 속도
    public float shadowFollowDuration = 2f;  // 그림자 유도 시간
    public float damageRadius = 1.5f;  // 충돌 감지 범위
    public GameObject shadowPrefab;  // 그림자 오브젝트 프리팹
    public int damageAmount = 3;  // 플레이어에게 줄 대미지

    private Vector3 originalPosition;  // 원래 위치
    private Vector3 shadowTargetPosition;  // 그림자 위치 (하강 목표)
    private GameObject shadowObject;
    private bool isFalling = false;

    public void FallingAttack()
    {
        StartCoroutine(PerformFallAttack(shadowFollowDuration));
    }
    public void FallingAttack(float duration)
    {
        StartCoroutine(PerformFallAttack(duration));
    }

    private IEnumerator PerformFallAttack(float duration)
    {
        // 1. 위로 솟구치기
        originalPosition = transform.position;
        Vector3 riseTarget = originalPosition + Vector3.up * riseHeight;
        GetComponent<Collider2D>().enabled = false;  // 충돌 비활성화
        while (Vector3.Distance(transform.position, riseTarget) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, riseTarget, riseSpeed * Time.deltaTime);
            yield return null;
        }

        // 2. 그림자 오브젝트 생성
        if (shadowPrefab)
        {
            shadowObject = Instantiate(shadowPrefab, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);
            StartCoroutine(FollowPlayer(shadowObject, duration));
        }

        // 3. 유도 시간 대기
        yield return new WaitForSeconds(duration);

        // 4. 그림자 위치 저장 후 하강 시작
        if (shadowObject)
        {
            shadowTargetPosition = shadowObject.transform.position;
            Destroy(shadowObject);  // 그림자 제거
        }

        isFalling = true;
        while (Vector3.Distance(transform.position, shadowTargetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, shadowTargetPosition, fallSpeed * Time.deltaTime);
            yield return null;
        }

        // 5. 착지 후 충돌 활성화 및 대미지 처리
        GetComponent<Collider2D>().enabled = true;
        ApplyDamageAndKnockback();

    }

    private IEnumerator FollowPlayer(GameObject shadow, float duration)
    {
        float timer = 0f;
        SpriteRenderer sr = shadow.GetComponent<SpriteRenderer>();
        Color shadowColor = sr.color;
        shadowColor.a = 0f;
        sr.color = shadowColor;

        float maxAlpha = 0.7f;

        while (timer < shadowFollowDuration)
        {
            shadow.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
            shadowColor.a = Mathf.Lerp(0f, maxAlpha, timer / shadowFollowDuration);
            sr.color = shadowColor;

            timer += Time.deltaTime;
            yield return null;
        }
    }


    private void ApplyDamageAndKnockback()
    {
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, damageRadius);
        foreach (Collider2D hit in hitObjects)
        {
            if (hit.CompareTag("Player"))
            {
                // 밀어내기 적용
                Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 knockbackDirection = (hit.transform.position - transform.position).normalized;
                    rb.AddForce(knockbackDirection * 500f);
                }

                // 대미지 적용
                PlayerDamage playerDamage = hit.GetComponent<PlayerDamage>();
                if (playerDamage != null)
                {
                    playerDamage.GetDamage(damageAmount);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}
