using System.Data.Common;
using TMPro;
using UnityEngine;

public class BossHealthUI : MonoBehaviour
{
    public string BossName;
    public TextMeshProUGUI BossDisplayNameText;
    public BaseEnemy BossSettings;
    public float Health, MaxHealth, Width, Height;
    public RectTransform HealthBar;


    private void Start()
    {
        SetUIMaxHealth(BossSettings.MaxEnemyHealth);
        Health = BossSettings.MaxEnemyHealth;
        MaxHealth = BossSettings.MaxEnemyHealth;
        BossDisplayNameText.text = BossName;

    }
    public void SetUIMaxHealth(float maxHealth)
    {
        MaxHealth = maxHealth;
    }

    public void SetUIHealth(float health)
    {
        Debug.Log("Hit boss for - " +  health);
        Health -= health;
        Debug.Log("whats the boss health " + Health);
        if (Health > MaxHealth)
            Health = MaxHealth;
        if (Health < 0)
            Health = 0;
        float newWidth = (Health / MaxHealth) * Width;
        HealthBar.sizeDelta = new Vector2(newWidth, Height);
    }
}
