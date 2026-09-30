using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class PureRagePerk : UpgradePerk
{
    [Header("Stats Info")]
    [Header("Movement")]
    public float BoostedMovementSpeed;
    public float LoweredDashCoolDown;
    [Header("Damages")]
    public float MeleeUpgradeDam;
    public float RangeUpgradeDam;
    [Header("Rage Filter")]
    public Animator RageAnimator;
    [Header("Shaking camera")]
    public CinemachineImpulseSource PureRageCinemachine;
    public PlayerMovement PlayerMovementRef;
    public PlayerMeleeAttack PlayerMeleeAttackRef;
    public PlayerInfo PlayerInfoRef;

    public void ReadyPureRagePerk()
    {
        PlayersUltController.Instance.IsUsingPureRagePerk = true;
    }
    public override void EnablePerk()
    {
        PlayersUltController.Instance.IsUsingPureRagePerk = true;
        PerksController.Instance.AddPerkToList(this.gameObject);
    }

    public void ActivatePureRagePerk()
    {
        CameraShakeManager.Instance.ShakeCamera(PureRageCinemachine);
        PlayRageFilter();
        PlayerMovementRef.FullSpeed += BoostedMovementSpeed;
        PlayerMovementRef.DashCoolDown -= LoweredDashCoolDown;

        //Melee upgrade
        PlayerMeleeAttackRef.PlayerLightAttkDamg += MeleeUpgradeDam;

        //Range upgrade
        PlayerInfoRef.RangeDamg += RangeUpgradeDam;
    }

    public override void DisablePerk()
    {
        PlayersUltController.Instance.IsUsingPureRagePerk = false;
    }

    void PlayRageFilter() => RageAnimator.SetBool("IsRaging", true);
    public void PlayTurnOffRageFilter() => RageAnimator.SetBool("IsRaging", false);
}
