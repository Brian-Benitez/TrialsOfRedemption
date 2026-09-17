using TMPro;
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
    public TextMeshProUGUI DescriptionAmountText;
    public TextMeshProUGUI CurrentAmountText;
    public GameObject UpgradeButton;
    public GameObject MaxButton;
    private const int FirstUpgradeAmount = 10;
    private const int SecondUpgradeAmount = 50;
    private const int LastUpgradeAmount = 9999;
    public PlayerInfo PlayerInfoRef;

    private void Start()
    {
        TurnOnUpgradeButton();
    }
    public override void UpgradeStat()
    {
        if (PlayerInfoRef.Souls >= CostAmount)
        {
            IsUsingHellsRetriver = true;
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
        {
            CurrentTier = UpgradeTiers.FirstUpgrade;
        }
        if (StatsLvl == 2)
        {
            CurrentTier = UpgradeTiers.SecondUpgrade;
        }
        if(StatsLvl == 3)
        {
            CurrentTier = UpgradeTiers.LastUpgrade;
            TurnOnMaxButton();
            CostAmountText.gameObject.SetActive(false); 
        }
         
        ChangeAllText();
    }
    public void AddXAmountOfArrowsToPlayer()
    {
        if(IsUsingHellsRetriver)
        {
            if (CurrentTier == UpgradeTiers.FirstUpgrade)
            {
                PlayerAmmoController.Instance.AmmoAmount += FirstUpgradeAmount;
            }
            if (CurrentTier == UpgradeTiers.SecondUpgrade)
            {
                PlayerAmmoController.Instance.AmmoAmount += SecondUpgradeAmount;
            }
            if (CurrentTier == UpgradeTiers.LastUpgrade)
            {
                PlayerAmmoController.Instance.AmmoAmount += LastUpgradeAmount;
                CostAmountText.gameObject.SetActive(false);
                TurnOnMaxButton();
            }
        }
    }

    void ChangeAllText()
    {
        
        if(CurrentTier == UpgradeTiers.FirstUpgrade)
        {
            DescriptionAmountText.text = SecondUpgradeAmount.ToString();
            CurrentAmountText.text = FirstUpgradeAmount.ToString();
        }

        if (CurrentTier == UpgradeTiers.SecondUpgrade)
        {
            DescriptionAmountText.text = LastUpgradeAmount.ToString();
            CurrentAmountText.text = SecondUpgradeAmount.ToString();
        }

        if(CurrentTier == UpgradeTiers.LastUpgrade)
        {
            CurrentAmountText.text = LastUpgradeAmount.ToString();
        }
            
    }

    void TurnOnMaxButton()
    {
        UpgradeButton.SetActive(false);
        MaxButton.SetActive(true);
    }

    void TurnOnUpgradeButton()
    {
        UpgradeButton.SetActive(true);
        MaxButton.SetActive(false);
    }
}
