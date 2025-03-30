using System.Collections;
using UnityEngine;

public class FallAttack : MonoBehaviour
{
    public float riseHeight = 5f;
    public float riseSpeed = 10f;
    public float fallSpeed = 15f;
    public float shadowFollowDuration = 2f;
    public float damageRadius = 1.5f;
    public GameObject shadowPrefab;
    public int damageAmount = 3;

    private Vector3 originalPosition;
    private Vector3 shadowTargetPosition;
    private GameObject shadowObject;
    private bool isFalling = false;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
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
        animator.SetBool("isJump", true);
        // 1. 위로 솟구치기
        originalPosition = transform.position;
        Vector3 riseTarget = originalPosition + Vector3.up * riseHeight;
        GetComponent<Collider2D>().enabled = false;
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
        }

        isFalling = true;
        animator.SetBool("isLand", true);

        if (duration == 1) animator.SetBool("isStun", false);
        else animator.SetBool("isStun", true);

            while (Vector3.Distance(transform.position, shadowTargetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, shadowTargetPosition, fallSpeed * Time.deltaTime);
                yield return null;
            }

        Destroy(shadowObject);

        // 5. 착지 후 충돌 활성화 및 대미지 처리
        GetComponent<Collider2D>().enabled = true;
        ApplyDamageAndKnockback();

        CameraShake cameraShake = FindObjectOfType<CameraShake>();
        if (cameraShake != null)
        {
            cameraShake.Shake(0.1f, 0.3f);
        }
        animator.SetBool("isJump", false);
        animator.SetBool("isLand", false);
    }

    private IEnumerator FollowPlayer(GameObject shadow, float duration)
    {
        float timer = 0f;
        SpriteRenderer sr = shadow.GetComponent<SpriteRenderer>();
        Color shadowColor = sr.color;
        shadowColor.a = 0f;
        sr.color = shadowColor;

        float maxAlpha = 0.7f;

        while (timer < duration)
        {
            shadow.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
            shadowColor.a = Mathf.Lerp(0f, maxAlpha, timer / duration);
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
