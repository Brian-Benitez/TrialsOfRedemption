using TMPro;
using UnityEngine;

public class HealPlayerScript : LevelUpStat
{
    public TextMeshProUGUI DescriptionAmount;
    public TextMeshProUGUI CurrentText;
    public GameObject UpgradeButton;
    public GameObject MaxButton;
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
            UpdateStatsUI();
            DetermineCurrentTier();
            PlayerInfoRef.CharacterMaxHealth += upgradeHealthAmount;
            PlayerInfoRef.HealthBarUIRef.SetUIMaxHealth(PlayerInfoRef.CharacterMaxHealth);
            PlayerInfoRef.SetHealth(PlayerInfoRef.CharacterHealthAmount);
            PlayerInfoRef.Souls -= (int)CostAmount;
            PlayerInfoRef.UpdatePlayersStats();
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
        {
            CurrentTier = UpgradeTiers.LastUpgrade;
            CostAmountText.gameObject.SetActive(false);
            TurnOnMaxButton();
        }
            

        DetermineHealthAmount();
        ChangeText();
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


    void ChangeText()
    {
        CurrentText.text = upgradeHealthAmount.ToString();

        if(CurrentTier == UpgradeTiers.FirstUpgrade)
            DescriptionAmount.text = secondLevelHealthUpgrade.ToString();
        if(CurrentTier == UpgradeTiers.SecondUpgrade)
            DescriptionAmount.text = lastLevelHealthUpgrade.ToString();
    }
    void TurnOnMaxButton()
    {
        UpgradeButton.gameObject.SetActive(false);
        MaxButton.SetActive(true);
    }

}
