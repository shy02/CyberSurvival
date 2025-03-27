using System.Collections;
using UnityEngine;

public class Boss_1 : MonoBehaviour
{
    public GameObject missilePrefab;
    public GameObject homingMissilePrefab;
    public GameObject bossObject;

    public float speed = 3f;
    public float homingMissileCooldown = 5f;
    private float nextHomingMissileTime = 0f;

    public float portalMissileCooldown = 3f;
    private float nextPortalMissileTime = 0f;

    public float portalShotDelay = 0.3f;
    public int missileCount = 3;

    private Animator animator;

    private GameObject player;
    private Transform[] portalPositions;

    void Start()
    {
        // 애니메이터 컴포넌트 가져오기
        animator = bossObject.GetComponent<Animator>();

        // 이 스크립트는 이제 받지 않음. BossSpawn_1에서 설정
        // player와 portalPositions는 BossSpawn_1에서 할당될 것입니다.
    }

    void Update()
    {
        if (player == null || portalPositions == null || portalPositions.Length == 0)
        {
            return; // 플레이어나 포탈이 없다면 더 이상 실행하지 않음
        }

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

        FlipTowardsPlayer();
    }

    public void SetBossData(GameObject player, Transform[] portalPositions)
    {
        this.player = player;
        this.portalPositions = portalPositions;
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
