using TMPro;
using UnityEngine;

public class LevelUpManager : MonoBehaviour
{
    [Header("Upgrade imcrements")]
    public float HealthUpgradeImcrement;
    public float RangeUpgradeImcrement;
    public float RageUpgradeIncrement;
    public float MeleeUpgradeIncrement;
    public float ShieldUpgradeIncrement;

    [Header("Cost per Upgrade")]
    public int CostForHealthUpgrade;
    public int CostForRangeUpgrade;
    public int CostForDashUpgrade;
    public int CostyForMeleeUpgrade;
    public int CostForRageUpgrade;
    public int CostForShieldUpgrade;

    [Header("Texts")]
    public TextMeshProUGUI HealthCostAmountText;
    public TextMeshProUGUI MeleeCostAmountText;
    public TextMeshProUGUI RageCostAmountText;
    public TextMeshProUGUI DashCostAmountText;
    public TextMeshProUGUI ShieldCostAmountText;
    public TextMeshProUGUI RangeCostAmountText;

    [Header("Percentage Text")]
    public TextMeshProUGUI HealthPercentText;
    public TextMeshProUGUI MeleePercentText;
    public TextMeshProUGUI RagePercentText;
    public TextMeshProUGUI DashPercentText;
    public TextMeshProUGUI RangePercentText;
    public TextMeshProUGUI ShieldPercentText;

    [Header("Buttons")]
    public GameObject HealthButton;
    public GameObject RageButton;
    public GameObject MeleeButton;
    public GameObject DashButton;
    public GameObject RangeButton;
    public GameObject ShieldButton;

    [Header("Max Texts")]
    public TextMeshProUGUI HealthMaxText;
    public TextMeshProUGUI MeleeMaxText;
    public TextMeshProUGUI RageMaxText;
    public TextMeshProUGUI DashMaxText;
    public TextMeshProUGUI RangeMaxText;
    public TextMeshProUGUI ShieldMaxText;

    [Header("Scripts")]
    public PlayerMovement PlayerMovementRef;
    public PlayerInfo _playerInfo;
    public PlayerMeleeAttack PlayerMeleeAttackRef;

    [Header("Level caps")]
    private float _healthpercent;
    private int _healthUpgradeLevel = 0;
    public int _maxHealthUpgradeLevel;

    private float _meleeUpgradePercent;
    private int _meleeUpgradeLevel = 0;
    public int _maxMeleeUpgradeLevel;

    private float _rangeUpgradePercent;
    private int _rangeUpgradeLevel = 0;
    public int _maxRangeUpgradeLevel;

    private float _rageUpgradePercent;
    private int _rageUpgradeLevel = 0;
    public int _maxRageUpgradeLevel;

    private float _dashUpgradePercent;
    private int _dashUpgradeLevel = 0;
    public int _maxDashUpgradeLevel;

    private float _shieldUpgradePercent;
    private int _shieldUpgradeLevel = 0;
    public int _maxShieldUpgradeLevel;

    public void UpgradePlayerHealth()//used OnClick for UI.
    {
        if(_playerInfo.Souls >= CostForHealthUpgrade)
        {
            if(_maxHealthUpgradeLevel == _healthUpgradeLevel)
            {
                Debug.Log("Max upgrade has been reached.");
                DisableUpgradeButton(HealthMaxText, HealthButton);
            }
            else
            {
                Debug.Log("upgraded health + " + HealthUpgradeImcrement);
                _playerInfo.CharacterMaxHealth += HealthUpgradeImcrement;
                _healthpercent += HealthUpgradeImcrement;
                _healthUpgradeLevel += 1;
                _playerInfo.HealthBarUIRef.SetUIMaxHealth(_playerInfo.CharacterMaxHealth);
                _playerInfo.SetHealth(_playerInfo.CharacterMaxHealth);
                _playerInfo.Souls -= CostForHealthUpgrade;
                _playerInfo.UpdatePlayersStats();
                CostForHealthUpgrade *= 2;
                UpdateUIForUpgradeMenu(HealthCostAmountText, HealthPercentText, CostForHealthUpgrade, _healthpercent);
            }
        }
        else
        {
            Debug.Log("Player does not have enough souls.");
        }
    }

