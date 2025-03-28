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
    private EnemyMove_2 moveSc;
    GameObject player;
    Vector3 dir = Vector3.zero; //�Ѿ� ����
    float angle = 0;    //�Ѿ� ���� ����
    float delta = 0;    //��Ÿ�� ����

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        moveSc = GetComponent<EnemyMove_2>();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(player.transform.position, transform.position);

        if (distance <= attackRange)
        {
            moveSc.StopMove();

            delta -= Time.deltaTime;
            if(delta <= 0)
            {
                delta = attackCool;
                Attack();
            }
        }
        else
        {
            moveSc.StartMove();
        }

    }

    void Attack()
    {
        //�����϶��� �������� ����
        if (moveSc.canMove == true)
            return;

        dir = player.transform.position - transform.position;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        bulletCount = Random.Range(bulletCount-1, bulletCount + 2);

        for (int i = 0; i < bulletCount; i++)
        {
            Vector3 bulletDir = CalculateBulletDirection(i, bulletCount, angle);
            GameObject go = Instantiate(bullet, firePos.position, Quaternion.identity);
            go.GetComponent<EnemyBullet_2>().SetDir(bulletDir);
        }
        SoundMgr_2.instance.OneShot(attackSound, 0.5f);
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
