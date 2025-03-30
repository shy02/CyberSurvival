using UnityEngine;

public class TankEnemy_2 : MonoBehaviour
{
    [Header("���� ����")]
    [SerializeField] private float attackCool;  //��Ÿ��
    [SerializeField] private float attackRange; //���ݹ���
    [Header("���� ����")]
    [SerializeField] private AudioClip attackSound; //���� ����
    [Header("����Ʈ ����")]
    [SerializeField] private GameObject attackEffect; //���� ����Ʈ

    private EnemyMove_2 moveSc;

    float delta = 0;    //��Ÿ��
    bool isAttack = false;  //����������
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
            //�̵� ����
            moveSc.StopMove();
            anim.SetBool("isMove", false);
            //��Ÿ��
            delta -= Time.deltaTime;
            if (delta <= 0)
            {
                //����
                anim.SetTrigger("Attack");
                delta = attackCool; //���� �ȿ����� ��Ÿ�ӿ� ���� ����
            }
           
        }
        else if(distance > attackRange && isAttack == false)
        {
            //���� ����� ��Ÿ�� �ʱ�ȭ
            delta = 0;

            //�̵�
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

    //����Ʈ ����
    void SpawnEffect()
    {
        //����Ʈ ����
        GameObject effect = Instantiate(attackEffect, transform.position + Vector3.up, Quaternion.identity);
    }

    //���� ���
    void playSound()
    {
        SoundMgr_2.instance.OneShot(attackSound, 1f);
    }

}
