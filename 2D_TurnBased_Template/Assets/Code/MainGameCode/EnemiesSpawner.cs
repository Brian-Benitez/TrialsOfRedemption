using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    public static EnemiesSpawner Instance;

    [Header("Note: Only make enemies be an even amount of them")]
    public int EnemiesAlive;
    private int _maxForOneCertainEnemy = 5;
    public bool IsAllEnemiesDead = false;
    [Header("Spawns Points")]
    public List<List<GameObject>> AllListOfSpawnPoints;
    public List<GameObject> SpawnPointACount8;
    public List<GameObject> SpawnPointBCount8;
    public List<GameObject> BossSpawnPoints;

    public TypesOfEnemiesPerRoundController TypesOfEnemiesPerRoundControllerRef;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    private void Start()
    {
        AllListOfSpawnPoints = new List<List<GameObject>>();
        AllListOfSpawnPoints.Add(SpawnPointACount8);
        AllListOfSpawnPoints.Add(SpawnPointBCount8);
    }


    public void PickASpawnPointAndSpawn()
    {
        if(TypesOfEnemiesPerRoundControllerRef.RoundControllerRef.EnemiesWaveCounter == 10)//Boss fight
        {
            SpawnEnemiesOnSpawnPoints(BossSpawnPoints);
            Debug.Log("boss time");
        }
        else
        {
            int listindex = Random.Range(0, AllListOfSpawnPoints.Count);
            SpawnEnemiesOnSpawnPoints(AllListOfSpawnPoints[listindex]);
            Debug.Log("grunt time");
        }      
    }
    public void SpawnEnemiesOnSpawnPoints(List<GameObject> listofspawnpoints)
    {
        if (TypesOfEnemiesPerRoundControllerRef.RoundControllerRef.EnemiesWaveCounter == 10)//Boss fight
        {
            Debug.Log("did i hit here");
            EnemiesAlive++;
            var enemyInstance = Instantiate(TypesOfEnemiesPerRoundControllerRef.TypesOfInGameEnemies[0], listofspawnpoints[0].transform.position, Quaternion.identity);
            TypesOfEnemiesPerRoundControllerRef.ListOfEnemies.Add(enemyInstance);
        }
        else
        {
            int _counter = 0;
            for (int i = 0; i < listofspawnpoints.Count; i++)
            {
                //spawn enemies here...
                EnemiesAlive++;
                if (TypesOfEnemiesPerRoundControllerRef.TypesOfInGameEnemies.Count == 1)
                {
                    var enemyInstance = Instantiate(TypesOfEnemiesPerRoundControllerRef.TypesOfInGameEnemies[0], listofspawnpoints[i].transform.position, Quaternion.identity);
                    TypesOfEnemiesPerRoundControllerRef.ListOfEnemies.Add(enemyInstance);
                }
                else if (TypesOfEnemiesPerRoundControllerRef.TypesOfInGameEnemies.Count == 2)
                {
                    if(_counter < _maxForOneCertainEnemy)
                    {
                        var enemyInstance1 = Instantiate(TypesOfEnemiesPerRoundControllerRef.TypesOfInGameEnemies[0], listofspawnpoints[i].transform.position, Quaternion.identity);
                        TypesOfEnemiesPerRoundControllerRef.ListOfEnemies.Add(enemyInstance1);
                        _counter++;
                    }
                    else
                    {
                        var enemyInstance2 = Instantiate(TypesOfEnemiesPerRoundControllerRef.TypesOfInGameEnemies[1], listofspawnpoints[i].transform.position, Quaternion.identity);
                        TypesOfEnemiesPerRoundControllerRef.ListOfEnemies.Add(enemyInstance2);//issue here!
                    }
                    
                }
            }
        }  
    }
    public void CheckOnTotalEnemies()
    {
        if (EnemiesAlive <= 0)
            IsAllEnemiesDead = true;
        else
            IsAllEnemiesDead = false;
    }
}
