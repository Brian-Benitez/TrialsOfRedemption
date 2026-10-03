using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public Animator Animator;
    public BaseEnemy BaseEnemy;
    public AttackState AttackStateRef;
    [Header("Archers Settings")]
    public GetWithinRangeAttackState GetWithinRangeAttackStateRef;
    public RangeAttackLogicState AttackState;

    private void Update()
    {
        if(BaseEnemy.EnemyType == BaseEnemy.TypeOfEnemy.Archer || BaseEnemy.EnemyType == BaseEnemy.TypeOfEnemy.Wizard)
        {
            if (GetWithinRangeAttackStateRef.IsArcherMoving == true)
                IsWalking();
            else
                IsNotWalking();

            if (AttackState.IsPlayingAnimation == true)
                IsAttacking();
            else
                IsNotAttacking();
        }
        if(BaseEnemy.EnemyType == BaseEnemy.TypeOfEnemy.Swordsman)
        {
            if (AttackStateRef.IsPlayingAttackAni)
                IsAttacking();
            else
                IsNotAttacking();
        }
    }

    void IsAttacking()
    {
        Animator.SetBool("IsAttacking", true);
    }

    void IsNotAttacking()
    {
        Animator.SetBool("IsAttacking", false);
    }

    void IsWalking()
    {
        Animator.SetBool("IsWalking", true);
    }

    void IsNotWalking()
    {
        Animator.SetBool("IsWalking", false);
    }
}
