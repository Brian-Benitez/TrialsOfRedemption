using Unity.Cinemachine;
using UnityEngine;

public class PureRagePerk : UpgradePerk
{
    [Header("Stats Info")]
    [Header("Movement")]
    public float BoostedMovementSpeed = 11f;
    [Header("Damages")]
    public float MeleeUpgradeDam = 4f;
    public float RangeUpgradeDam = 2.5f;
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
        Debug.Log("how many times");
        CameraShakeManager.Instance.ShakeCamera(PureRageCinemachine);
        PlayRageFilter();
        PlayerMovementRef.FullSpeed = BoostedMovementSpeed;

        //Melee upgrade
        PlayerMeleeAttackRef.PlayerLightAttkDamg = MeleeUpgradeDam;

        //Range upgrade
        PlayerInfoRef.RangeDamg = RangeUpgradeDam;
    }
    public void SetPlayerToNormalStats()
    {
        PlayTurnOffRageFilter();
        //Movement upgrade
        PlayerMovementRef.FullSpeed = 8f;

        //Melee upgrade
        PlayerMeleeAttackRef.PlayerLightAttkDamg = PlayerMeleeAttackRef.DefaultLightAttkDamg;

        //Range upgrade
        PlayerInfoRef.RangeDamg = 1f;
        Debug.Log("restart pure rage perk");
    }

    public override void DisablePerk()
    {
        PlayersUltController.Instance.IsUsingPureRagePerk = false;
    }

    void PlayRageFilter() => RageAnimator.SetBool("IsRaging", true);
    public void PlayTurnOffRageFilter() => RageAnimator.SetBool("IsRaging", false);
}
