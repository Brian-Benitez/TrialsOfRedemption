using System.Collections.Generic;
using UnityEngine;
public class PerksController : MonoBehaviour
{
    public static PerksController Instance;
    public int MaxAmountOfPerks;
    public List<UpgradePerk> ListOfActivePerks;
    public List<UpgradePerk> AllPerks;
    public int ListIndex;
    [Header("UIs")]
    public GameObject LevelUpUIGO;
    public PerkCardController PerkCardControllerRef;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }


    public void AddPerkToList(UpgradePerk GO)
    {
        bool _isPerkInList = false;

        for (int i = 0; i < ListOfActivePerks.Count; i++)//use if(listofactiveperks.contains(GO)
        {
            if (ListOfActivePerks[i].name == GO.name)
            {
                Debug.Log("Perk is already in a slot!");
                _isPerkInList = true;
            }
        }

        if (!_isPerkInList)
        {
            if (ListOfActivePerks.Count >= MaxAmountOfPerks)
            {
                ListOfActivePerks[ListIndex].DisablePerk();
                ListOfActivePerks[ListIndex].IsPerkActive = false;
                ListOfActivePerks.RemoveAt(ListIndex);
                Debug.Log("remove perk that was previously there");
            }
            ListOfActivePerks.Insert(ListIndex, GO);
            ListOfActivePerks[ListIndex].IsPerkActive = true;
            Debug.Log(GO.name + " Is enabled!");
        }
    }

    public void RestartAllPlayersPerks()
    {
        if (ListOfActivePerks.Count > 0)
        {
            PerkCardControllerRef.RemoveAllVisualPerkCardsFromList();//Removes all UI versions of the perks
            for (int i = 0; i < AllPerks.Count; i++)//disables all perks
            {
                Debug.Log("turn off " + AllPerks[i].gameObject.name);
                AllPerks[i].DisablePerk();
            }
            
            for (int j = 0; j < ListOfActivePerks.Count; j++)
            {
                ListOfActivePerks.Remove(ListOfActivePerks[j]);

            }
            
        }

    }
    public void EnablePerksUI()
    {
        LevelUpUIGO.SetActive(false);
    }
    public void DisablePerksUI()
    {
        LevelUpUIGO.SetActive(true); 
    }
   
}
