using UnityEngine;

public class BossApear : MonoBehaviour
{
    //������ �����Ҷ� ����� ��ũ��Ʈ
    [SerializeField] GameObject Effect;
    public void ApearBoss()
    {
        Instantiate(Effect, transform.position, Quaternion.identity);
    }
}
