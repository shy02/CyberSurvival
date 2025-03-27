using System.Collections;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public int RageHealth = 300;
    private bool isEnraged = false;
    private int attackCount = 0;

    private ShotgunAttack shotgunAttack;
    private MachineGunAttack machinegunAttack;
    private DashAttack dashAttack;
    private FallAttack fallAttack;
    private int BossHealth;

    private EnemyAI enemyAI; // EnemyAI 스크립트 참조
    private Rigidbody2D rb;
    private bool isAttacking = false; // 공격 중인지 여부를 추적하는 변수
    private float attackCooldown = 0f; // 공격 쿨타임 관리
    private int fallAttackCount = 6;

    private void Start()
    {
        shotgunAttack = GetComponent<ShotgunAttack>();
        machinegunAttack = GetComponent<MachineGunAttack>();
        dashAttack = GetComponent<DashAttack>();
        fallAttack = GetComponent<FallAttack>();
        enemyAI = GetComponent<EnemyAI>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        BossHealth = GetComponent<EnemyDamage_3>().Hp;

        // 보스가 광폭화 상태로 전환
        if (BossHealth <= RageHealth && !isEnraged)
        {
            isEnraged = true;
            fallAttackCount = 10;
            machinegunAttack.fireDuration = 3f;
            machinegunAttack.fireRate = 0.06f;
            Debug.Log("보스가 광폭화 상태로 변경됨!");
        }

        if (isAttacking) return;

        // 공격 쿨타임이 남아있는 경우
        if (attackCooldown > 0f)
        {
            attackCooldown -= Time.deltaTime;
            return; // 쿨타임이 남아있으면 대기
        }

        // 공격 순서
        PerformRandomAttack();
    }

    private void PerformRandomAttack()
    {
        if (attackCount >= fallAttackCount && !isAttacking)
        {
            Debug.Log("낙하 공격 수행!");
            StartCoroutine(PerformFallAttack());
        }
        else
        {
            float rand = Random.value;
            if (rand < 0.4f)
            {
                StartCoroutine(PerformShotgunAttack());
            }
            else if (rand < 0.8f)
            {
                StartCoroutine(PerformMachinegunAttack());
            }
            else
            {
                StartCoroutine(PerformDashAttack());
            }
        }
    }

    private IEnumerator PerformFallAttack()
    {
        if (isAttacking) yield break;

        isAttacking = true;
        Debug.Log("낙하 공격 시작");

        if (!isEnraged) fallAttack.FallingAttack();

        if (isEnraged)
        {
            fallAttack.FallingAttack(1f);
            yield return new WaitForSeconds(2.44f);
            fallAttack.FallingAttack(1f);
            Debug.Log("광폭화 상태 - 연속 낙하 공격 수행!");
        }
        yield return new WaitForSeconds(2.4f);
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        yield return new WaitForSeconds(4.5f);
        rb.bodyType = RigidbodyType2D.Dynamic;

        attackCount = 0;  // 낙하 공격 시 카운트 초기화
        // 낙하 공격 종료 처리
        isAttacking = false;
        attackCooldown = 0.5f; // 쿨타임 설정
        Debug.Log("낙하 공격 완료");
    }

    private IEnumerator PerformShotgunAttack()
    {
        isAttacking = true;
        Debug.Log("샷건 공격 실행");

        shotgunAttack.FireShotgun();

        if (isEnraged)
        {
            yield return new WaitForSeconds(0.3f);
            shotgunAttack.FireShotgun();
            attackCooldown = 0.5f;  // 광폭화 상태일 때 쿨타임
        }
        else
        {
            attackCooldown = 1f;  // 기본 쿨타임
        }

        yield return new WaitForSeconds(attackCooldown);
        attackCount++;
        Debug.Log($"샷건 공격 완료 - attackCount 증가: {attackCount}");

        isAttacking = false;
    }

    private IEnumerator PerformMachinegunAttack()
    {
        isAttacking = true;
        Debug.Log("머신건 공격 실행");

        machinegunAttack.StartFiring();

        if (isEnraged)
        {
            attackCooldown = 2.7f;
        }
        else
        {
            attackCooldown = 3.6f;
        }

        yield return new WaitForSeconds(attackCooldown);
        attackCount++;
        Debug.Log($"머신건 공격 완료 - attackCount 증가: {attackCount}");

        isAttacking = false;
    }

    private IEnumerator PerformDashAttack()
    {
        isAttacking = true;
        Debug.Log("대시 공격 실행");

        dashAttack.Dash();

        if (isEnraged)
        {
            attackCooldown = 0.4f;
        }
        else
        {
            attackCooldown = 0.8f;
        }

        yield return new WaitForSeconds(attackCooldown);

        if (isEnraged)
        {
            shotgunAttack.FireShotgun();
            attackCooldown = 0.4f;
            yield return new WaitForSeconds(attackCooldown);
        }

        attackCount++;
        Debug.Log($"대시 공격 완료 - attackCount 증가: {attackCount}");

        isAttacking = false;
    }
}
