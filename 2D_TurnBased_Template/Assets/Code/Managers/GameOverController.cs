using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    public GameObject MainMenuPrefab;
    public GameObject GameOverPrefab;
    public GameObject LevelUpPrefab;
    public List<UpgradePerk> AllPerks;
    public List<LevelUpStat> AllStats;
    public TextMeshProUGUI RoundsSurvived;
    [Header("Scripts")]
    public RoundController RoundControllerRef;
    public PlayerInfo PlayerInfoRef;
    public TypesOfEnemiesPerRoundController TypesOfEnemiesPerRoundControllerRef;

    public void GoToMainMenu()
    {
        GameOverPrefab.SetActive(false);
        MainMenuPrefab.SetActive(true);
    }
    public void TurnOnGameOverScreen()
    {
        GameOverPrefab.SetActive(true);
        RoundsSurvived.text = "" + RoundControllerRef.EnemiesWaveCounter;
    }

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
        RestartAllPlayersPerks();
        LevelUpPrefab.SetActive(true); 
    }

    void RestartAllPlayersPerks()
    {
        for (int i = 0; i < AllPerks.Count; i++)
        {
            Debug.Log("turn off " +  AllPerks[i].gameObject.name);
            AllPerks[i].DisablePerk();
        }
    }

    void RestartAllPlayersStats()
    {
        for (int i = 0; i < AllStats.Count; i++)
        {
            AllStats[i].RestartStat();
        }

    }
}
