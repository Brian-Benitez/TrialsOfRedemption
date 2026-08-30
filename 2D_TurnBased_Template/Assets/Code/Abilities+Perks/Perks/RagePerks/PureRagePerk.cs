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
        PlayerMovementRef.FullSpeed += BoostedMovementSpeed;
        PlayerMovementRef.DashCoolDown -= LoweredDashCoolDown;

        //Melee upgrade
        PlayerMeleeAttackRef.PlayerLightAttkDamg += MeleeUpgradeDam;
        PlayerMeleeAttackRef.PlayerHeavyAttkDamg += MeleeUpgradeDam;

        //Range upgrade
        PlayerInfoRef.RangeDamg += RangeUpgradeDam;
    }

    public override void DisablePerk()
    {
        PlayersUltController.Instance.IsUsingPureRagePerk = false;
    }
}
