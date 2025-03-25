using UnityEngine;

public class DeathEffectAutoDestroy_1 : MonoBehaviour
{
    [SerializeField] private float destroyTime = 2f;  // 삭제될 시간 (초)

    // Start is called before the first frame update
    void Start()
    {
        // destroyTime만큼 기다린 후 현재 오브젝트 삭제
        Destroy(gameObject, destroyTime);
    }
}