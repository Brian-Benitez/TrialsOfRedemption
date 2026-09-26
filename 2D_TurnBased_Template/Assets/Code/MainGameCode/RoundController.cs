using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class RoundController : MonoBehaviour
{
    [Header("Round Info")]
    public int EnemiesWaveCounter;
    public int TotalAmountOfRoundsWon;
    public int MaxAmountOfRounds;
    public TextMeshProUGUI RoundsText;

    [Header("Starting round info")]
    public bool IsRoundStarted = false;

    [Header("Upgrade GameObject")]
    public GameObject UpgradePrefab;
    [Header("StartButton GameObject")]
    public GameObject StartButtonPrefab;

    [Header("Round Start Events")]//put all this in separate class..
    public bool IsStartedEvent = false;
    public UnityEvent StartRoundEvent;

    public bool IsRoundEnd = false;
    public UnityEvent StartEndRoundEvent;

    private void Update()
    {
        if (IsRoundStarted)
        {
            if (!IsStartedEvent)
            {
                EnemiesSpawner.Instance.IsAllEnemiesDead = false;
                UpgradePrefab.SetActive(false);
                StartButtonPrefab.SetActive(false);
                IsStartedEvent = true;
                StartRoundEvent.Invoke();
            }
        }
        if (EnemiesSpawner.Instance.IsAllEnemiesDead)
        { 
            if(!IsRoundEnd)
            {
                IsRoundStarted = false;
                UpgradePrefab.SetActive(true);
                StartButtonPrefab.SetActive(true);
                IsRoundEnd = true;
                StartEndRoundEvent.Invoke();

            }
        }
    }

    public void IncreaseRoundCounter()
    {
        if (EnemiesWaveCounter == MaxAmountOfRounds)
        {
            RestartRoundCounter();
            EnemiesWaveCounter += Mathf.Clamp(1, 0, MaxAmountOfRounds);
        }
        else
            EnemiesWaveCounter += Mathf.Clamp(1, 0, MaxAmountOfRounds);
        TotalAmountOfRoundsWon++;
        RoundsText.text = "" + TotalAmountOfRoundsWon;
        Debug.Log("what round are we on " + EnemiesWaveCounter);
    }
    /// <summary>
    /// Only when boss is defeated
    /// </summary>
    public void RestartRoundCounter() => EnemiesWaveCounter = 0;
}
