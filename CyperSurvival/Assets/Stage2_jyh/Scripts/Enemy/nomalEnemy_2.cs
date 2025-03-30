using UnityEngine;
using UnityEngine.U2D;

public class nomalEnemy_2 : MonoBehaviour
{
    [Header("공격 설정")]
    [SerializeField] private float attackCool;  //쿨타임
    [SerializeField] private float attackRange; //공격범위
    [SerializeField] private int bulletCount;   //총알갯수
    [Header("공격 참조")]
    public GameObject bullet;
    public Transform firePos;
    [Header("사운드")]
    [SerializeField]private AudioClip attackSound;
    //참조
    private EnemyMove_2 moveSc;
    GameObject player;
    Animator anim;
    //공격 변수
    Vector3 dir = Vector3.zero; //총알 방향
    float angle = 0;    //총알 사이 각도
    float delta = 0;    //쿨타임 변수
    bool isAttack = false;  //공격중인지

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
        moveSc = GetComponent<EnemyMove_2>();
    }

    void Update()
    {
        if (player == null) return;

        Debug.Log("공격중인지: " + isAttack);

        float distance = Vector2.Distance(player.transform.position, transform.position);

        if (distance <= attackRange && isAttack == false)
        {
            //이동 멈춤
            moveSc.StopMove();
            anim.SetBool("isMove", false);

            delta -= Time.deltaTime;
            if (delta <= 0)
            {
                anim.SetTrigger("Attack");
                delta = attackCool;
            }
        }
        else if (distance > attackRange && isAttack == false)
        {
            delta = 0;

            moveSc.StartMove();
            anim.SetBool("isMove", true);
        }
        else if (isAttack == true)
        {
            moveSc.StopMove();
        }

    }

    void AttackOnOff()
    {
        isAttack = !isAttack;
    }

    //애니메이션에서 호출
    void Attack1()
    {
        //움직일때는 공격하지 않음
        if (moveSc.canMove == true)
            return;

        dir = player.transform.position - transform.position;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        for (int i = 0; i < bulletCount; i++)
        {
            Vector3 bulletDir = CalculateBulletDirection(i, bulletCount, angle);
            GameObject go = Instantiate(bullet, firePos.position, Quaternion.identity);
            go.GetComponent<EnemyBullet_2>().SetDir(bulletDir);
        }
        SoundMgr_2.instance.OneShot(attackSound, 1f);
    }

    void Attack2()
    {
        //움직일때는 공격하지 않음
        if (moveSc.canMove == true)
            return;

        //좌우반전
        if (dir.x < 0)
            angle += 30;
        else
            angle -= 30;

        for (int i = 0; i < bulletCount; i++)
        {
            Vector3 bulletDir = CalculateBulletDirection(i, bulletCount, angle);
            GameObject go = Instantiate(bullet, firePos.position, Quaternion.identity);
            go.GetComponent<EnemyBullet_2>().SetDir(bulletDir);
        }
        SoundMgr_2.instance.OneShot(attackSound, 1f);
    }

    void Attack3()
    {
        //움직일때는 공격하지 않음
        if (moveSc.canMove == true)
            return;

        //좌우반전
        if (dir.x < 0)
            angle -= 60;
        else
            angle += 60;

        for (int i = 0; i < bulletCount; i++)
        {
            Vector3 bulletDir = CalculateBulletDirection(i, bulletCount, angle);
            GameObject go = Instantiate(bullet, firePos.position, Quaternion.identity);
            go.GetComponent<EnemyBullet_2>().SetDir(bulletDir);
        }
        SoundMgr_2.instance.OneShot(attackSound, 1f);
    }


    Vector3 CalculateBulletDirection(int index, int totalBullets, float baseAngle)
    {
        float angleStep = 4f;
        float angleOffset = (totalBullets % 2 == 0) ? 2f : 0f;   //갯수가 짝수면 2.5도씩 오프셋
        float currentAngle = baseAngle + angleOffset + angleStep * (index - totalBullets / 2);

        float dirX = Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float dirY = Mathf.Sin(currentAngle * Mathf.Deg2Rad);

        return new Vector3(dirX, dirY, 0);
    }


}
