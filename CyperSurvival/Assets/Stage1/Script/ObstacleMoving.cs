using UnityEngine;

public class ObstacleMoving : MonoBehaviour
{
    public float moveDistance = 9f;  // 왕복할 거리 (9 단위)
    public float speed = 2f;         // 이동 속도
    public bool moveRight = true;    // true면 우측, false면 좌측으로 시작

    private Vector3 startPosition;   // 오브젝트의 시작 위치

    void Start()
    {
        // 오브젝트의 현재 위치를 시작 위치로 설정
        startPosition = transform.position;
    }

    void Update()
    {
        // Mathf.PingPong을 사용하여 왕복 움직임 (0부터 moveDistance까지 왕복)
        float xPosition = Mathf.PingPong(Time.time * speed, moveDistance);

        // moveRight가 true일 경우 오른쪽으로 이동, false일 경우 왼쪽으로 이동
        if (!moveRight)
        {
            xPosition = moveDistance - xPosition;  // 좌측으로 이동하는 방식으로 변경
        }

        // 오브젝트의 위치를 시작 위치에서 왕복하도록 설정
        transform.position = new Vector3(startPosition.x + xPosition, transform.position.y, transform.position.z);
    }
}