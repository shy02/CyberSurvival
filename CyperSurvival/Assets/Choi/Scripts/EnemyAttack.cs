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
            /*case EnemyAI.EnemyType.Bike:
                BikeAttack();
                break;*/
            /*case EnemyAI.EnemyType.Boss:
                BossAttackPattern();
                break;*/
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

    /*public void BikeAttack()
    {
        enemyAI.ischord = true;
        gameObject.GetComponent<EnemyBike>().StartMovement();
    }*/
    /*public void BossAttackPattern()
    {


    }*/
}
