using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class BaseCharacter : MonoBehaviour// need to move melee and rage values here. Want this to be main place to change values
{
    [Header("Health")]
    public float CharacterHealthAmount;
    public float CharacterMaxHealth;
    public float BaseLineHealth = 15f;

    [Header("Range Dmg")]
    public float RangeDamg;
    [Header("Souls/XP")]
    public int Souls;
    public int BossSouls;
    public float XP;
    [Header("Booleans")]
    public bool IsCharacterDead = false;
    [Header("Texts")]
    public TextMeshProUGUI UpgradeUISoulsText;// will fix this later
    public TextMeshProUGUI PerksUISoulsText;//this too
    public TextMeshProUGUI InGameSoulsText;
    public TextMeshProUGUI InGameBossSoulText;
    public TextMeshProUGUI UltAmountText;
    public TextMeshProUGUI MaxUltAmountText;
    public TextMeshProUGUI ArrowCountText;

    public GameObject PlayersCore;
    [Header("Scripts")]
    public HealthBarUI HealthBarUIRef;
    public GameOverController GameOverControllerRef;
    public SecondChanceAbility SecondChanceAbilityRef;
    public NpcController NpcControllerRef;
    public PlayerAnimationController PlayerAnimationControllerRef;
    public PlayerMovement PlayerMovementRef;

    private void Update()
    {
        if (IsCharacterDead)
            PlayerMovementRef.TurnOnStopPlayerMovement();
    }
    public void TakeDamage(float damage)
    {
        if(!IsCharacterDead)
        {
            PostProcessingController.Instance.PlayCorutineHitEffect();
            CharacterHealthAmount -= damage;
            SetHealth(-damage);
            PlayerAnimationControllerRef.IsHurt();
            Debug.Log("player took: " + damage);
            DoesCharacterDie();
        }
    }

    public void SetHealth(float healthChange)
    {
        CharacterHealthAmount += healthChange;
        CharacterHealthAmount = Mathf.Clamp(CharacterHealthAmount, 0, CharacterMaxHealth);
        healthChange = Mathf.Clamp(CharacterHealthAmount, 0, CharacterMaxHealth);
        HealthBarUIRef.SetUIHealth(healthChange);
    }

    public void DoesCharacterDie()
    {
        if(SecondChanceAbilityRef.CurrentTier != SecondChanceAbility.UpgradeTiers.None && SecondChanceAbilityRef.IsSecondChanceUsed == false && CharacterHealthAmount <= 0)
        {
            SetHealth(SecondChanceAbilityRef.ReviveAmountForPlayer);
            HealthBarUIRef.SetUIHealth(SecondChanceAbilityRef.ReviveAmountForPlayer);
            SecondChanceAbilityRef.ActivateSecondChanceAbility();
            Debug.Log("Used Second Chance!");
        }
        else
        {
            if (CharacterHealthAmount <= 0)
            {
                PlayerIsDead();
            }
            else
            {
                Debug.Log("Still has health");
                IsCharacterDead = false;
            }
        }
    }

    void PlayerIsDead()
    {
        PlayerMovementRef.StopPlayerMovement = true;
        IsCharacterDead = true;
        NpcControllerRef.IncrementLayoutIndex();
        PlayersUltController.Instance.UltPoints = 0f;
        UltBarUI.Instance.UltAmountUI = 0;
        UltBarUI.Instance.SetUIUltBar(-UltBarUI.Instance.MaxUltAmountUI);
        XP = 0;
        XPController.Instance.RestartLevelUpThershold();
        XPBarUI.Instance.SetUIXP(-XPBarUI.Instance.MaxXPAmountUI);
        SecondChanceAbilityRef.IsSecondChanceUsed = false;
        //PlayersCore.SetActive(false);
        PostProcessingController.Instance.PlayCorutineDeathTunnelVision();
        StartCoroutine(PlayingDeathScreenTransition());
    }

    IEnumerator PlayingDeathScreenTransition()
    {
        PlayerAnimationControllerRef.IsDead();
        yield return new WaitForSecondsRealtime(2f);
        GameOverControllerRef.TurnOnDeathScreen();
        yield return new WaitForSecondsRealtime(3f);
        GameOverControllerRef.RestartGame();
        GameOverControllerRef.TurnOffDeathScreen();
        PostProcessingController.Instance.StopDeathTunnelVison();
        PlayerAnimationControllerRef.IsNotDead();
        PlayerMovementRef.StopPlayerMovement = false;
    }

    public void UpdatePlayersStats()
    {
        UpgradeUISoulsText.text = " " + Souls;
        PerksUISoulsText.text = " " + Souls;
        InGameSoulsText.text = " " + Souls;
        InGameBossSoulText.text = " " + BossSouls;
        UltAmountText.text = " " + PlayersUltController.Instance.UltPoints;
        MaxUltAmountText.text = " " + PlayersUltController.Instance.MaxUltPoints;
        ArrowCountText.text = " " + PlayerAmmoController.Instance.AmmoAmount;
    }
}
