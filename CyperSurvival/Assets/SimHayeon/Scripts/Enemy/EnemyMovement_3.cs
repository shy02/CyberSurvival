using UnityEngine;

public class EnemyMovement_3 : MonoBehaviour
{
    Transform player; // 나중에 게임 매니저에서 참조하는 방식으로 바꿀 예정

    [Tooltip("적이 이동하는 속도")]
    [SerializeField] float Speed;

    Animator Bossanime;
    GameObject RushEffect;

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
            RushEffect = transform.GetChild(1).gameObject;
            if(dir.x > 0)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
                RushEffect.GetComponent<SpriteRenderer>().flipX = false;
                RushEffect.transform.localPosition = new Vector3(-0.59f, 0.3f, 0);
            }
            else
            {//얘는 OK
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
                RushEffect.GetComponent<SpriteRenderer>().flipX = true;
                RushEffect.transform.localPosition = new Vector3(0.58f, 0.3f, 0);
            }

            if (dir.sqrMagnitude < 0.5f * 0.5f)
            {
                Bossanime.SetBool("Walk", false);
                Stage3SoundManager.instace.StopWalkSound();
            }
            else
            {
                Bossanime.SetBool("Walk", true);
                Stage3SoundManager.instace.PlayWalkSound();
                transform.Translate(dir.normalized * Speed * Time.deltaTime);
            }
        }
        else
        {
            transform.Translate(dir.normalized * Speed * Time.deltaTime);
        }
    }
}
