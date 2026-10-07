using UnityEngine;

public class ShotgunPerk : UpgradePerk
{
    public PlayerRangeWeapon PlayerRangeWeaponRef;
    public override void EnablePerk()
    {
        if (!IsPerkActive)
        {
            PlayerRangeWeaponRef.IsUsingShotgunPerk = true;
            PlayerRangeWeaponRef.ChangeArrowsDurations();
            PerksController.Instance.AddPerkToList(this);
        }
    }

    public override void DisablePerk()
    {
        PlayerRangeWeaponRef.NormalArrowDurations();
    }
}
