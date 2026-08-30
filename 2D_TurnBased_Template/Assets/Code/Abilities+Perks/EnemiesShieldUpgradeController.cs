using System.Collections.Generic;
using UnityEngine;

public class EnemiesShieldUpgradeController : MonoBehaviour
{
    public float UpgradeIncrements;
    private float ShieldUpgradeIncrement;

    public void AddToShieldIncrement() => ShieldUpgradeIncrement += UpgradeIncrements;
    public void RestartShieldIncrements() => ShieldUpgradeIncrement = 0;

    public void UpgradeEnemyShields(List<GameObject> enemies)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].GetComponentInChildren<EnemyShield>().EnemyShieldHealth = 0;
            enemies[i].GetComponentInChildren<EnemyShield>().EnemyShieldHealth += ShieldUpgradeIncrement;
            enemies[i].GetComponentInChildren<EnemyShield>().TryTurningOnShield();
        }
    }

    public void RestartEnemyShields(List<GameObject> enemies)
    {
        RestartShieldIncrements();
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].GetComponentInChildren<EnemyShield>().EnemyShieldHealth = 0;
            enemies[i].GetComponentInChildren<EnemyShield>().TurnOffShield();
        }
    }
}
