using UnityEngine;

public class EnemyMovement_3 : MonoBehaviour
{
    public Transform player; // 나중에 게임 매니저에서 참조하는 방식으로 바꿀 예정

    [Tooltip("적이 이동하는 속도")]
    [SerializeField] float Speed;

    private void Start()
    {
        player = StageManager_3.instance.player;
    }
    void FixedUpdate()
    {
        Vector3 dir = player.transform.position - transform.position;
        transform.Translate(dir.normalized * Speed * Time.deltaTime);
    }
}
