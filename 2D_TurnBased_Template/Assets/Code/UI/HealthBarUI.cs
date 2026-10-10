using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public float Health, MaxHealth, Width, Height;

    public RectTransform HealthBar;
    public GameObject TheHealthBar;
    public GameObject DefaultPOS;
    public GameObject FirstUpgradePOS;
    public GameObject SecondUpgradePOS;
    public GameObject LastUpgradePOS;
    int _upgradeIndex = 0;

    public void SetUIMaxHealth(float maxHealth)
    {
        MaxHealth = maxHealth;
    }

    public void UpdateHealthBarUI()
    {
        _upgradeIndex ++;
        RectTransform currenthealthbar = TheHealthBar.GetComponent<RectTransform>();
        float newWidth = Width + 100f;
        Width += 100f;
        currenthealthbar.sizeDelta = new Vector2(newWidth, Height);
        if (_upgradeIndex == 1)
            TheHealthBar.transform.position = FirstUpgradePOS.transform.position;
        else if(_upgradeIndex == 2)
            TheHealthBar.transform.position = SecondUpgradePOS.transform.position;
        else if( _upgradeIndex == 3)
            TheHealthBar.transform.position = LastUpgradePOS.transform.position;
        Debug.Log("i played");
    }

    public void SetUIHealth(float health)
    {
        Health = health;

        if (Health > MaxHealth)
            Health = MaxHealth;
        if (Health < 0)
            Health = 0;
        float newWidth = (Health / MaxHealth) * Width;
        HealthBar.sizeDelta = new Vector2 (newWidth, Height);
    }
}
