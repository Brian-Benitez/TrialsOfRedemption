using TMPro;
using UnityEngine;

public class PlayersUltController : MonoBehaviour
{
    public static PlayersUltController Instance;
    [Header("Ult Booleans")]
    public bool IsUsingPureRagePerk = false;
    public bool IsUsingHealingRagePerk = false;

    [Header("Ult Settings")]
    public bool IsUlted;
    public bool IsUpgradeOn;
    public float UltPoints;
    public float MaxUltPoints;
    public float UltDuration;
    public KeyCode UltActivationKey;

    [Header("Pure Rage Perk Settings")]
    public int BoostedMovementSpeed;
    public float LoweredDashCoolDown;
    public int MeleeUpgradeDam;
    public int RangeUpgradeDam;

    public TextMeshProUGUI UltAmountText;
    public TextMeshProUGUI MaxUltAmountText;

    public PlayerMovement PlayerMovementRef;
    public PlayerMeleeAttack PlayerMeleeAttackRef;
    public PlayerInfo PlayerInfoRef;
    public PureRagePerk PureRagePerkRef;
    public HealingRagePerk HealingRagePerkRef;
    public UltBarUI UltBarUIRef;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        UltBarUIRef.SetUIMaxUlt(MaxUltPoints);
    }


    private void Update()
    {
        if(Input.GetKeyDown(UltActivationKey))
        {
            if(!IsUlted && UltPoints >= MaxUltPoints || !IsUlted && UltPoints == MaxUltPoints / 2)
            {
                IsUlted = true;
                UltBarUI.Instance.StartDrianUltUICorutine();
            }
        }

        if(IsUlted && UltDuration > 0)//add another check so it runs once
        {
            UltDuration -= Time.deltaTime;
        }
        else if(UltDuration <= 0)
        {
            IsUlted = false;
            IsUpgradeOn = false;
            ResettingPlayerFromPerk();
            RemoveAllUltPoints();
            UltDuration += MaxUltPoints;
        }

        if(IsUlted && !IsUpgradeOn)
        {
            IsUpgradeOn = true;
            Debug.Log("Start ult");
            ActivateRagePerk();
        }
    }

    public void ActivateRagePerk()
    {
        if (IsUsingPureRagePerk)
            PureRagePerkRef.ActivatePureRagePerk();
        if (IsUsingHealingRagePerk)
            HealingRagePerkRef.ActivateHealPerk();
    }

    public void ResettingPlayerFromPerk()
    {
        if (IsUsingPureRagePerk)
            SetPlayerToNormalStats();
    }

    public void AddUltPoint(float amount)
    {
        if(UltPoints >= MaxUltPoints)
            UltPoints = MaxUltPoints;
        else
            UltPoints += amount / 4f;

        PlayerInfoRef.UpdatePlayersStats();
        UltBarUIRef.SetUIUltBar(amount / 4f);
    }

    public void RemoveAllUltPoints() => UltPoints = 0;
    
    public void SetPlayerToNormalStats()
    {
        //Movement upgrade
        PlayerMovementRef.FullSpeed -= BoostedMovementSpeed;
        PlayerMovementRef.DashCoolDown += LoweredDashCoolDown;

        //Melee upgrade
        PlayerMeleeAttackRef.PlayerLightAttkDamg -= MeleeUpgradeDam;
        PlayerMeleeAttackRef.PlayerHeavyAttkDamg -= MeleeUpgradeDam;

        //Range upgrade
        PlayerInfoRef.RangeDamg -= RangeUpgradeDam;
        IsUsingPureRagePerk = false;
    }
}
