using UnityEngine;

public class Bomb_3 : MonoBehaviour
{
    //설치된 순간 카운트 다운 시작
    //몇초후 펑!!!!
    //하고 사라짐

    [SerializeField] float CountDown = 1f;//터지기 까지의 시간
    [SerializeField] float DestroyTime = 2f;//오브젝트 사라지는 시간
    [SerializeField] int Damage = 15;
    [SerializeField] GameObject Bomb_effect;//터지는 효과 오브젝트

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

        //여기서 플레이어 데미지
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