    public void UpgradeMeleeDam()
    {
        if(_maxMeleeUpgradeLevel == _meleeUpgradeLevel)
        {
            Debug.Log("maxed out");
            DisableUpgradeButton(MeleeMaxText, MeleeButton);
        }
        else
        {
            if (_playerInfo.Souls >= CostyForMeleeUpgrade)
            {
                Debug.Log("upgraded melee");
                PlayerMeleeAttackRef.PlayerLightAttkDamg += MeleeUpgradeIncrement;
                PlayerMeleeAttackRef.PlayerHeavyAttkDamg += MeleeUpgradeIncrement;
                PlayerMeleeAttackRef.PlayerSpecialDamg += MeleeUpgradeIncrement;
                _meleeUpgradePercent += MeleeUpgradeIncrement;
                _meleeUpgradeLevel += 1;
                _playerInfo.Souls -= CostyForMeleeUpgrade;
                _playerInfo.UpdatePlayersStats();
                CostyForMeleeUpgrade *= 2;
                UpdateUIForUpgradeMenu(MeleeCostAmountText, MeleePercentText, CostyForMeleeUpgrade, _meleeUpgradePercent);
            }
        }
    }
    public void UpgradeRangeDamage()//use for OnClick for UI.
    {
        if(_maxRangeUpgradeLevel == _rangeUpgradeLevel)
        {
            Debug.Log("maxed out");
            DisableUpgradeButton(RangeMaxText, RangeButton);
        }
        else
        {
            if (_playerInfo.Souls >= CostForRangeUpgrade)
            {
                Debug.Log("Upgraded range damage + " + CostForRangeUpgrade);
                _playerInfo.RangeDamg += RangeUpgradeImcrement;
                _rangeUpgradePercent += RangeUpgradeImcrement;
                _rangeUpgradeLevel += 1;
                _playerInfo.Souls -= CostForRangeUpgrade;
                _playerInfo.UpdatePlayersStats();
                CostForRangeUpgrade *= 2;
                UpdateUIForUpgradeMenu(RangeCostAmountText, RangePercentText, CostForRangeUpgrade, _rangeUpgradePercent);
            }
        }
    }

    public void UpgradeRage()
    {
        if(_maxRageUpgradeLevel == _rageUpgradeLevel)
        {
            Debug.Log("maxxed out");
            DisableUpgradeButton(RageMaxText, RageButton);
        }
        else
        {
            if (_playerInfo.Souls >= CostForRageUpgrade)
            {
                Debug.Log("upgraded rage");
                _playerInfo.Souls -= CostForRageUpgrade;
                _rageUpgradePercent += RageUpgradeIncrement;
                _rageUpgradeLevel += 1;
                PlayersUltController.Instance.MaxUltPoints += RageUpgradeIncrement;
                _playerInfo.UpdatePlayersStats();
                CostForRageUpgrade *= 2;
                UpdateUIForUpgradeMenu(RageCostAmountText, RagePercentText, CostForRageUpgrade, _rageUpgradePercent);
            }
        }
    }
    public void UpgradeDash()
    {
        if(_maxDashUpgradeLevel == _dashUpgradeLevel)
        {
            Debug.Log("maxxed out");
            DisableUpgradeButton(DashMaxText, DashButton);
        }
        else
        {
            if (_playerInfo.Souls >= CostForDashUpgrade)
            {
                PlayerMovementRef.DashCoolDown -= PlayerMovementRef.DashCoolDownUpgrade;
                _playerInfo.Souls -= CostForDashUpgrade;
                _dashUpgradeLevel += 1;
                _dashUpgradePercent -= PlayerMovementRef.DashCoolDownUpgrade;
                _playerInfo.UpdatePlayersStats();
                CostForDashUpgrade *= 2;
                UpdateUIForUpgradeMenu(DashCostAmountText, DashPercentText, CostForDashUpgrade, _dashUpgradePercent);
            }
        }
    }

    public void UpgradeShield()
    {
        if(_maxShieldUpgradeLevel == _shieldUpgradeLevel)
        {
            Debug.Log("maxxed out");
            DisableUpgradeButton(ShieldMaxText, ShieldButton);
        }
        else
        {
            if (_playerInfo.Souls >= CostForShieldUpgrade)
            {
                _playerInfo.Souls -= CostForShieldUpgrade;
                _shieldUpgradePercent += ShieldUpgradeIncrement;
                _shieldUpgradeLevel += 1;
                _playerInfo.UpdatePlayersStats();
                ShieldController.Instance.UpgradeShield(ShieldUpgradeIncrement);
                CostForShieldUpgrade *= 2;
                UpdateUIForUpgradeMenu(ShieldCostAmountText, ShieldPercentText, CostForShieldUpgrade, _shieldUpgradePercent);
            }
        }
    }
    public void UpdateUIForUpgradeMenu(TextMeshProUGUI costText, TextMeshProUGUI percentUI, int costAmount, float upgradeIncrement)
    {
        costText.text = " " + costAmount;
        percentUI.text = " " + (decimal)upgradeIncrement + "%";
        Debug.Log("cost amount " + costAmount + " what is should be " + costAmount * 2);
    }

    public void DisableUpgradeButton(TextMeshProUGUI maxText, GameObject button)
    {
        button.SetActive(false);
        maxText.gameObject.SetActive(true);
    }

    public void EnableUpgradeButton(TextMeshProUGUI maxText, GameObject button)
    {
        button.SetActive(true);
        maxText.gameObject.SetActive(false);
    }
}
