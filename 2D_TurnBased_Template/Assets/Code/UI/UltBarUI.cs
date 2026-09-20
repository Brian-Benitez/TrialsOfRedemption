using System.Collections;
using UnityEngine;

public class UltBarUI : MonoBehaviour
{
    public static UltBarUI Instance;
    public float UltAmountUI, MaxUltAmountUI, Width, Height;

    public RectTransform UltBar;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void SetUIMaxUlt(float maxHealth)
    {
        MaxUltAmountUI = maxHealth;
    }

    public void SetUIUltBar(float amount)
    {
        UltAmountUI += amount;
        if (UltAmountUI > MaxUltAmountUI)
            UltAmountUI = MaxUltAmountUI;
        if (UltAmountUI < 0)
            UltAmountUI = 0;
            

        float newWidth = (UltAmountUI / MaxUltAmountUI) * Width;
        UltBar.sizeDelta = new Vector2(newWidth, Height);
    }

    public void StartDrianUltUICorutine() => StartCoroutine(DrainUltUI());
    IEnumerator DrainUltUI()
    {
        while(PlayersUltController.Instance.UltPoints > 0)
        {
            PlayersUltController.Instance.UltPoints--;
            yield return new WaitForSecondsRealtime(1f);
            SetUIUltBar(-.75f);
            UltAmountUI -= .75f;
        }
        if (UltAmountUI < 0)
            UltAmountUI = 0;
        
    }
}
