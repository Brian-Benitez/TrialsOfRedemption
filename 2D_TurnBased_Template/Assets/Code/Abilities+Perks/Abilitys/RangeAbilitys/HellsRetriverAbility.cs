using UnityEngine;

public class HellsRetriverAbility : LevelUpStat
{
    public enum UpgradeTiers
    {
        None,
        FirstUpgrade,
        SecondUpgrade,
        LastUpgrade
    }

    public UpgradeTiers CurrentTier = UpgradeTiers.None;
    public bool IsUsingHellsRetriver = false;
    private const int FirstUpgradeAmount = 10;
    private const int SecondUpgradeAmount = 50;
    private const int LastUpgradeAmount = 9999;
    public PlayerInfo PlayerInfoRef;
    public override void UpgradeStat()
    {
        if (PlayerInfoRef.Souls >= CostAmount)
        {
            IsUsingHellsRetriver = true;
            StatsLvl++;
            PlayerInfoRef.Souls -= (int)CostAmount;// if theres issues with souls being subtracted by cost amount its here.
            PlayerInfoRef.UpdatePlayersStats();
            UpdateStatsUI();
            SetCurrentUpgradeTier();
        }
        else
        {
            Debug.Log("player does not have enough souls.");
        }
    }

    void SetCurrentUpgradeTier()
    {
        if (StatsLvl == 1)
            CurrentTier = UpgradeTiers.FirstUpgrade;
        if (StatsLvl == 2)
            CurrentTier = UpgradeTiers.SecondUpgrade;
        if(StatsLvl == 3)
            CurrentTier= UpgradeTiers.LastUpgrade;
    }
    public void AddXAmountOfArrowsToPlayer()
    {
        if(IsUsingHellsRetriver)
        {
            if (CurrentTier == UpgradeTiers.FirstUpgrade)
                PlayerAmmoController.Instance.AmmoAmount += FirstUpgradeAmount;
            if (CurrentTier == UpgradeTiers.SecondUpgrade)
                PlayerAmmoController.Instance.AmmoAmount += SecondUpgradeAmount;
            if (CurrentTier == UpgradeTiers.LastUpgrade)
                PlayerAmmoController.Instance.AmmoAmount += LastUpgradeAmount;
        }
    }
}
