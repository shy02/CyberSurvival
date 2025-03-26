using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject AttackEffect;
    public void ExecuteAttack(EnemyAI.EnemyType enemyType)
    {
        switch (enemyType)
        {
            case EnemyAI.EnemyType.Melee:
                MeleeAttack();
                break;
            case EnemyAI.EnemyType.Ranged:
                RangedAttack();
                break;
            case EnemyAI.EnemyType.Bike:
                BikeAttack();
                break;
            case EnemyAI.EnemyType.Boss:
                BossAttack();
                break;
        }
    }

    public void MeleeAttack()
    {
        AttackEffect.gameObject.SetActive(true);
    }

    public void RangedAttack()
    {
        Instantiate(AttackEffect, transform.position, Quaternion.identity);
    }

    public void BikeAttack()
    {
        gameObject.GetComponent<ChordMovement>().StartMovement();
    }
    public void BossAttack()
    {
        


    }
}
