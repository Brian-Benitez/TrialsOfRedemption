using System.Collections;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    [Header("Booleans")]
    public bool IsAttacking = false;
    public bool CanMeleeAttackAgain = false;
    public bool CanSpeicalAgain = false;
    public bool ChangedValues = false;

    [Header("Type Of Attack")]
    public bool IsLightAttack = false;
    public bool IsHeavyAttack = false;
    public bool IsSpecialAttack = false;

    [Header("Amount Of Attacks")]
    public int MaxAmountOfAttacks;
    public int AmountOfAttacks;
    private float TimerToRestartAttacks;
    public float MaxTimerToRestartAttack;


    [Header("Spiecal stats")]
    public KeyCode SpecialKey;
    public Transform SpeicalPos;
    public float SpeicalRange;
    public float _maxwaitTimeForSpeical = 0.8f;

    [Header("Transforms")]
    public Transform AttackPos;
    public Transform DirectionalLooks;

    [Header("----------Stats----------")]
    [Header("Floats")]
    public float AttackRange;

    [Header("Windup Stats")]
    public float AttackSpeed;
    public float WindUpDuration;

    [Header("Heavy Windup Stats")]
    private float HeavyWindUpSpeed;
    public float MaxHeavyWindUpSpeed;

    [Header("Player attk damg")]
    public float PlayerLightAttkDamg;
    public float PlayerHeavyAttkDamg;
    public float PlayerSpecialDamg;

    [Header("LayerMasks")]
    public LayerMask WhatIsEnemies;

    [Header("Scripts")]
    public ActivateSlash ActivateSlashRef;
    public PlayerAnimationController PlayerAnimationControllerRef;
    private PlayerMovement _playerMovement;
    public FlipSprite FlipSpriteRef;

    private float _maxTimeBtwAttacks;
    private float _holdTime = 0f;
    private float _maxHoldTimeForHeavyAttk = 0.2f;
    private float _specialCooldown = 0f;

    private void Start()
    {
        _playerMovement = GetComponentInParent<PlayerMovement>();
        _maxTimeBtwAttacks = WindUpDuration;
        RestartTimerForAttacks();
    }

    private void Update()
    {
        DirectionalLooks.transform.position = AttackPos.position;//idk why this gets paused when i stop moving player

        if(AmountOfAttacks >= MaxAmountOfAttacks)
        {
            if(TimerToRestartAttacks >= MaxTimerToRestartAttack)
            {
                TimerToRestartAttacks = 0;
                CanMeleeAttackAgain = true;
                AmountOfAttacks = 0;
            }
            TimerToRestartAttacks += Time.deltaTime;
            CanMeleeAttackAgain = false;
        }

        if (Input.GetMouseButtonDown(0) && CanMeleeAttackAgain)
        {
            StartCoroutine(WindUpAttack(PlayerLightAttkDamg, AttackPos, AttackRange, WhatIsEnemies));
            //ActivateSlashRef.DeactivateSlashingArt();
            _playerMovement.UnSlowPlayer();
            AmountOfAttacks++;
        }

        _specialCooldown += Time.deltaTime;

        if(_specialCooldown >= _maxwaitTimeForSpeical)
        {
            IsSpecialAttack = true;
        }

        if(Input.GetKeyDown(SpecialKey) && IsSpecialAttack)
        {
            FlipSpriteRef.PlayerLookAtMouse();
            Hit(PlayerSpecialDamg, SpeicalPos, SpeicalRange, WhatIsEnemies);
            _specialCooldown = 0;
        }

        if (Input.GetMouseButton(0))
        {
            _holdTime += Time.deltaTime;
            
            if (_holdTime >= _maxHoldTimeForHeavyAttk)
            {
                _playerMovement.SlowPlayer();
                CanMeleeAttackAgain = false;

                if(HeavyWindUpSpeed >= MaxHeavyWindUpSpeed)
                {
                    IsHeavyAttack = true;
                    CanMeleeAttackAgain = true;
                }
                else
                {
                    HeavyWindUpSpeed += Time.deltaTime;//double check all this
                }
            }
        }
        if (Input.GetMouseButtonUp(0) && IsHeavyAttack && CanMeleeAttackAgain)
        {
            FlipSpriteRef.PlayerLookAtMouse();
            Hit(PlayerHeavyAttkDamg, AttackPos, AttackRange, WhatIsEnemies);
            _playerMovement.UnSlowPlayer();
        }

        if (WindUpDuration <= 0f)
        {
            CanMeleeAttackAgain = true;
            IsAttacking = false;
            return;
        }
        else
        {
            WindUpDuration -= Time.deltaTime;
            CanMeleeAttackAgain = false;
        }
    }


    public IEnumerator WindUpAttack(float dam, Transform pos, float range, LayerMask enemy)
    {
        PlayerAnimationControllerRef.IsAttacking();
        yield return new WaitForSecondsRealtime(AttackSpeed);
        Hit(dam, pos, range, enemy);
        PlayerAnimationControllerRef.IsNotAttacking();
    }
    void Hit(float dam, Transform pos, float range, LayerMask enemy)
    {
        IsAttacking = true;
        Collider2D[] enemiesToDamges = Physics2D.OverlapCircleAll(pos.position, range, enemy);

        ActivateSlashRef.ActivateSlashingArt();

        for (int i = 0; i < enemiesToDamges.Length; i++)
        {
            if (enemiesToDamges[i].CompareTag("EnemyShield"))
            {
                enemiesToDamges[i].GetComponentInChildren<EnemyShield>().ShieldTakeDamage(dam);
            }
            else if (enemiesToDamges[i].GetComponent<BaseEnemy>() != null)
            {
                enemiesToDamges[i].GetComponent<BaseEnemy>().TakeDamage(dam);
                DamagePopUp.Create(enemiesToDamges[i].transform.position, dam);
                Debug.Log("hit enemieas");
            }
        }
        RestartTimerForAttacks();
        RestartMeleeBools();
    }
    void RestartTimerForAttacks() => WindUpDuration = _maxTimeBtwAttacks;

    void RestartMeleeBools()
    {
        IsHeavyAttack = false;
        IsSpecialAttack = false;
        IsLightAttack = false;
        ChangedValues = false;
        _holdTime = 0f;
        HeavyWindUpSpeed = 0f;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackPos.position, AttackRange);
        Gizmos.DrawWireSphere(SpeicalPos.position, SpeicalRange);
    }
}
