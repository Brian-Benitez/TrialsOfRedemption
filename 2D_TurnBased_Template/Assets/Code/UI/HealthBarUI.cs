using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public float Health, MaxHealth, Width, Height;

    public RectTransform HealthBar;

    public void SetUIMaxHealth(float maxHealth)
    {
        MaxHealth = maxHealth;
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
