using System.Collections;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public static ShieldController Instance;//For every singlton we have, make sure everything works then start making things private that we dont need

    [Header("Shield Object")]
    public GameObject ShieldObject;
    public KeyCode ParryKeyCode;
    public bool IsParrying = false;
    private bool CanParry = false;
    public float ParryDuration;
    public float ParryCooldown;
    private float _maxParryCooldown;

    [Header("Shield Info")]
    public float ShieldHealth;
    public float MaxShieldHealth;//Use this to upgrade

    public bool IsShieldActive = false;

    [Header("Cooldown")]
    public bool IsShieldBroken = false;
    private float ShieldCoolDownTimer = 0;
    public float ShieldBreakDuration;

    [Header("Shield key")]
    public KeyCode ShieldKey;
    public PlayerAnimationController PlayerAnimationControllerRef;
    private PlayerMovement _playerMovement;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        _playerMovement = GetComponentInParent<PlayerMovement>();
        _maxParryCooldown = ParryCooldown;
    }

    void Update()
    {
        if(_playerMovement.IsDashing)
            return;

        if (!CanParry && ParryCooldown > 0)
            ParryCooldown -= Time.deltaTime;
        else
            CanParry = true;

        if (CanParry && Input.GetKeyDown(ParryKeyCode))
        {
            _playerMovement.SlowPlayer();
            ShieldObject.SetActive(false);
            StartCorutineActivateParry();
            ParryCooldown = _maxParryCooldown;
            CanParry = false;
        }
     
        if (Input.GetKey(ShieldKey) && !IsShieldBroken && !IsParrying)
        {
            _playerMovement.SlowPlayer();
            ShieldObject.SetActive(true);
            TurnOnShieldObject();
            ChangePlayerLayerName();
        }
        else
        {
            _playerMovement.UnSlowPlayer();
            ShieldObject.SetActive(false);
            TurnOffIsShieldActive();
        }

        if(ShieldHealth <= 0)
            IsShieldBroken = true;

        if(IsShieldBroken)
        {
            ShieldCoolDownTimer += Time.deltaTime;

            if(ShieldCoolDownTimer >= ShieldBreakDuration)
            {
                IsShieldBroken = false;
                ShieldHealth = MaxShieldHealth;
            }
        }
            
    }
    public void UpgradeShield(float increment)
    {
        MaxShieldHealth += increment;
        ShieldHealth = MaxShieldHealth;
    }

    public void StartCorutineActivateParry() => StartCoroutine(ActivateParry());
    IEnumerator ActivateParry()
    {
        PlayerAnimationControllerRef.IsParrying();
        yield return new WaitForSecondsRealtime(0.08f);
        IsParrying = true;
        ChangePlayerLayerToParry();
        yield return new WaitForSecondsRealtime(ParryDuration);
        ChangeBackPlayerLayerName();
        PlayerAnimationControllerRef.IsNotParrying();
        IsParrying = false;
    }
    void ChangePlayerLayerToParry() => PlayerController.Instance.Player.gameObject.tag = "Parry";
    void ChangePlayerLayerName() => PlayerController.Instance.Player.gameObject.tag = "Shield";
    void ChangeBackPlayerLayerName() => PlayerController.Instance.Player.gameObject.tag = "Player";
    void TurnOnShieldObject() => IsShieldActive = true;
    void TurnOffIsShieldActive() => IsShieldActive = false;
}
