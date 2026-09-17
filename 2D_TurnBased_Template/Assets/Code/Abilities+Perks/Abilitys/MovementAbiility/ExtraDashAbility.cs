using TMPro;
using UnityEngine;

public class ExtraDashAbility : LevelUpStat
{
    public bool IsUsingExtraDash = false;
    public GameObject UpgradeButton;
    public GameObject MaxButton;
    private const int UpgradedDashAmount = 3;
    private const int DowngradedDashAmount = 2;
    public PlayerMovement PlayerMovementRef;
    public PlayerInfo PlayerInfoRef;

    private void Start()
    {
        TurnOnUpgradeButton();
    }
    public override void UpgradeStat()
    {
        if (PlayerInfoRef.Souls >= CostAmount)
        {
            IsUsingExtraDash = true;
            PlayerInfoRef.Souls -= (int)CostAmount;
            PlayerInfoRef.UpdatePlayersStats();
            UpdateStatsUI();
            UpgradeDashAmount();
            TurnOnMaxButton();
            CostAmountText.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("player does not have enough souls.");
        }
    }

    void UpgradeDashAmount() => PlayerMovementRef.MaxUsedDashes = UpgradedDashAmount;

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
