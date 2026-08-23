
using System.Collections;
using UnityEngine;

public class AttackState : State//rename this to EnemyAttackState
{
    [Header("Test demo")]
    public bool IsPlayingAttackAni = false;

    [Header("Melee pos")]
    public Transform MeleePos;

    [Header("Attacks Settings")]
    public float AttackRange;
    public float WindUpTimeForMelee;
    public int AmountOfAttacks;
    public int MaxAmountOfAttacks;
    public float AttackCooldownTimer;
    public float MaxTimerOfCooldown;

    [Header("Bools Conditions to attack")]
    public bool IsWaiting = false;//do not DELETE
    public bool WithinRange = false;
    public bool IsDoneCoolingDown = false;

    [Header("Level 2 Enemies settings")]
    public bool HasRolled = false;

    [Header("Layermasks for what can be hit")]
    public LayerMask WhatisHittable;

    public bool AttackMissedPlayer = false;
    private IEnumerator _windupCoroutine;
    private EnemySwordsman EnemySwordsmanRef;
    //States ref here
    MovementState ChaseState;
    public StunState StunState;
    public DKChaseState DKChaseState;
    public BossVunerableState BossVunerableStateRef;
    BlockAndMoveState BlockAndMoveState;
    EnemyWeaponRotation _enemyWeaponRotationRef;

    private void Start()
    {
        EnemySwordsmanRef = gameObject.GetComponentInParent<EnemySwordsman>();
        ChaseState = GetComponentInParent<MovementState>();
        _enemyWeaponRotationRef = GetComponentInParent<EnemyWeaponRotation>();
        
        if (EnemySwordsmanRef.EnemyDifficulty == BaseEnemy.LevelOfEnemy.LevelTwo)
        {
            BlockAndMoveState = GetComponentInParent<BlockAndMoveState>();
        }
        _windupCoroutine = WindUpAttack();
    }

    void Update()
    {
        if (AttackCooldownTimer >= MaxTimerOfCooldown)
        {
            AttackCooldownTimer = MaxTimerOfCooldown;
        }
        else
            AttackCooldownTimer += Time.deltaTime;
    }

    void MeleeAttack()
    {
        Collider2D[] enemiesToDamges = Physics2D.OverlapCircleAll(MeleePos.position, AttackRange, WhatisHittable);

        if (enemiesToDamges.Length == 0)
        {
            Debug.Log("i hit no one:(");
            AttackMissedPlayer = true;
        }
        else
            AttackMissedPlayer = false;

        for (int i = 0; i < enemiesToDamges.Length; i++)
        {
            if (enemiesToDamges[i].CompareTag("Shield"))
            {
                Debug.Log("Hit shield!");
                ShieldController.Instance.ShieldHealth -= EnemySwordsmanRef.EnemyDamage;
            }

            if (enemiesToDamges[i].CompareTag("Player"))
            {
                enemiesToDamges[i].GetComponent<BaseCharacter>().TakeDamage(EnemySwordsmanRef.EnemyDamage);
                Debug.Log("Enemy hit " + enemiesToDamges[i].gameObject.name + "for " + EnemySwordsmanRef.EnemyDamage);
            }
        }
    }
    public IEnumerator WindUpAttack()
    {
        IsWaiting = true;
        IsPlayingAttackAni = true;
        Debug.Log("Winding up attack " + WindUpTimeForMelee + " Seconds");
        yield return new WaitForSecondsRealtime(WindUpTimeForMelee);
        Debug.Log("Winding up attack done");
        MeleeAttack();
        _enemyWeaponRotationRef.IsAttacking = false;
        IsPlayingAttackAni = false;
        IsWaiting = false;
        AmountOfAttacks++;
    }

    public override State RunCurrentState()
    {
        if(StunState.IsStunned)
        {
            StopCoroutine(_windupCoroutine);
        }

        if (IsDoneCoolingDown)
        {
            if (!IsWaiting && WithinRange && AmountOfAttacks < MaxAmountOfAttacks)
            {
                _enemyWeaponRotationRef.IsAttacking = true;
                StartCoroutine(WindUpAttack());
            }
        }

        if (AmountOfAttacks >= MaxAmountOfAttacks)
        {
            IsDoneCoolingDown = false;
            AmountOfAttacks = 0;
            AttackCooldownTimer = 0;
            if (EnemySwordsmanRef.EnemyDifficulty == BaseEnemy.LevelOfEnemy.LevelTwo && !HasRolled)
            {
                HasRolled = true;
                BlockAndMoveState.RollingToBlock();
            }
            if(EnemySwordsmanRef.EnemyDifficulty == BaseEnemy.LevelOfEnemy.Boss)
            {
                return BossVunerableStateRef;
            }
        }

        if (AttackCooldownTimer >= MaxTimerOfCooldown)
        {
            IsDoneCoolingDown = true;
            if (EnemySwordsmanRef.EnemyDifficulty == BaseEnemy.LevelOfEnemy.LevelTwo && HasRolled)
                HasRolled = false;
        }

        if (EnemySwordsmanRef.EnemyDifficulty == BaseEnemy.LevelOfEnemy.LevelTwo && BlockAndMoveState.CanBlock)
        {
            //block
            return BlockAndMoveState;
        }
        if (EnemySwordsmanRef.EnemyDifficulty == BaseEnemy.LevelOfEnemy.LevelThree)
        {
            //do something
        }
        if (AttackMissedPlayer == true)
        {
            RestartEnemy();
            if (EnemySwordsmanRef.EnemyDifficulty == BaseEnemy.LevelOfEnemy.Boss)
            {
                Debug.Log("we go here");
                return DKChaseState;
            }
  
            else
                return ChaseState;
        }

        return this;
    }

    public void RestartEnemy()
    {
        AttackMissedPlayer = false;
        WithinRange = false;
        _enemyWeaponRotationRef.IsAttacking = false;
    }

    //----------------------------------------------------Debug--stuff--------------------------------------------------------------------->
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(MeleePos.position, AttackRange);
    }
}
