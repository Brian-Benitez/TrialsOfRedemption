using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator PlayerAnimator;

    public PlayerMeleeAttack PlayerMeleeAttackRef;
    public PlayerMovement PlayerMovementRef;


    private void Update()
    {
        if (PlayerMovementRef.IsDashing)
            IsDashing();
        else
            IsNotDashing();
    }

    //Walking animations bools
    public void IsMoving() => PlayerAnimator.SetBool("IsMoving", true);
    public void IsNotMoving() => PlayerAnimator.SetBool("IsMoving", false);

    //Attacking animations bools
    public void IsAttacking()
    {
        if(PlayerMeleeAttackRef.AmountOfAttacks == 0)
        {
            PlayerAnimator.SetBool("IsAttacking", true);
        }
        else if(PlayerMeleeAttackRef.AmountOfAttacks == 1)
        {
            //PlayerAnimator.SetBool("IsAttacking", false);
            PlayerAnimator.SetBool("IsAttackingTwo", true);
        }
        else if(PlayerMeleeAttackRef.AmountOfAttacks == 2 || PlayerMeleeAttackRef.AmountOfAttacks == 3)
        {
            //PlayerAnimator.SetBool("IsAttackingTwo", false);
            PlayerAnimator.SetBool("IsAttackingThree", true);
            Debug.Log("play me");
        }
       
    }
    public void IsNotAttacking()
    {
        PlayerAnimator.SetBool("IsAttacking", false);
        PlayerAnimator.SetBool("IsAttackingTwo", false);
        PlayerAnimator.SetBool("IsAttackingThree", false);
    }

    public void IsDashing() => PlayerAnimator.SetBool("IsDashing", true);
    public void IsNotDashing() => PlayerAnimator.SetBool("IsDashing", false);

    public void IsParrying() => PlayerAnimator.SetBool("IsParrying", true);

    public void IsNotParrying() => PlayerAnimator.SetBool("IsParrying", false);
}
