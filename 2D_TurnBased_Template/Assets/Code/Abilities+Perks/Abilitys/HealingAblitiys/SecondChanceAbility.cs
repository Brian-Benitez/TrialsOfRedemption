using TMPro;
using UnityEngine;

public class SecondChanceAbility : LevelUpStat
{
    public float ReviveAmountForPlayer;
    public bool IsSecondChanceEnabled = false;
    public bool IsSecondChanceUsed = false;
    public TextMeshProUGUI DescriptionNumberAmount;
    public TextMeshProUGUI CurrentAmount;
    public GameObject UpgradeButton;
    public GameObject MaxButton;
    private const float firstReviveHealthAmount = 50f;
    private const float secondReviveHealthAmount = 100f;
    private const float lastReviveHealthAmount = 150f;

    private void Start()
    {
        CostAmountText.text = " " + CostAmount;
    }

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
            IsSecondChanceEnabled = true;
            PlayerInfoRef.Souls -= (int)CostAmount;
            PlayerInfoRef.UpdatePlayersStats();
            UpdateStatsUI();
            DetermineCurrentTier();
            ChangeAllText();
            AudioController.Instance.PlayClickedOnButtonSFX();
        }
        else
        {
            Debug.Log("player does not have enough souls.");
            AudioController.Instance.PlayUILockedSFX();
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
            CostAmountText.gameObject.SetActive(false);
            TurnOnMaxButton();
        }
            
    }
    public void ActivateSecondChanceAbility()
    {
        if(IsSecondChanceEnabled && IsSecondChanceUsed == false)
        {
            IsSecondChanceUsed = true;
        }
    }

    void ChangeAllText()
    {
        CurrentAmount.text = ReviveAmountForPlayer.ToString();

        if (StatsLvl == 1)
            DescriptionNumberAmount.text = secondReviveHealthAmount.ToString();
        if (StatsLvl == 2)
            DescriptionNumberAmount.text = lastReviveHealthAmount.ToString();
    }
    void TurnOnUpgradeButton()
    {
        UpgradeButton.SetActive(true);
        MaxButton.SetActive(false);
    }

    void TurnOnMaxButton()
    {
        MaxButton.SetActive(true);
        UpgradeButton.SetActive(false);
    }
}
