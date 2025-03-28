using UnityEngine;

public class Bomb_3 : MonoBehaviour
{
    //��ġ�� ���� ī��Ʈ �ٿ� ����
    //������ ��!!!!
    //�ϰ� �����

    [SerializeField] float CountDown = 1f;//������ ������ �ð�
    [SerializeField] float DestroyTime = 2f;//������Ʈ ������� �ð�
    [SerializeField] int Damage = 15;
    [SerializeField] GameObject Bomb_effect;//������ ȿ�� ������Ʈ

    bool CangiveDamage = false;

    Transform Target;
    private void Start()
    {
        Target = StageManager_3.instance.player;
        Invoke(nameof(StartBomb),CountDown);
        Stage3SoundManager.instace.FallingBomb();
    }

    private void StartBomb()
    {
        Stage3SoundManager.instace.Bomb_Boom();
        Instantiate(Bomb_effect, transform.position, Quaternion.identity);

        //���⼭ �÷��̾� ������
        if (CangiveDamage) DamageToPlayer();

        Destroy(gameObject, DestroyTime);
    }

    private void DamageToPlayer()
    {
        if (Target != null)
        {
            Target.GetComponent<Player>().TakeDamage(Damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CangiveDamage = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            CangiveDamage = false;
        }
    }

}
