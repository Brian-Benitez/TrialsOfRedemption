using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

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

    public HealthBarUI HealthBarUIRef;
    public GameOverController GameOverControllerRef;
    public SecondChanceAbility SecondChanceAbilityRef;
    public void TakeDamage(float damage)
    {
        PostProcessingController.Instance.PlayCorutineHitEffect();
        CharacterHealthAmount -= damage;
        SetHealth(-damage);
        Debug.Log("player took: " + damage);
        DoesCharacterDie();
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
        if(SecondChanceAbilityRef.IsSecondChanceUsed == false && CharacterHealthAmount <= 0)
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
                IsCharacterDead = true;
                GameOverControllerRef.TurnOnGameOverScreen();
                SecondChanceAbilityRef.IsSecondChanceUsed = false;
                PlayersCore.SetActive(false);
            }
            else
            {
                Debug.Log("Still has health");
                IsCharacterDead = false;
            }
        }
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
