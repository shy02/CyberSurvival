using UnityEngine;

public class ObstacleMoving2_1 : MonoBehaviour
{
    public float moveSpeed = 2f;      // 이동 속도
    private Vector3 targetPosition;   // 목표 위치
    private Vector3 startPosition;    // 시작 위치
    private int currentPhase = 0;     // 현재 단계 (Y 이동, X 이동 등)

    void Start()
    {
        // 시작 위치 기록
        startPosition = transform.position;
        targetPosition = startPosition;  // 최초 목표는 시작 위치
    }

    void Update()
    {
        // 목표 위치로 이동
        MoveObject();
    }

    void MoveObject()
    {
        // 현재 단계에 따라 이동
        float step = moveSpeed * Time.deltaTime; // 한 프레임 이동 거리

        // 목표 위치에 도달했을 때 단계 전환
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // 현재 단계에 맞는 이동 방향 설정
            switch (currentPhase)
            {
                case 0:
                    targetPosition = new Vector3(transform.position.x, startPosition.y + 6.9f, transform.position.z);
                    break;
                case 1:
                    targetPosition = new Vector3(transform.position.x + 3.8f, transform.position.y, transform.position.z);
                    break;
                case 2:
                    targetPosition = new Vector3(transform.position.x, transform.position.y - 6.9f, transform.position.z);
                    break;
                case 3:
                    targetPosition = new Vector3(transform.position.x - 3.8f, transform.position.y, transform.position.z);
                    break;
            }

            // 이동 단계 업데이트
            currentPhase = (currentPhase + 1) % 4;
        }

        // 목표 위치로 이동
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    }
}