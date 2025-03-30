using UnityEngine;

public class TankEnemy_2 : MonoBehaviour
{
    [Header("공격 설정")]
    [SerializeField] private float attackCool;  //쿨타임
    [SerializeField] private float attackRange; //공격범위
    [Header("사운드 설정")]
    [SerializeField] private AudioClip attackSound; //공격 사운드
    [Header("이펙트 설정")]
    [SerializeField] private GameObject attackEffect; //공격 이펙트

    private EnemyMove_2 moveSc;

    float delta = 0;    //쿨타임
    bool isAttack = false;  //공격중인지
    GameObject player = null;
    Animator anim;

    void Start()
    {
        if (player == null)
            player = GameObject.FindWithTag("Player");

        moveSc = GetComponent<EnemyMove_2>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;

        Vector3 moveDir = player.transform.position - transform.position;
        float distance = moveDir.magnitude;   

        if(distance <= attackRange && isAttack == false)
        {
            //이동 멈춤
            moveSc.StopMove();
            anim.SetBool("isMove", false);
            //쿨타임
            delta -= Time.deltaTime;
            if (delta <= 0)
            {
                //공격
                anim.SetTrigger("Attack");
                delta = attackCool; //범위 안에서만 쿨타임에 따라 공격
            }
           
        }
        else if(distance > attackRange && isAttack == false)
        {
            //범위 벗어나면 쿨타임 초기화
            delta = 0;

            //이동
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

    //이펙트 생성
    void SpawnEffect()
    {
        //이펙트 생성
        GameObject effect = Instantiate(attackEffect, transform.position + Vector3.up, Quaternion.identity);
    }

    //사운드 재생
    void playSound()
    {
        SoundMgr_2.instance.OneShot(attackSound, 1f);
    }

}
