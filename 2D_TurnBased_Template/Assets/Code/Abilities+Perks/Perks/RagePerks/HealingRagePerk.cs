using UnityEngine;

public class HealingRagePerk : UpgradePerk
{
    public PlayerInfo PlayerInfoRef;
    public HealthBarUI HealthBarUIRef;

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

    public void ActivateHealPerk()
    {
        HealthBarUIRef.SetUIHealth(PlayersUltController.Instance.MaxUltPoints);
        PlayerInfoRef.CharacterHealthAmount += PlayersUltController.Instance.MaxUltPoints;
    }

    public override void DisablePerk()
    {
        PlayersUltController.Instance.IsUsingHealingRagePerk = false;
    }
}
