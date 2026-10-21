using Unity.Cinemachine;
using UnityEngine;

public class PureRagePerk : UpgradePerk
{
    public bool PureRagePerkIsActive = false;
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
        PerksController.Instance.AddPerkToList(this);

        if(PlayersUltController.Instance.HasAUltPerk == false)
            PlayersUltController.Instance.TurnOnUltUI();
    }

    public void ActivatePureRagePerk()
    {
        PureRagePerkIsActive = true;
        Debug.Log("how many times");
        CameraShakeManager.Instance.ShakeCamera(PureRageCinemachine);
        PlayRageFilter();

        //Melee upgrade
        PlayerMeleeAttackRef.PlayerLightAttkDamg = MeleeUpgradeDam;

        //Range upgrade
        PlayerInfoRef.RangeDamg = RangeUpgradeDam;
    }
    public void SetPlayerToNormalStats()
    {
        PureRagePerkIsActive = false;
        PlayTurnOffRageFilter();
        //Melee upgrade
        PlayerMeleeAttackRef.PlayerLightAttkDamg = PlayerMeleeAttackRef.DefaultLightAttkDamg;

        //Range upgrade
        PlayerInfoRef.RangeDamg = 5f;
        Debug.Log("restart pure rage perk");
    }

    public override void DisablePerk()
    {
        PlayersUltController.Instance.IsUsingPureRagePerk = false;
    }

    void PlayRageFilter() => RageAnimator.SetBool("IsRaging", true);
    public void PlayTurnOffRageFilter() => RageAnimator.SetBool("IsRaging", false);
}
