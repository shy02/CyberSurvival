using UnityEngine;

public class DeathEffectAutoDestroy_1 : MonoBehaviour
{
    [SerializeField] private float destroyTime = 2f;  // ������ �ð� (��)

    // Start is called before the first frame update
    void Start()
    {
        // destroyTime��ŭ ��ٸ� �� ���� ������Ʈ ����
        Destroy(gameObject, destroyTime);
    }
}