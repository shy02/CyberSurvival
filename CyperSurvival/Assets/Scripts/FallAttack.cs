using System.Collections;
using UnityEngine;

public class FallAttack : MonoBehaviour
{
    public float riseHeight = 5f;  // �ڱ�ġ�� ����
    public float riseSpeed = 10f;  // ��� �ӵ�
    public float fallSpeed = 15f;  // �ϰ� �ӵ�
    public float shadowFollowDuration = 2f;  // �׸��� ���� �ð�
    public float damageRadius = 1.5f;  // �浹 ���� ����
    public GameObject shadowPrefab;  // �׸��� ������Ʈ ������
    public int damageAmount = 3;  // �÷��̾�� �� �����

    private Vector3 originalPosition;  // ���� ��ġ
    private Vector3 shadowTargetPosition;  // �׸��� ��ġ (�ϰ� ��ǥ)
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
        // 1. ���� �ڱ�ġ��
        originalPosition = transform.position;
        Vector3 riseTarget = originalPosition + Vector3.up * riseHeight;
        GetComponent<Collider2D>().enabled = false;  // �浹 ��Ȱ��ȭ
        while (Vector3.Distance(transform.position, riseTarget) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, riseTarget, riseSpeed * Time.deltaTime);
            yield return null;
        }

        // 2. �׸��� ������Ʈ ����
        if (shadowPrefab)
        {
            shadowObject = Instantiate(shadowPrefab, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);
            StartCoroutine(FollowPlayer(shadowObject, duration));
        }

        // 3. ���� �ð� ���
        yield return new WaitForSeconds(duration);

        // 4. �׸��� ��ġ ���� �� �ϰ� ����
        if (shadowObject)
        {
            shadowTargetPosition = shadowObject.transform.position;
            Destroy(shadowObject);  // �׸��� ����
        }

        isFalling = true;
        while (Vector3.Distance(transform.position, shadowTargetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, shadowTargetPosition, fallSpeed * Time.deltaTime);
            yield return null;
        }

        // 5. ���� �� �浹 Ȱ��ȭ �� ����� ó��
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
                // �о�� ����
                Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 knockbackDirection = (hit.transform.position - transform.position).normalized;
                    rb.AddForce(knockbackDirection * 500f);
                }

                // ����� ����
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
