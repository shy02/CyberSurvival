using UnityEngine;
using UnityEngine.UI;

public class Spawner_2 : MonoBehaviour
{
    EnemyDamage_3 enemyDamage_3;
    public Image hpBar;
    int MaxHp = 100;
    int CurrentHp = 100;

    void Start()
    {
        enemyDamage_3 = GetComponent<EnemyDamage_3>();
    }

    void Update()
    {
        
    }

}
