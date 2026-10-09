
using UnityEngine;

public class InteractingWithUpgradeSystem : MonoBehaviour
{
    public GameObject UpgradeUIGameObject;
    public GameObject SoulsUI;
    public GameObject BossSoulsUI;
    public GameObject EKeyPNG;
    public KeyCode InteracteWithUpgradeKey;
    public KeyCode DismissUIKey;

    public bool CanInteract = false;
    public bool IsInteracting = false;
    public bool CheckpointReached = false;
    public PlayerMovement PlayerMovementRef;

    private void Start()
    {
        UpgradeUIGameObject.SetActive(false);
    }

    private void Update()
    {
        if (CanInteract)
        {
            if (Input.GetKeyDown(InteracteWithUpgradeKey))
            {
                UpgradeUIGameObject.SetActive(true);
                XPController.Instance.IsUpgrading = true;
                IsInteracting = true;
                CheckpointReached = true;
            }
            if (Input.GetKeyUp(DismissUIKey))
            {
                UpgradeUIGameObject.SetActive(false);
                BossSoulsUI.SetActive(false);
                IsInteracting = false;
                PlayerMovementRef.TurnOffStopPlayerMovement();
                XPController.Instance.IsUpgrading = false;
            }
        }
        //makes player not move when in menu
        if(IsInteracting)
            PlayerMovementRef.TurnOnStopPlayerMovement();
    }

    public void TurnOnBossSoulsUI()
    {
        BossSoulsUI.SetActive(true);
        SoulsUI.SetActive(false);
    }

    public void TurnOnSoulsUI()
    {
        SoulsUI.SetActive(true);
        BossSoulsUI.SetActive(false);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        CanInteract = true;
        EKeyPNG.SetActive(true);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        CanInteract = false;
        EKeyPNG.SetActive(false);
    }
}
