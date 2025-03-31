using UnityEngine;

public class Spawner_2 : MonoBehaviour
{
    [SerializeField] AudioClip deathSound;
    EnemyDamage_3 enemyDamage;

    private void Start()
    {
        enemyDamage = GetComponent<EnemyDamage_3>();
    }

    private void Update()
    {
        if (enemyDamage.GetHp() <= 0)
            SoundMgr_2.instance.OneShot(deathSound, 1f);
    }

}
