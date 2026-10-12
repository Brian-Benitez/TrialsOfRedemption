using UnityEngine;

public class GulitEffectsAnimatorController : MonoBehaviour
{
    public Animator BossAnimator;
    public Animator ShockwaveAnimator;
    public Animator RangeAttackAnimator;

    public AttackState AttackStateRef;
    public DKRangeAttack DKRangeAttackRef;
    public DKChaseState DKChaseStateRef;


    private void Update()
    {
        if (DKChaseStateRef.IsMoving)//moving animtions
            PlayMovingAnimation();
        else
            StopPlayingMovingAnimation();   

        if (AttackStateRef.IsPlayingAttackAni)//attacking animations
            PlayAttackingAnimations();
        else
            StopPlayingAttackingAnimations();

        if (DKRangeAttackRef.IsAttackingNow)//range attack animations
            PlayRangeAttackAnimation();
        else
            StopPlayingRangeAttackAnimation();
    }

    public void PlayMovingAnimation() => BossAnimator.SetBool("IsMoving", true);
    public void StopPlayingMovingAnimation() => BossAnimator.SetBool("IsMoving", false);

    ///Shockwave animations here.
    public void PlayAttackingAnimations()
    {
        BossAnimator.SetBool("IsAttacking", true);
        ShockwaveAnimator.SetBool("IsAttacking", true);
    }
    public void StopPlayingAttackingAnimations()
    {
        BossAnimator.SetBool("IsAttacking", false);
        ShockwaveAnimator.SetBool("IsAttacking", false);
    }

    //Range Attacks animations here.
    public void PlayRangeAttackAnimation() => RangeAttackAnimator.SetBool("IsRangeAttacking", true);
    public void StopPlayingRangeAttackAnimation() => RangeAttackAnimator.SetBool("IsRangeAttacking", false);
}
