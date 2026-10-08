using UnityEngine;

public class MorePerkSlotsAbility : LevelUpStat
{
    public PlayerInfo PlayerInfoRef;
    public override void UpgradeStat()
    {
        if (PlayerInfoRef.BossSouls >= CostAmount)
        {
            PerksController.Instance.MaxAmountOfPerks++;
            PlayerInfoRef.BossSouls -= (int)CostAmount;
            PlayerInfoRef.UpdatePlayersStats();
            UpdateStatsUI();
        }
    }
}
