using UnityEngine;

public class MovementState : State
{
    [Header("States")]
    AttackState AttackState;
    public bool IsMoving = false;

    [Header("Floats")]
    public float AttackRange;
    public float AggroRange;
    public float StandByRange;
    public float DistanceFromPlayer;

    [Header("Scripts")]
    public EnemySwordsman EnemySwordsmanRef;
    public EnemyAggroDistance EnemyAggroDistanceRef;
    public StunState StunStateRef;
    EnemyWeaponRotation _enemyWeaponRotationRef;

    private void Start()
    {
        AttackState = GetComponentInChildren<AttackState>();
        _enemyWeaponRotationRef = GetComponentInChildren<EnemyWeaponRotation>();
    }

    private void Update()
    {
        if (_enemyWeaponRotationRef.IsAttacking || AttackState.WithinRange || StunStateRef.IsStunned)
            return;

        if (EnemyAggroDistanceRef.IsAggro)
        {
            MoveBasedOnPriority();
        }
        else if (!EnemyAggroDistanceRef.IsAggro)//do something when out of aggro
            return;
       
    }
   
    /// <summary>
    /// movement code
    /// </summary>
    void MoveBasedOnPriority()
    {
        if (EnemyAggroDistanceRef.IsFightingPlayer)
        {
            if (Vector2.Distance(transform.position, PlayerController.Instance.Player.position) > AggroRange)
            {
                AttackState.WithinRange = false;
                IsMoving = true;
                transform.position = Vector2.MoveTowards(transform.position, PlayerController.Instance.Player.position, EnemySwordsmanRef.EnemySpeed * Time.deltaTime);
            }
            if (Vector2.Distance(transform.position, PlayerController.Instance.Player.position) <= AggroRange)
            {
                EnemyAggroDistanceRef.IsFightingPlayer = true;//this is for when a aggro enemy is far away from player and one enemy is closer to fight player
                transform.position = Vector2.MoveTowards(transform.position, PlayerController.Instance.Player.position, EnemySwordsmanRef.EnemySpeed * Time.deltaTime);
            }
            if(Vector2.Distance(transform.position,PlayerController.Instance.Player.position) <= AttackRange)
            {
                AttackState.WithinRange = true;
            }
        }  
        else if(EnemyAggroDistanceRef.IsFightingPlayer == false)
        {
            if (Vector2.Distance(transform.position, PlayerController.Instance.Player.position) <= AttackRange || Vector2.Distance(transform.position, PlayerController.Instance.Player.position) <= AggroRange)
            {
                EnemyTurnController.Instance.TryAddingEnemyToList(this.gameObject);
            }
            if (Vector2.Distance(transform.position, PlayerController.Instance.Player.position) <= StandByRange)
            {
                transform.position = Vector2.MoveTowards(transform.position, PlayerController.Instance.Player.position, -EnemySwordsmanRef.EnemySpeed / 2 * Time.deltaTime);
                //get away from player unless your right next to them or circumstances changes
            }
        }
            
        DistanceFromPlayer = Vector2.Distance(transform.position, PlayerController.Instance.Player.position);
    }

    public override State RunCurrentState()
    {
        if (AttackState.WithinRange)
        {
            IsMoving = false;
            return AttackState;
        }
            

        return this;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
        Gizmos.DrawWireSphere(transform.position, AggroRange);
        Gizmos.DrawWireSphere(transform.position, StandByRange);
    }

}
