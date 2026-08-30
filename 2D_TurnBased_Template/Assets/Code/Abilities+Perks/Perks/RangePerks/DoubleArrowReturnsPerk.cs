using UnityEngine;

public class DoubleArrowReturnsPerk : UpgradePerk
{
    public int ArrowsMultipler = 2;
    public override void EnablePerk()
    {
        if (!IsPerkActive && IsPerkPicked)
        {
            PlayerAmmoController.Instance.AmountOfArrowsReturned = ArrowsMultipler;
            PlayerAmmoController.Instance.PlayerInfoRef.UpdatePlayersStats();
            PerksController.Instance.AddPerkToList(this.gameObject);
        }
    }


    public override void DisablePerk()
    {
        if(IsPerkActive)
        {
            PlayerAmmoController.Instance.AmountOfArrowsReturned = 1;
            Debug.Log("remove max ammo perk");
        }
    }
}
