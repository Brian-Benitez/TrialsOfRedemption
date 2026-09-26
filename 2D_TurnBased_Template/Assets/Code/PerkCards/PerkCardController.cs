using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class PerkCardController : MonoBehaviour
{
    public bool IsChoosenPerksEnabled = false;
    public RectTransform BackroundCanvas;
    public RectTransform ChoosenPerkScreen;
    public List<RectTransform> PlayersVisualActivePerks;
    public List<RectTransform> PerkCardsChoices;
    public List<RectTransform> AllPerkCards;

    public List<Vector2> AnchorsForCards;
    public Vector2 PickedCardPos;
    public Vector2 PlayerCardsPilePos;
    public Vector2 WaitForRemovingPos;
    public Vector2 RestartCardsPos;
    public Vector2 RestartBackroundPos;
    public Vector2 RestartChoosenPerkScreen;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(IsChoosenPerksEnabled)
                RestartChoosenPerks();
            else
                SeeChoosenPerkCards();
        }
    }

    public void CheckIfTheresRoomForAPerk()
    {
        if (PlayersVisualActivePerks.Count >= PerksController.Instance.MaxAmountOfPerks)
        {
            Debug.Log("player is picking a perk card with maxxed out slots, making them pick...");
            ReadyCardsForOneDiscard();
        }
        else
        {
            StartPickedACardCoroutine();
            Debug.Log("did i play");
        }
    }

    public void MoveBackroundOnScreen() => BackroundCanvas.DOAnchorPos(PickedCardPos, 1f);
    public void RestartBackroundOnScreen() => BackroundCanvas.DOAnchorPos(RestartBackroundPos, .5f);
    public void StartPlaceCardsOnScreenCoroutine() => StartCoroutine(PlaceCardsOnScreen());
    private IEnumerator PlaceCardsOnScreen()
    {
        TurnOffButtons(PerkCardsChoices);
        yield return new WaitForSecondsRealtime(1f);
        PerkCardsChoices[0].DOAnchorPos(AnchorsForCards[0], .5f);
        yield return new WaitForSecondsRealtime(0.4f);
        PerkCardsChoices[1].DOAnchorPos(AnchorsForCards[1], .5f);
        yield return new WaitForSecondsRealtime(0.4f);
        PerkCardsChoices[2].DOAnchorPos(AnchorsForCards[2], .5f);
        yield return new WaitForSecondsRealtime(1f);
        TurnOnButtons(PerkCardsChoices);
    }

    public void StartPickedACardCoroutine() => StartCoroutine(PickedACard());
    private IEnumerator PickedACard()
    {
        TurnOffButtons(PerkCardsChoices);
        for (int i = 0; i < PerkCardsChoices.Count; i++)
        {
            if (PerkCardsChoices[i].GetComponent<PerkCardBehaviour>().IsPickedOnChoice)
            {
                PerkCardsChoices[i].DOAnchorPos(PickedCardPos, 1f);
                PlayersVisualActivePerks.Add(PerkCardsChoices[i]);
                AllPerkCards.Remove(PerkCardsChoices[i]);//remove from list so it doesnt get picked again
                PerkCardsChoices[i].GetComponent<UpgradePerk>().IsPerkPicked = true;
            }
            else
            {
                PerkCardsChoices[i].DOAnchorPos(RestartCardsPos, .5f);
            }
        }

        yield return new WaitForSecondsRealtime(3f);

        for (int i = 0; i < PerkCardsChoices.Count; i++)
        {
            if (PerkCardsChoices[i].GetComponent<PerkCardBehaviour>().IsPickedOnChoice)
            {
                PerkCardsChoices[i].DOAnchorPos(PlayerCardsPilePos, .5f);
            }
        }
        RestartBackroundOnScreen();
        PerkCardsChoices.Clear();
        XPController.Instance.IsUpgrading = false;
    }

    public void StartDiscardACardCorutine() => StartCoroutine(DiscardACard());
    private IEnumerator DiscardACard()
    {
        TurnOffButtons(PerkCardsChoices);
        for (int i = 0; i < PerkCardsChoices.Count; i++)//adding choosen card
        {
            if (PerkCardsChoices[i].GetComponent<PerkCardBehaviour>().IsPickedOnChoice)
            {
                PlayersVisualActivePerks.Add(PerkCardsChoices[i]);
                PerkCardsChoices[i].GetComponent<UpgradePerk>().IsPerkPicked = true;
            }
        }

        for (int i = 0; i < PlayersVisualActivePerks.Count; i++)//animation of moving cards
        {
            if (PlayersVisualActivePerks[i].GetComponent<PerkCardBehaviour>().IsBeingRemoved == true)
            {
                PlayersVisualActivePerks[i].DOAnchorPos(RestartCardsPos, .5f);
            }
            else
            {
                PlayersVisualActivePerks[i].DOAnchorPos(PlayerCardsPilePos, .5f);
            }
        }

        for (int i = 0; i < PlayersVisualActivePerks.Count; i++)//remove perk from list
        {
            if (PlayersVisualActivePerks[i].GetComponent<PerkCardBehaviour>().IsBeingRemoved == true)
            {
                PlayersVisualActivePerks.Remove(PlayersVisualActivePerks[i]);
                AllPerkCards.Add(PlayersVisualActivePerks[i]);//add removed perk back in all perk list
            }
        }

        yield return new WaitForSecondsRealtime(2f);

        RestartBackroundOnScreen();
    }
    public void RandomlyPickingChoiceCards()
    {
        while (PerkCardsChoices.Count < PerksController.Instance.MaxAmountOfPerks)
        {
            int index = Random.Range(0, AllPerkCards.Count);

            if (PerkCardsChoices.Contains(AllPerkCards[index]))
            {
                Debug.Log("added this card already...restarting");
            }
            else
            {
                PerkCardsChoices.Add(AllPerkCards[index]);
            }
        }
    }

   

    private void ReadyCardsForOneDiscard()
    {
        for (int i = 0; i < PerkCardsChoices.Count; i++)//removing choice cards 
        {
            if (PerkCardsChoices[i].GetComponent<PerkCardBehaviour>().IsPickedOnChoice)
            {
                PerkCardsChoices[i].DOAnchorPos(WaitForRemovingPos, 1f);
            }
            else
            {
                PerkCardsChoices[i].DOAnchorPos(RestartCardsPos, .5f);
            }
        }

        for (int J = 0; J < PlayersVisualActivePerks.Count; J++)//putting active perks on screen and making them have the disable button on
        {
            PlayersVisualActivePerks[J].GetComponent<PerkCardBehaviour>().TurnOffEnableButton();//gotta turn it on again here
            PlayersVisualActivePerks[J].DOAnchorPos(AnchorsForCards[J], .5f);
        }
    }

    public void SeeChoosenPerkCards()
    {
        IsChoosenPerksEnabled = true;
        ChoosenPerkScreen.DOAnchorPos(PickedCardPos, 0.5f);

        if(PlayersVisualActivePerks.Count != 0)
        {
            TurnOffButtons(PlayersVisualActivePerks);
            for (int i = 0; i < PlayersVisualActivePerks.Count; i++)
            {
                PlayersVisualActivePerks[i].DOAnchorPos(AnchorsForCards[i], 0.5f);
            }
        }
    }

    public void RestartChoosenPerks()
    {
        IsChoosenPerksEnabled = false;
        ChoosenPerkScreen.DOAnchorPos(RestartChoosenPerkScreen, 0.5f);

        if (PlayersVisualActivePerks.Count != 0)
        {
            for (int i = 0; i < PlayersVisualActivePerks.Count; i++)
            {
                PlayersVisualActivePerks[i].DOAnchorPos(PlayerCardsPilePos, 0.5f);
            }
        }
    }

    private void TurnOffButtons(List<RectTransform> aList)
    {
        for (int i = 0; i < aList.Count; i++)
        {
            aList[i].GetComponent<PerkCardBehaviour>().TurnOffAllButtons();
        }
    }

    private void TurnOnButtons(List<RectTransform> aList)
    {
        for (int i = 0; i < aList.Count; i++)
        {
            aList[i].GetComponent<PerkCardBehaviour>().TurnOnEnableButton();
        }
    }


    public void RemoveAllPerkCardsFromList() => PerkCardsChoices.Clear();
}
