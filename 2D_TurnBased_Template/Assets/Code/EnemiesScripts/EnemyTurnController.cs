using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnController : MonoBehaviour
{
    public static EnemyTurnController Instance;
    public List<GameObject> EnemiesFightingPlayer;
    public int MaxAmountOfDirectEnemyThreat;
    public bool IsThereAnOpenSlot = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }


    private void Update()
    {
        CheckOnAmountOfEnemyThreats();
    }

    public bool IsEnemyInList(GameObject enemy)
    {
        if(enemy.GetComponentInChildren<EnemyAggroDistance>().IsFightingPlayer)
            return true;
        else
            return false;
    }
    public void TryAddingEnemyToList(GameObject enemy)
    {
        if (EnemiesFightingPlayer.Count == MaxAmountOfDirectEnemyThreat)
            Debug.Log("cannot add more enemies");
        else if(enemy.GetComponentInChildren<EnemyAggroDistance>().IsFightingPlayer == false && EnemiesFightingPlayer.Count < MaxAmountOfDirectEnemyThreat)
        {
            EnemiesFightingPlayer.Add(enemy);
            enemy.GetComponentInChildren<EnemyAggroDistance>().IsFightingPlayer = true;
        }
    }

    public void RemoveEnemyFromList(GameObject enemy)
    {
        EnemiesFightingPlayer.Remove(enemy);
        enemy.GetComponentInChildren<EnemyAggroDistance>().IsFightingPlayer = false;
    }
    /// <summary>
    /// Checks to see if there any slots left to fight the player.
    /// </summary>
    public void CheckOnAmountOfEnemyThreats()
    {
        if (EnemiesFightingPlayer.Count == MaxAmountOfDirectEnemyThreat)
        {
            IsThereAnOpenSlot = false;
            Debug.Log("no more room");
        }
        else
        {
            Debug.Log("there room");
            IsThereAnOpenSlot = true;
        }
            
    }
}
