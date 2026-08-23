using UnityEngine;

public class XPController : MonoBehaviour
{
    public static XPController Instance;
    public bool IsUpgrading = false;
    public float LevelUpThershold;
    public float MinLevelUpThershold;
    public float ThersholdMultiplier;
    public float MinLevelUpMultiplier;
    public BaseCharacter PlayerStats;
    public PerkCardController PerkCardControllerRef;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    private void Start()
    {
        MinLevelUpMultiplier = ThersholdMultiplier;
        MinLevelUpMultiplier = LevelUpThershold;
    }

    public void AddXPToPlayer(float xpamount)
    {
        PlayerStats.XP += xpamount;
        XPBarUI.Instance.SetUIXP(xpamount);
    }

    public void CanLevelUp()
    {
        if(PlayerStats.XP >= LevelUpThershold)
        {
            Debug.Log("player can level up");
            IsUpgrading = true;
            PerkCardControllerRef.RandomlyPickingChoiceCards();
            PerkCardControllerRef.MoveBackroundOnScreen();
            PerkCardControllerRef.StartPlaceCardsOnScreenCoroutine();
            RaiseLevelUpThershold();
        }
    }

    public void RaiseLevelUpThershold()
    {
        float newThershold = MinLevelUpThershold * ThersholdMultiplier;
        LevelUpThershold += newThershold;
        ThersholdMultiplier += 0.5f;
    }
}
