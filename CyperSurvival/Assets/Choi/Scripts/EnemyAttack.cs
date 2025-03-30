using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject AttackEffect;
    private Animator animator;
    [SerializeField] private Transform firePosition;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

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
        animator.SetTrigger("isAttack");
        AttackEffect.gameObject.SetActive(true);
    }

    public void RangedAttack()
    {
        animator.SetTrigger("isAttack");
        Instantiate(AttackEffect, firePosition.position, Quaternion.identity);
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
