using UnityEngine;

public class LingeringHealthAbility : LevelUpStat
{
    [Header("Info")]
    public bool IsLingerHealthEnabled = false;
    private float healthUpgradeAmount;
    private const float firstUpgradeAmount = 3f;
    private const float secondUpgradeAmount = 5f;
    private const float finalUpgradeAmount = 7f;
    public enum AbilityTiers
    {
        None,
        FirstUpgrade,
        SecondUpgrade,
        FinalUpgrade
    }
    public AbilityTiers Tier = AbilityTiers.None;
    [Header("Scripts")]
    public PlayerInfo PlayerInfoRef;

    public override void UpgradeStat()
    {
        if (PlayerInfoRef.Souls >= CostAmount)
        {
            IsLingerHealthEnabled = true;
            PlayerInfoRef.Souls -= (int)CostAmount;// if theres issues with souls being subtracted by cost amount its here.
            PlayerInfoRef.UpdatePlayersStats();
            UpdateStatsUI();
            SetHealthIncrementAmount();
        }
        else
        {
            Debug.Log("player does not have enough souls.");
        }
    }
    private void SetHealthIncrementAmount()
    {
        if (StatsLvl == 1)
            Tier = AbilityTiers.FirstUpgrade;
        if (StatsLvl == 2)
            Tier = AbilityTiers.SecondUpgrade;
        if (StatsLvl == 3)
            Tier = AbilityTiers.FinalUpgrade;
    }
    public void UseLingeringHealthAbility()
    {
        if (IsLingerHealthEnabled)
        {
            if (Tier == AbilityTiers.FirstUpgrade)
                healthUpgradeAmount = firstUpgradeAmount;
            if (Tier == AbilityTiers.SecondUpgrade)
                healthUpgradeAmount = secondUpgradeAmount;
            if (Tier == AbilityTiers.FinalUpgrade)
                healthUpgradeAmount = finalUpgradeAmount;

            PlayerInfoRef.SetHealth(healthUpgradeAmount);
        }   
    }
}
