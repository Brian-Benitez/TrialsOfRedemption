using UnityEngine;

public class HealPlayerScript : LevelUpStat
{
    [Header("Scripts")]
    public PlayerInfo PlayerInfoRef;

    public override void UpgradeStat()
    {
        if(PlayerInfoRef.Souls >= CostAmount)
        {
            PlayerInfoRef.CharacterHealthAmount = PlayerInfoRef.CharacterMaxHealth;
            //PlayerInfoRef.HealthBarUIRef.SetUIMaxHealth(PlayerInfoRef.CharacterMaxHealth);
            PlayerInfoRef.SetHealth(PlayerInfoRef.CharacterMaxHealth);
            PlayerInfoRef.Souls -= (int)CostAmount;// if theres issues with souls being subtracted by cost amount its here.
            PlayerInfoRef.UpdatePlayersStats();
            UpdateStatsUI();
        }
        else
        {
            Debug.Log("player does not have enough souls.");
        }
        /*
        PlayerInfoRef.CharacterMaxHealth += IncrementingStatsAmount;//THIS IS FOR UPGRADING HEALTH KEEPING IT HERE FOR LATER USAGE DO NOT DELETE
        PlayerInfoRef.HealthBarUIRef.SetUIMaxHealth(PlayerInfoRef.CharacterMaxHealth);
        PlayerInfoRef.SetHealth(PlayerInfoRef.CharacterHealthAmount);
        PlayerInfoRef.Souls -= (int)CostAmount;// if theres issues with souls being subtracted by cost amount its here.
        PlayerInfoRef.UpdatePlayersStats();
        UpdateStatsUI();
        */
    }
}
