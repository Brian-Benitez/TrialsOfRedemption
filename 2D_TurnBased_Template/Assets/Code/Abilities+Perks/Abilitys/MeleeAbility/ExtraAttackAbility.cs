using UnityEngine;

public class ExtraAttackAbility : LevelUpStat
{
    public bool IsUsingExtraAttack = false;
    public GameObject UpgradeButtonGameObject;
    public GameObject MaxButtonGameObject;
    private const int DefaultAmountOfAttack = 3;
    private const int UpgradedAmountOfAttacks = 4;
    [Header("Scripts")]
    public PlayerInfo PlayerInfoRef;
    public PlayerMeleeAttack PlayerMeleeAttackRef;

    private void Start()
    {
        TurnOnUpgradeButton();
    }
    public override void UpgradeStat()
    {
        if (PlayerInfoRef.Souls >= CostAmount)
        {
            PlayerInfoRef.Souls -= (int)CostAmount;
            PlayerInfoRef.UpdatePlayersStats();
            UpdateStatsUI();
            AddExtraAttackToPlayer();
            TurnOnMaxButton();
            CostAmountText.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("player does not have enough souls.");
        }
    }
    private void AddExtraAttackToPlayer()
    {
        Debug.Log("ability extra attack is enbaled");
        IsUsingExtraAttack = true;
        PlayerMeleeAttackRef.MaxAmountOfAttacks = UpgradedAmountOfAttacks;
    }

    void TurnOnUpgradeButton()
    {
        UpgradeButtonGameObject.SetActive(true);
        MaxButtonGameObject.SetActive(false);
    }

    void TurnOnMaxButton()
    {
        MaxButtonGameObject.SetActive(true);
        UpgradeButtonGameObject.SetActive(false);
    }
}
