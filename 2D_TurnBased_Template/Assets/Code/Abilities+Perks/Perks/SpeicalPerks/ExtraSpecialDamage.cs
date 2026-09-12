using UnityEngine;

public class ExtraSpecialDamage : UpgradePerk
{
    [Header("Stats Info")]
    public int AddedDamage;
    public float DecreaseRangeAmount;
    public PlayerMeleeAttack PlayerMeleeAttackRef;
    public override void EnablePerk()
    {
        if (!IsPerkActive)
        {
            PlayerMeleeAttackRef.PlayerSpecialDamg += AddedDamage;//1;
            PlayerMeleeAttackRef.SpeicalRange -= DecreaseRangeAmount;//0.6f;
            PerksController.Instance.AddPerkToList(this.gameObject);
        }
    }

    public override void DisablePerk()
    {
        PlayerMeleeAttackRef.PlayerSpecialDamg = 2f;
        PlayerMeleeAttackRef.SpeicalRange = 3f;//0.6f;
        Debug.Log("removed rage quake perk");
    }
}
