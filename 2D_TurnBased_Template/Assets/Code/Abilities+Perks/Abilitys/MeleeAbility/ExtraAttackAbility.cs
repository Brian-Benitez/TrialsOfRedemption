using UnityEngine;

public class ExtraAttackAbility : LevelUpStat
{
    private const int DefaultAmountOfAttack = 3;
    private const int UpgradedAmountOfAttacks = 4;
    public PlayerInfo PlayerInfoRef;
    public PlayerMeleeAttack PlayerMeleeAttackRef;
    public override void UpgradeStat()
    {
        if (PlayerInfoRef.Souls >= CostAmount)
        {
            PlayerInfoRef.Souls -= (int)CostAmount;
            PlayerInfoRef.UpdatePlayersStats();
            UpdateStatsUI();
            AddExtraAttackToPlayer();
        }
        else
        {
            Debug.Log("player does not have enough souls.");
        }
    }
    private void AddExtraAttackToPlayer()
    {
        Debug.Log("ability extra attack is enbaled");
        PlayerMeleeAttackRef.MaxAmountOfAttacks = UpgradedAmountOfAttacks;
    }
}
