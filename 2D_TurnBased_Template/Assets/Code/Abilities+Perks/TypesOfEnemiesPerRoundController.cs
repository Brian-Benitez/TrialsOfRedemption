using System.Collections.Generic;
using UnityEngine;

public class TypesOfEnemiesPerRoundController : MonoBehaviour
{
    [Header("Max enemies in round")]
    public int MaxAmountOfEnemies;
    public int MaxAmountOfBosses;

    [Header("Enemies")]
    public List<GameObject> ListOfEnemies;
    public List<GameObject> TypesOfInGameEnemies;
    public List<GameObject> EyeMosnters;
    public List<GameObject> SwordsmanGameObjects;
    public List<GameObject> AOEEnemies;
    public List<GameObject> ArchersGameObjects;
    public List<GameObject> Wizards;
    public GameObject BossGameObject;

    public RoundController RoundControllerRef;
    public EnemiesSpawner EnemiesSpawnerRef;
    public EnemiesShieldUpgradeController UpgradeEnemiesControllerRef;

    
    public void TypeOfEnemiesForRound()
    {
        switch (RoundControllerRef.EnemiesWaveCounter)
        {
            case 1://just level one sword enemies
                FirstWaveEnemies();
                Debug.Log("this is round 1");
                break;

            case 2://level one archers and swords
                SecondWaveEnemies();
                Debug.Log("this is round 2");
                break;

            case 3://level two archers and swords
                ThirdWaveEnemies();
                Debug.Log("this is round 3");
                break;

            case 4://level three archers and swords
                FourthWaveEnemies();
                Debug.Log("this is round 4");
                break;

            case 5://either all archers or swords lvl 3
                FifthWaveEnemies();
                break;

            case 6:
                SixthWaveEnemies();
                break;

            case 7:
                SeventhWaveEnemies();
                break;

            case 8:
                EighthWaveEnemies();
                break;
            
            case 9:
                NintheWaveEnemies();
                Debug.Log("whats up");
                break;

            case 10://boss 
                TenthWaveEnemies();
                Debug.Log("this is round 10");
                break;

            default:
                Debug.Log("what");
                break;

        }
    }

    void FirstWaveEnemies()
    {
        TypesOfInGameEnemies.Add(EyeMosnters[0]);
    }

    void SecondWaveEnemies()
    {
        TypesOfInGameEnemies.Add(EyeMosnters[0]);
        TypesOfInGameEnemies.Add(SwordsmanGameObjects[0]);
        Debug.Log("added level one enemies");
    }

    void ThirdWaveEnemies()
    {
       TypesOfInGameEnemies.Add(EyeMosnters[0]);
       TypesOfInGameEnemies.Add(ArchersGameObjects[0]);
    }

    void FourthWaveEnemies()
    {
        TypesOfInGameEnemies.Add(SwordsmanGameObjects[0]);
        TypesOfInGameEnemies.Add(ArchersGameObjects[0]);
        Debug.Log("added both archers and swordsman as enemies");
    }

    void FifthWaveEnemies()
    {
        TypesOfInGameEnemies.Add(SwordsmanGameObjects[0]);
        TypesOfInGameEnemies.Add(ArchersGameObjects[1]);
        Debug.Log("added both archers and swordsman as enemies Lvl 2");
    }

    void SixthWaveEnemies()
    {
        TypesOfInGameEnemies.Add(SwordsmanGameObjects[0]);
        TypesOfInGameEnemies.Add(Wizards[0]);
    }

    void SeventhWaveEnemies()
    {
        bool isAllArchers = false;
        bool isAllSwordsman = false;//can add more later and more unique

        int results = Random.Range(0, 1);
        if (results == 0)
        {
            isAllArchers = true;
            Debug.Log("is a all archer turn");
        }
        else if (results == 1)
        {
            Debug.Log("is a all swordsman turn");
            isAllSwordsman = true;
        }

        if (isAllArchers)
        {
            TypesOfInGameEnemies.Add(ArchersGameObjects[1]);
        }
        else if (isAllSwordsman)
        {
            TypesOfInGameEnemies.Add(SwordsmanGameObjects[1]);
        }
    }

    void EighthWaveEnemies()
    {
        TypesOfInGameEnemies.Add(AOEEnemies[0]);
        TypesOfInGameEnemies.Add(SwordsmanGameObjects[1]);
    }

    void NintheWaveEnemies()
    {
        TypesOfInGameEnemies.Add(AOEEnemies[0]);
        TypesOfInGameEnemies.Add(ArchersGameObjects[1]);
    }
    void TenthWaveEnemies() => TypesOfInGameEnemies.Add(BossGameObject);

    public void RemoveAllEnemiesFromList()
    {
        if(ListOfEnemies != null)
        {
            for (int i = 0; i < ListOfEnemies.Count; i++)
            {
                Destroy(ListOfEnemies[i]);
            }
        }
       
        TypesOfInGameEnemies.Clear();
        ListOfEnemies.Clear();
        EnemiesSpawnerRef.EnemiesAlive = 0;
        EnemiesSpawnerRef.IsAllEnemiesDead = true;
    }

    public void UpgradeAllEnemies()
    {
        UpgradeEnemiesControllerRef.UpgradeEnemyShields(EyeMosnters);
        UpgradeEnemiesControllerRef.UpgradeEnemyShields(SwordsmanGameObjects);
        UpgradeEnemiesControllerRef.UpgradeEnemyShields(AOEEnemies);
        UpgradeEnemiesControllerRef.UpgradeEnemyShields(ArchersGameObjects);
        UpgradeEnemiesControllerRef.UpgradeEnemyShields(Wizards);
    }

    public void RestartEnemiesShield()
    {
        UpgradeEnemiesControllerRef.RestartEnemyShields(EyeMosnters);
        UpgradeEnemiesControllerRef.RestartEnemyShields(SwordsmanGameObjects);
        UpgradeEnemiesControllerRef.RestartEnemyShields(AOEEnemies);
        UpgradeEnemiesControllerRef.RestartEnemyShields(ArchersGameObjects);
        UpgradeEnemiesControllerRef.RestartEnemyShields(Wizards);
    }
}
