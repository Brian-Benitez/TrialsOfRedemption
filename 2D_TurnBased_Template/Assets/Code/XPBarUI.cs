using UnityEngine;

public class XPBarUI : MonoBehaviour
{
    public static XPBarUI Instance;

    public float XPAmountUI, MaxXPAmountUI, Width, Height;

    public RectTransform XPBar;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        SetXPMaxHealth();
    }
    public void SetXPMaxHealth()
    {
        MaxXPAmountUI = XPController.Instance.LevelUpThershold;
    }

    public void SetUIXP(float amountOfXPWon)//check when player hits cap and levels up.
    {
        XPAmountUI += amountOfXPWon;
        float newWidth = (XPAmountUI / MaxXPAmountUI) * Width;
        XPBar.sizeDelta = new Vector2(newWidth, Height);
    }
}
