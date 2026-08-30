using UnityEngine;

public class UpgradeHealth : LevelUpStat
{
    public int BossSoulCost;
    [Header("Scripts")]
    public PlayerInfo PlayerInfoRef;

    public override void UpgradeStat()
    {
        if(BossSoulCost >= PlayerInfoRef.BossSouls)
        {
            PlayerInfoRef.HealthBarUIRef.SetUIMaxHealth(PlayerInfoRef.CharacterMaxHealth);
            PlayerInfoRef.SetHealth(PlayerInfoRef.CharacterHealthAmount);
            PlayerInfoRef.BossSouls -= BossSoulCost;
            PlayerInfoRef.UpdatePlayersStats();
            UpdateStatsUI();
        }
    }
}
