using UnityEngine;

public class BossApear_3 : MonoBehaviour
{
    //보스가 등장할때 사용할 스크립트
    [SerializeField] GameObject Effect;
    public void ApearBoss()
    {
        Instantiate(Effect, transform.position, Quaternion.identity);
    }
}
