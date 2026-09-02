using UnityEngine;

public class ExtraDashAbility : LevelUpStat
{
    public bool IsUsingExtraDash = false;
    private const int UpgradedDashAmount = 3;
    private const int DowngradedDashAmount = 2;
    public PlayerMovement PlayerMovementRef;
    public PlayerInfo PlayerInfoRef;
    public override void UpgradeStat()
    {
        if (PlayerInfoRef.Souls >= CostAmount)
        {
            IsUsingExtraDash = true;
            PlayerInfoRef.Souls -= (int)CostAmount;
            PlayerInfoRef.UpdatePlayersStats();
            UpdateStatsUI();
            UpgradeDashAmount();
        }
        else
        {
            Debug.Log("player does not have enough souls.");
        }
    }

    void UpgradeDashAmount() => PlayerMovementRef.MaxUsedDashes = UpgradedDashAmount;
}
