using System.Collections;
using UnityEngine;

public class Boss_1 : MonoBehaviour
{
    public GameObject player;
    public Transform[] portalPositions;
    public GameObject missilePrefab;
    public GameObject homingMissilePrefab;
    public GameObject bossObject;
    public GameObject level2Spawner;  // level2Spawner를 위한 변수

    public float speed = 3f;
    public float homingMissileCooldown = 5f;
    private float nextHomingMissileTime = 0f;

    public float portalMissileCooldown = 3f;
    private float nextPortalMissileTime = 0f;

    public float portalShotDelay = 0.3f;
    public int missileCount = 3;

    private Animator animator;

    void Start()
    {
        // 애니메이터 컴포넌트 가져오기
        animator = bossObject.GetComponent<Animator>();

        // level2Spawner가 삭제되었을 때 보스와 포탈 활성화
        StartCoroutine(WaitForLevel2SpawnerDeletion());
    }

    void Update()
    {
        FollowPlayer();

        if (Time.time > nextPortalMissileTime)
        {
            StartCoroutine(FirePortalMissiles());
            nextPortalMissileTime = Time.time + portalMissileCooldown;
        }

        if (Time.time > nextHomingMissileTime)
        {
            LaunchHomingMissile();
            nextHomingMissileTime = Time.time + homingMissileCooldown;
        }

        // 플레이어와의 상대적인 x축 위치에 따라 보스의 방향을 바꿔줌
        FlipTowardsPlayer();
    }

    IEnumerator WaitForLevel2SpawnerDeletion()
    {
        // level2Spawner가 null이 될 때까지 기다림
        while (level2Spawner != null)
        {
            yield return null;  // 매 프레임마다 level2Spawner가 삭제되었는지 체크
        }

        // level2Spawner가 사라지면 보스와 포탈을 활성화
        ActivateBossAndPortals();
    }

    void ActivateBossAndPortals()
    {
        // 보스를 활성화
        bossObject.SetActive(true);

        // 포탈을 모두 활성화
        foreach (Transform portal in portalPositions)
        {
            portal.gameObject.SetActive(true);
        }

        // 기본 애니메이션 실행
        animator.SetBool("Run", true);
    }

    void FollowPlayer()
    {
        if (player != null)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    IEnumerator FirePortalMissiles()
    {
        for (int i = 0; i < missileCount; i++)
        {
            foreach (Transform portal in portalPositions)
            {
                if (portal == null) continue;

                GameObject missile = Instantiate(missilePrefab, portal.position, Quaternion.identity);
                missile_1 missileScript = missile.GetComponent<missile_1>();

                if (missileScript != null)
                    missileScript.SetDirection(player.transform.position);
                else
                    Debug.LogError("미사일 프리팹에 missile_1 스크립트가 없습니다!");

                yield return new WaitForSeconds(portalShotDelay);
            }
        }
        yield return new WaitForSeconds(3f);
    }

    void LaunchHomingMissile()
    {
        // 공격 애니메이션 실행
        animator.SetBool("attack", true);

        // 호밍 미사일 발사
        GameObject missile = Instantiate(homingMissilePrefab, transform.position, Quaternion.identity);
        missile.GetComponent<HomingMissile>().SetTarget(player);

        // 미사일 발사 후 attack 애니메이션을 종료
        Invoke("StopAttackAnimation", 0.2f); // 공격 애니메이션이 0.2초 후에 종료되도록 설정
    }

    void StopAttackAnimation()
    {
        // attack 애니메이션 종료
        animator.SetBool("attack", false);
    }

    void FlipTowardsPlayer()
    {
        // 보스의 x축이 플레이어의 x축보다 작으면 보스가 반전하도록 설정
        if (player != null)
        {
            Vector3 playerPos = player.transform.position;

            // 보스의 위치와 플레이어의 위치를 비교하여 방향을 변경
            if (transform.position.x < playerPos.x)
            {
                // 플레이어가 오른쪽에 있으면, 오른쪽으로 보스가 보이도록 설정
                if (transform.localScale.x < 0)
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                // 플레이어가 왼쪽에 있으면, 왼쪽으로 보스가 보이도록 설정
                if (transform.localScale.x > 0)
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
    }
}