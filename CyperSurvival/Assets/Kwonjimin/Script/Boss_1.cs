using System.Collections;
using UnityEngine;

public class Boss_1 : MonoBehaviour
{
    public GameObject player; // 플레이어 객체
    public Transform[] portalPositions; // 미사일 발사 포탈 위치들
    public GameObject shockwavePrefab; // 충격파 프리팹
    public GameObject missilePrefab; // 미사일 프리팹
    public GameObject homingMissilePrefab; // 추적 미사일 프리팹
    public GameObject bossObject; // 보스 객체

    public float speed = 3f; // 보스 속도
    public float attackCooldown = 1f; // 공격 쿨타임
    private float nextAttackTime = 0f;

    private enum AttackPattern { Shockwave, PortalMissile, HomingMissile };
    private AttackPattern[] attackPatterns;
    private int currentPatternIndex = 0;

    void Start()
    {
        // 공격 패턴을 랜덤화
        attackPatterns = (AttackPattern[])System.Enum.GetValues(typeof(AttackPattern));
        ShuffleAttackPatterns();
        bossObject.SetActive(false); // 보스 초기에는 숨겨 놓기
    }

    void Update()
    {
        // 플레이어를 따라가는 코드
        FollowPlayer();

        // 공격 패턴 실행
        if (Time.time > nextAttackTime)
        {
            ExecuteAttackPattern();
            nextAttackTime = Time.time + attackCooldown; // 쿨타임 설정
        }

        // Monster_1 태그를 가진 오브젝트가 0개인지 확인
        CheckMonstersAndSpawnBoss();
    }

    void FollowPlayer()
    {
        // 플레이어 방향으로 보스를 이동시킴
        if (player != null)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    void ExecuteAttackPattern()
    {
        // 현재 공격 패턴에 맞는 공격을 실행
        switch (attackPatterns[currentPatternIndex])
        {
            case AttackPattern.Shockwave:
                LaunchShockwave();
                break;
            case AttackPattern.PortalMissile:
                FirePortalMissiles();
                break;
            case AttackPattern.HomingMissile:
                LaunchHomingMissile();
                break;
        }

        // 다음 공격 패턴으로 이동
        currentPatternIndex = (currentPatternIndex + 1) % attackPatterns.Length;
    }

    void LaunchShockwave()
    {
        GameObject shockwave = Instantiate(shockwavePrefab, transform.position, Quaternion.identity);
        shockwave.transform.localScale = new Vector3(1f, 1f, 1f); // 충격파 크기
        Destroy(shockwave, 2f); // 충격파 2초 후 제거
    }

    void FirePortalMissiles()
    {
        foreach (Transform portal in portalPositions)
        {
            GameObject missile = Instantiate(missilePrefab, portal.position, Quaternion.identity); // 포탈에서 미사일 발사

            // 플레이어 방향 계산
            Vector3 direction = (player.transform.position - portal.position).normalized;

            // 미사일의 방향을 플레이어를 향하도록 설정
            missile.transform.up = direction; // 미사일이 플레이어를 향해 회전하도록 설정

            // 미사일 스크립트 추가
            missile.AddComponent<missile_1>(); // 충돌 처리를 위한 미사일 스크립트 추가
        }
    }

    void LaunchHomingMissile()
    {
        GameObject missile = Instantiate(homingMissilePrefab, transform.position, Quaternion.identity);
        missile.GetComponent<HomingMissile>().SetTarget(player); // 플레이어를 추적하도록 설정
    }

    void CheckMonstersAndSpawnBoss()
    {
        // "Monster_1" 태그를 가진 오브젝트들을 찾아서, 없으면 보스 생성
        if (GameObject.FindGameObjectsWithTag("Monster_1").Length == 0)
        {
            // 보스 오브젝트를 활성화
            if (bossObject != null && !bossObject.activeInHierarchy)
            {
                bossObject.SetActive(true);
            }
        }
    }

    // 공격 패턴 순서를 랜덤화하는 함수
    void ShuffleAttackPatterns()
    {
        for (int i = 0; i < attackPatterns.Length; i++)
        {
            AttackPattern temp = attackPatterns[i];
            int randomIndex = Random.Range(i, attackPatterns.Length);
            attackPatterns[i] = attackPatterns[randomIndex];
            attackPatterns[randomIndex] = temp;
        }
    }
}
