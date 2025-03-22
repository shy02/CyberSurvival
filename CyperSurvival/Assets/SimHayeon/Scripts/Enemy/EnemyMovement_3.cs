using UnityEngine;

public class EnemyMovement_3 : MonoBehaviour
{
    public Transform player; // 나중에 게임 매니저에서 참조하는 방식으로 바꿀 예정

    [Tooltip("적이 이동하는 속도")]
    [SerializeField] float Speed;

    Animator Bossanime;

    private void Start()
    {
        player = StageManager_3.instance.player;
        if (gameObject.name == "Boss")
        {
            Bossanime = gameObject.GetComponent<Animator>();
            Bossanime.SetBool("Walk", true);
        }
    }
    void FixedUpdate()
    {
        Vector3 dir = player.transform.position - transform.position;
        if(gameObject.name == "Boss")
        {
            if(dir.x > 0)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }

            if (dir.sqrMagnitude < 0.5f * 0.5f)
            {
                Bossanime.SetBool("Walk", false);
            }
            else
            {
                Bossanime.SetBool("Walk", true);
                transform.Translate(dir.normalized * Speed * Time.deltaTime);
            }
        }
        else
        {
            transform.Translate(dir.normalized * Speed * Time.deltaTime);
        }
    }
}
