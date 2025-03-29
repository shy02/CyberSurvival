using UnityEngine;

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

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
        moveSc = GetComponent<EnemyMove_2>();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(player.transform.position, transform.position);

        if (distance <= attackRange)
        {
            //�̵� ����
            moveSc.StopMove();
            anim.SetBool("isMove", false);

            delta -= Time.deltaTime;
            if(delta <= 0)
            {
                delta = attackCool;
                //isattack = true;
                anim.SetTrigger("Attack");
            }
        }
        else if(distance > attackRange)
        {
            moveSc.StartMove();
            anim.SetBool("isMove", true);
        }

    }

    //�ִϸ��̿��� ȣ��
    void Attack()
    {
        //�����϶��� �������� ����
        if (moveSc.canMove == true)
            return;

        dir = player.transform.position - transform.position;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        int bullets = Random.Range(bulletCount-1, bulletCount + 2); //�Ѿ� ��������

        for (int i = 0; i < bullets; i++)
        {
            Vector3 bulletDir = CalculateBulletDirection(i, bullets, angle);
            GameObject go = Instantiate(bullet, firePos.position, Quaternion.identity);
            go.GetComponent<EnemyBullet_2>().SetDir(bulletDir);
        }
        SoundMgr_2.instance.OneShot(attackSound, 1f);
    }

    Vector3 CalculateBulletDirection(int index, int totalBullets, float baseAngle)
    {
        float angleStep = 5f;
        float angleOffset = (totalBullets % 2 == 0) ? 2.5f : 0f;   //������ ¦���� 2.5���� ������
        float currentAngle = baseAngle + angleOffset + angleStep * (index - totalBullets / 2);

        float dirX = Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float dirY = Mathf.Sin(currentAngle * Mathf.Deg2Rad);

        return new Vector3(dirX, dirY, 0);
    }


}
