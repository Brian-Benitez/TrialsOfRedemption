using UnityEngine;

public class ShieldUpgrade : LevelUpStat
{
    public PlayerInfo PlayerInfoRef;
    public override void UpgradeStat()
    {
        if (PlayerInfoRef.Souls >= CostAmount)
        {
            PlayerInfoRef.Souls -= (int)CostAmount;
            StatsLvl++;
            CostAmount *= 2;
            PlayerInfoRef.UpdatePlayersStats();
            UpdateStatsUI();
        }
    }
}
