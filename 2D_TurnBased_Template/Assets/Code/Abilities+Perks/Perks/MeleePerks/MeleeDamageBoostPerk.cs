using UnityEngine;

public class MeleeDamageBoostPerk : UpgradePerk
{
    public float DamageAdditionToAllMeleeAttks;

    public PlayerMeleeAttack PlayerMeleeAttackRef;

    public override void EnablePerk()
    {
        PlayerMeleeAttackRef.CurrentPlayerLightAttkDamg += DamageAdditionToAllMeleeAttks;
        PlayerMeleeAttackRef.CurrentPlayerSpecialDamg += DamageAdditionToAllMeleeAttks;
        PerksController.Instance.AddPerkToList(this);
    }

    public override void DisablePerk()
    {
        PlayerMeleeAttackRef.CurrentPlayerLightAttkDamg = PlayerMeleeAttackRef.DefaultLightAttkDamg;
        PlayerMeleeAttackRef.CurrentPlayerSpecialDamg = PlayerMeleeAttackRef.DefaultSpeicalDamg;
        Debug.Log("restarted melee damage boos perk");
    }
}
