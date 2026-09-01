using UnityEngine;

public class SecondChanceAbility : LevelUpStat
{
    public float ReviveAmountForPlayer;
    private const float firstReviveHealthAmount = 5f;
    private const float secondReviveHealthAmount = 10f;
    private const float lastReviveHealthAmount = 15f;
    public bool IsSecondChanceEnabled = false;
    public bool IsSecondChanceUsed = false;
    public enum UpgradeTiers
    {
        None,
        FirstUpgrade,
        SecondUpgrade,
        LastUpgrade
    }
    public UpgradeTiers CurrentTier = UpgradeTiers.None;
    public BaseCharacter PlayerBaseStats;
    public PlayerInfo PlayerInfoRef;
    public override void UpgradeStat()
    {
        if (PlayerInfoRef.Souls >= CostAmount)
        {
            StatsLvl++;
            PlayerInfoRef.Souls -= (int)CostAmount;
            PlayerInfoRef.UpdatePlayersStats();
            UpdateStatsUI();
            DetermineCurrentTier();
        }
        else
        {
            Debug.Log("player does not have enough souls.");
        }
    }

    void DetermineCurrentTier()
    {
        if (StatsLvl == 1)
        {
            CurrentTier = UpgradeTiers.FirstUpgrade;
            ReviveAmountForPlayer = firstReviveHealthAmount;
        }
        if (StatsLvl == 2)
        {
            CurrentTier = UpgradeTiers.SecondUpgrade;
            ReviveAmountForPlayer = secondReviveHealthAmount;
        }
        if(StatsLvl == 3)
        {
            CurrentTier = UpgradeTiers.LastUpgrade;
            ReviveAmountForPlayer = lastReviveHealthAmount;
        }
            
    }
    public void ActivateSecondChanceAbility()
    {
        if(IsSecondChanceEnabled && IsSecondChanceUsed == false)
        {
            IsSecondChanceUsed = true;
        }
    }
}
