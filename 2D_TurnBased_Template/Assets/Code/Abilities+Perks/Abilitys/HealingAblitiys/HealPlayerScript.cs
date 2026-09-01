using UnityEngine;

public class HealPlayerScript : LevelUpStat
{
    private float upgradeHealthAmount;
    private const float firstLevelHealthUpgrade = 5f;
    private const float secondLevelHealthUpgrade = 10f;
    private const float lastLevelHealthUpgrade = 15f;
    public enum UpgradeTiers
    {
        None,
        FirstUpgrade,
        SecondUpgrade,
        LastUpgrade
    }
    public UpgradeTiers CurrentTier = UpgradeTiers.None;
    [Header("Scripts")]
    public PlayerInfo PlayerInfoRef;

    public override void UpgradeStat()
    {
        if(PlayerInfoRef.Souls >= CostAmount)
        {
            StatsLvl++;
            DetermineCurrentTier();
            PlayerInfoRef.CharacterMaxHealth += upgradeHealthAmount;//THIS IS FOR UPGRADING HEALTH KEEPING IT HERE FOR LATER USAGE DO NOT DELETE
            PlayerInfoRef.HealthBarUIRef.SetUIMaxHealth(PlayerInfoRef.CharacterMaxHealth);
            PlayerInfoRef.SetHealth(PlayerInfoRef.CharacterHealthAmount);
            PlayerInfoRef.Souls -= (int)CostAmount;// if theres issues with souls being subtracted by cost amount its here.
            PlayerInfoRef.UpdatePlayersStats();
            UpdateStatsUI();
        }
        else
        {
            Debug.Log("player does not have enough souls.");
        }
    }

    void DetermineCurrentTier()
    {
        if(StatsLvl == 1)
            CurrentTier = UpgradeTiers.FirstUpgrade;
        if (StatsLvl == 2)
            CurrentTier = UpgradeTiers.SecondUpgrade;
        if(StatsLvl == 3)
            CurrentTier = UpgradeTiers.LastUpgrade;

        DetermineHealthAmount();
    }
    void DetermineHealthAmount()
    {
        if(CurrentTier == UpgradeTiers.FirstUpgrade)
            upgradeHealthAmount = firstLevelHealthUpgrade;
        if (CurrentTier == UpgradeTiers.SecondUpgrade)
            upgradeHealthAmount = secondLevelHealthUpgrade;
        if(CurrentTier == UpgradeTiers.LastUpgrade)
            upgradeHealthAmount = lastLevelHealthUpgrade;
    }

}
