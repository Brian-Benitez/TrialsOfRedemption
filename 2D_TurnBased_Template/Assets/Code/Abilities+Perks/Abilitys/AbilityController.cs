using UnityEngine;

public class AbilityController : MonoBehaviour
{
    public GameObject FirstBlock;
    public GameObject LastBlock;
    public RoundController RoundControllerRef;
    public enum UnlockTiers
    {
        None,
        FirstUnlock,
        LastUnlock,
    }
    public UnlockTiers CurrentUnlock = UnlockTiers.None;

    private void Start()
    {
        SetRestrictersOnAbilitys();
    }

    public void CheckingOnAchivementsPlayersWon()
    {
        if(RoundControllerRef.TotalAmountOfRoundsWon == 6)
            CurrentUnlock = UnlockTiers.FirstUnlock;
        if (RoundControllerRef.TotalAmountOfRoundsWon == 10)
            CurrentUnlock = UnlockTiers.LastUnlock;

        SetRestrictersOnAbilitys();
    }

    void SetRestrictersOnAbilitys()
    {
        if(CurrentUnlock == UnlockTiers.None)
        {
            FirstBlock.SetActive(true);
            LastBlock.SetActive(true);
        }
        if(CurrentUnlock == UnlockTiers.FirstUnlock)
        {
            FirstBlock.SetActive(false);
        }
        if(CurrentUnlock == UnlockTiers.LastUnlock)
        {
            LastBlock.SetActive(false);
        }
    }
}
