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
    private EnemyMove_2 moveSc;
    Transform player;
    Vector3 dir = Vector3.zero;
    float angle = 0;
    float delta = 0;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        moveSc = GetComponent<EnemyMove_2>();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(player.position, transform.position);

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

        dir = player.position - transform.position;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        bulletCount = Random.Range(3,6);

        for (int i = 0; i < bulletCount; i++)
        {
            Vector3 bulletDir = CalculateBulletDirection(i, bulletCount, angle);
            GameObject go = Instantiate(bullet, firePos.position, Quaternion.identity);
            go.GetComponent<EnemyBullet_2>().SetDir(bulletDir);
        }

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
