using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject StartMenuPrefab;
    public GameObject CreditsMenuPrefab;
    public bool IsInMenuScreen = false;
    public bool IsInCreditsMenu = false;
    public PlayerMovement PlayerMovementRef;
    public PlayerMeleeAttack PlayerMeleeAttackRef;

    private void Start()
    {
        StartMenuPrefab.SetActive(true);
        IsInMenuScreen = true;
    }

    private void Update()
    {
        if(IsInMenuScreen)
        {
            PlayerMeleeAttackRef.IsAttackingPaused = true;
            PlayerMovementRef.TurnOnStopPlayerMovement();
            if(IsInCreditsMenu && Input.GetKeyDown(KeyCode.Q))
                CreditsMenuPrefab.SetActive(false);
        }

    }
    public void IsNotInMenuScreen() => IsInMenuScreen = false;
    public void OpenMainMenuScreen()
    {
        //fade to black
        StartMenuPrefab.SetActive(true);
    }
    public void StartingGame()
    {
        //add fade to black...
        StartMenuPrefab.SetActive(false);
        CreditsMenuPrefab.SetActive(false);
        //place player in tutorial area...
    }

    public void OptionMenuOpen()
    {
        IsInCreditsMenu = true;
        //add small pop up on menu...
        CreditsMenuPrefab.SetActive(true);
        //OptionsMenuPrefab.SetActive(true);
    }
    public void QuitGame() => Application.Quit();
}
