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
    public float SpeicalUpgradeDam;
    public float RangeUpgradeDam = 2.5f;
    [Header("var holders for maybe upgraded damgs")]
    public float CurrentLightDam;
    public float CurrentSpeicalDam;
    public float CurrentRangeDam;
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
        //checks if player has  dam boost taht after ult is done, they keep that boost
        CurrentLightDam = PlayerMeleeAttackRef.CurrentPlayerLightAttkDamg;
        CurrentSpeicalDam = PlayerMeleeAttackRef.CurrentPlayerSpecialDamg;
        CurrentRangeDam = PlayerInfoRef.RangeDamg;
        //Melee upgrade
        PlayerMeleeAttackRef.CurrentPlayerLightAttkDamg = MeleeUpgradeDam;
        PlayerMeleeAttackRef.CurrentPlayerSpecialDamg = SpeicalUpgradeDam;
        //Range upgrade
        PlayerInfoRef.RangeDamg = RangeUpgradeDam;
    }
    public void SetPlayerToNormalStats()
    {
        PureRagePerkIsActive = false;
        PlayTurnOffRageFilter();
        //Melee upgrade
        PlayerMeleeAttackRef.CurrentPlayerLightAttkDamg = CurrentLightDam;
        PlayerMeleeAttackRef.CurrentPlayerSpecialDamg = CurrentSpeicalDam;
        //Range upgrade
        PlayerInfoRef.RangeDamg = CurrentRangeDam;
        Debug.Log("restart pure rage perk");
    }

    public override void DisablePerk()
    {
        PlayersUltController.Instance.IsUsingPureRagePerk = false;
        PureRagePerkIsActive = false;
    }

    void PlayRageFilter() => RageAnimator.SetBool("IsRaging", true);
    public void PlayTurnOffRageFilter() => RageAnimator.SetBool("IsRaging", false);
}
