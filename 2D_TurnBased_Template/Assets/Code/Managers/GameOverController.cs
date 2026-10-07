using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    public GameObject MainMenuPrefab;
    public GameObject GameOverPrefab;
    public GameObject LevelUpPrefab;
    public TextMeshProUGUI RoundsSurvived;
    [Header("Animator")]
    public Animator DeathScreenAnimator;

    [Header("Scripts")]
    public RoundController RoundControllerRef;
    public PlayerInfo PlayerInfoRef;
    public TypesOfEnemiesPerRoundController TypesOfEnemiesPerRoundControllerRef;

    public void GoToMainMenu()
    {
        GameOverPrefab.SetActive(false);
        MainMenuPrefab.SetActive(true);
    }
    public void TurnOnDeathScreen()
    {
        DeathScreenAnimator.SetBool("IsPlayingDeadScreen", true);
        RoundsSurvived.text = "" + RoundControllerRef.EnemiesWaveCounter;
    }

    public void TurnOffDeathScreen() => DeathScreenAnimator.SetBool("IsPlayingDeadScreen", false);

    public void RestartGame() 
    {
        PlayerInfoRef.XP = 0;
        RoundsSurvived.text = "" + RoundControllerRef.EnemiesWaveCounter;
        PlayerInfoRef.PlayersCore.SetActive(true);
        BuffEnemiesManager.Instance.StartRestartEnemiesShieldEvent();
        TypesOfEnemiesPerRoundControllerRef.RemoveAllEnemiesFromList();
        PlayerInfoRef.IsCharacterDead = false;
        PlayerInfoRef.HealthBarUIRef.SetUIHealth(PlayerInfoRef.BaseLineHealth);
        PlayerInfoRef.SetHealth(PlayerInfoRef.BaseLineHealth);
        GameOverPrefab.SetActive(false);
        RoundControllerRef.EnemiesWaveCounter = 0;
        RoundControllerRef.TotalAmountOfRoundsWon = 0;
        SoulsBankController.Instance.DemonBossSoulsBank = 0;
        SoulsBankController.Instance.SoulsBank = 0;
        PlayerSpawnerController.Instance.SpawnPlayerInCampfire();
        PerksController.Instance.RestartAllPlayersPerks();
        LevelUpPrefab.SetActive(true); 
    }
}
