using UnityEngine;

public class EnemyMove_2 : MonoBehaviour
{
    GameObject player = null;

    //�̵�
    [SerializeField] private float moveSpeed = 1;
    Vector3 moveDir = Vector3.zero;
    private SpriteRenderer spriteRenderer;

    //������
    [SerializeField] private int damage;

    //isMove
    [HideInInspector] public bool canMove = true;

    //������ ���� ���
    [SerializeField] private AudioClip deathSound;
    EnemyDamage_3 enemyDamage_3;

    void Start()
    {
        if (player == null)
            player = GameObject.FindWithTag("Player");

        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyDamage_3 = GetComponent<EnemyDamage_3>();

    }

    void Update()
    {
        if ((deathSound != null))
        {
            if (enemyDamage_3.GetHp() <= 0)
            {
                SoundMgr_2.instance.OneShot(deathSound, 0.5f);
                return;
            }
        }
       
        Vector3 vec = player.transform.position - transform.position;
        moveDir = vec.normalized;

        //�¿����
        if (vec.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        if (canMove == true)
            transform.Translate(moveDir * moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(damage);
        }
    }

    public void StopMove()
    {
        canMove = false;
    }

    public void StartMove()
    {
        canMove = true;
    }

}
