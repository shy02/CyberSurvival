using UnityEngine;

public class EnemyMovement_3 : MonoBehaviour
{
    const float RUSH_EFFECT_POS_X = 0.59f;
    const float RUSH_EFFECT_POS_Y = 0.3f;
    const float DISTANCETOPLAYER = 0.5f;

    const int RUSH_CHILD_NUMBER = 1; // ������ ���° �ڽ��̳�
    


    Transform player; // ���߿� ���� �Ŵ������� �����ϴ� ������� �ٲ� ����

    [Tooltip("���� �̵��ϴ� �ӵ�")]
    [SerializeField] float Speed;

    Animator Bossanime;
    SpriteRenderer spriterenderer;
    SpriteRenderer Rushrenderer;
    GameObject RushEffect;

    private void Start()
    {
        if (gameObject.CompareTag("Boss"))
        {
            RushEffect = transform.GetChild(RUSH_CHILD_NUMBER).gameObject;
            Rushrenderer = RushEffect.GetComponent<SpriteRenderer>();
        }
        player = StageManager_3.instance.player;
        spriterenderer = GetComponent<SpriteRenderer>();
        
        InitializeBossAnimation();
    }

    private void InitializeBossAnimation()
    {
        if (transform.CompareTag("Boss"))
        {
            Bossanime = gameObject.GetComponent<Animator>();
            Bossanime.SetBool("Walk", true);
        }
    }


    void FixedUpdate()
    {
        Vector3 dir = player.transform.position - transform.position;

        if(gameObject.CompareTag("Boss"))
        {
            HandleBossMovement(dir);
        }

        HandleMovement(dir);
        
    }

    #region ��������
    private void HandleBossMovement(Vector3 dir)
    {
        BossDirectionANDRushSet(dir);
        BossUpdateDirect(dir);
    }

    private void BossUpdateDirect(Vector3 dir)
    {
        if (dir.sqrMagnitude < DISTANCETOPLAYER * DISTANCETOPLAYER)
        {
            Bossanime.SetBool("Walk", false);
            Stage3SoundManager.instace.StopWalkSound();
        }
        else
        {
            Bossanime.SetBool("Walk", true);
            Stage3SoundManager.instace.PlayWalkSound();
        }
    }

    private void BossDirectionANDRushSet(Vector3 dir)
    {
        if (dir.x > 0)
        {
            spriterenderer.flipX = true;
            Rushrenderer.flipX = false;
            RushEffect.transform.localPosition = new Vector3(-RUSH_EFFECT_POS_X, RUSH_EFFECT_POS_Y, 0);
        }
        else
        {//��� OK
            spriterenderer.flipX = false;
            Rushrenderer.flipX = true;
            RushEffect.transform.localPosition = new Vector3(RUSH_EFFECT_POS_X, RUSH_EFFECT_POS_Y, 0);
        }
    }
    #endregion
    #region �⺻ �� ����
    private void HandleMovement(Vector3 dir)
    {
        transform.Translate(dir.normalized * Speed * Time.deltaTime);
    }
    #endregion
}
