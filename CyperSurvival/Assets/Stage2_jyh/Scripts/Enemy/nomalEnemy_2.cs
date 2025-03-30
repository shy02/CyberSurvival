using UnityEngine;
using UnityEngine.U2D;

public class nomalEnemy_2 : MonoBehaviour
{
    [Header("���� ����")]
    [SerializeField] private float attackCool;  //��Ÿ��
    [SerializeField] private float attackRange; //���ݹ���
    [SerializeField] private int bulletCount;   //�Ѿ˰���
    [Header("���� ����")]
    public GameObject bullet;
    public Transform firePos;
    [Header("����")]
    [SerializeField]private AudioClip attackSound;
    //����
    private EnemyMove_2 moveSc;
    GameObject player;
    Animator anim;
    //���� ����
    Vector3 dir = Vector3.zero; //�Ѿ� ����
    float angle = 0;    //�Ѿ� ���� ����
    float delta = 0;    //��Ÿ�� ����
    bool isAttack = false;  //����������

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
        moveSc = GetComponent<EnemyMove_2>();
    }

    void Update()
    {
        if (player == null) return;

        Debug.Log("����������: " + isAttack);

        float distance = Vector2.Distance(player.transform.position, transform.position);

        if (distance <= attackRange && isAttack == false)
        {
            //�̵� ����
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

    //�ִϸ��̼ǿ��� ȣ��
    void Attack1()
    {
        //�����϶��� �������� ����
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
        //�����϶��� �������� ����
        if (moveSc.canMove == true)
            return;

        //�¿����
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
        //�����϶��� �������� ����
        if (moveSc.canMove == true)
            return;

        //�¿����
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
        float angleOffset = (totalBullets % 2 == 0) ? 2f : 0f;   //������ ¦���� 2.5���� ������
        float currentAngle = baseAngle + angleOffset + angleStep * (index - totalBullets / 2);

        float dirX = Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float dirY = Mathf.Sin(currentAngle * Mathf.Deg2Rad);

        return new Vector3(dirX, dirY, 0);
    }


}
