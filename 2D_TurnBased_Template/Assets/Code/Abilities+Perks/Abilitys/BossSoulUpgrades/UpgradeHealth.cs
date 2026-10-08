using UnityEngine;

public class UpgradeHealth : LevelUpStat
{
    [Header("Scripts")]
    public PlayerInfo PlayerInfoRef;

    public override void UpgradeStat()
    {
        if(PlayerInfoRef.BossSouls >= CostAmount)
        {
            PlayerInfoRef.HealthBarUIRef.SetUIMaxHealth(PlayerInfoRef.CharacterMaxHealth);
            PlayerInfoRef.SetHealth(PlayerInfoRef.CharacterHealthAmount);
            PlayerInfoRef.BossSouls -= (int)CostAmount;
            PlayerInfoRef.UpdatePlayersStats();
            UpdateStatsUI();
        }
    }
}
