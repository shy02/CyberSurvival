using UnityEngine;

public class BossApear_3 : MonoBehaviour
{
    //������ �����Ҷ� ����� ��ũ��Ʈ
    [SerializeField] GameObject Effect;
    public void ApearBoss()
    {
        Instantiate(Effect, transform.position, Quaternion.identity);
    }
}
