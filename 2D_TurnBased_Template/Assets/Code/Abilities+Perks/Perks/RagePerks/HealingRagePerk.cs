using UnityEngine;

public class HealingRagePerk : UpgradePerk
{
    public PlayerInfo PlayerInfoRef;

    public void ReadyHealingRagePerk()
    {
        PlayersUltController.Instance.IsUsingHealingRagePerk = true;
    }
    public override void EnablePerk()
    {
        PlayersUltController.Instance.IsUsingHealingRagePerk = true;
        PerksController.Instance.AddPerkToList(this.gameObject);
        //PlayerInfoRef.CharacterHealthAmount += PlayersUltController.Instance.MaxUltPoints;
    }

    public void ActivateHealPerk() => PlayerInfoRef.CharacterHealthAmount += PlayersUltController.Instance.MaxUltPoints;

    public override void DisablePerk()
    {
        PlayersUltController.Instance.IsUsingHealingRagePerk = false;
    }
}
