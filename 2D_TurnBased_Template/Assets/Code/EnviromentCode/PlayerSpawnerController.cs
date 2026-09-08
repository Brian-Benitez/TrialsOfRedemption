using UnityEngine;

public class PlayerSpawnerController : MonoBehaviour
{
    public static PlayerSpawnerController Instance;
    public bool SpawnInArena = false;
    public KeyCode InteractKeyCode;
    private bool CanInteract = false;
    public GameObject InteractGO;
    public GameObject StartSpawner;
    public GameObject CampfireSpawner;
    [Header("Scripts")]
    public StartingGameController StartingGameControllerRef;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        if(SpawnInArena)
        {
            SpawnPlayerInArena();
        }
        else
            SpawnPlayerInCampfire();
    }

    private void Update()
    {
        if (Input.GetKeyDown(InteractKeyCode) && CanInteract)
        {
            SpawnPlayerInArena();
            StartingGameControllerRef.StartNewRound();
        }
            
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            CanInteract = true;
            InteractGO.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            InteractGO.SetActive(true);
            CanInteract = false;
        }
       
    }
    public void SpawnPlayerInArena()
    {
        PlayerController.Instance.Player.position = StartSpawner.transform.position;
    }

    public void SpawnPlayerInCampfire() => PlayerController.Instance.Player.position = CampfireSpawner.transform.position;
}
