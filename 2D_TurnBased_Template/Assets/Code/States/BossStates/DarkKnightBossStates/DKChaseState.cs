using UnityEngine;

public class DKChaseState : State
{
    private float MinimumDistanceFromPlayer = 5.5f;
    private float DistanceFromPlayer;
    private float PreferedRangeAttkDistance = 7f;

    public bool CanStartRangeAttack = false;
    public bool IsMoving = false;

    [Header("Scripts")]
    public DKRangeAttack rangeAttack;
    public AttackState AttackState;
    public BaseEnemy BaseEnemyRef;
    public BossStagesController BossStagesControllerRef;


    private void Update()
    {
        if (rangeAttack.CanRangeAttack && BossStagesControllerRef.IsFinalStage || AttackState.WithinRange || rangeAttack.IsAttackingNow)
            return;
    }

    public override State RunCurrentState()
    {
        if (AttackState.WithinRange)
        return AttackState;

        if (CanStartRangeAttack)
            return rangeAttack;

        if (DistanceFromPlayer >= PreferedRangeAttkDistance && rangeAttack.CanRangeAttack)
            CanStartRangeAttack = true;

        if (Vector2.Distance(transform.position, PlayerController.Instance.Player.position) > MinimumDistanceFromPlayer)
        {
            IsMoving = true;
            AttackState.WithinRange = false;// not yet in range
            transform.position = Vector2.MoveTowards(transform.position, PlayerController.Instance.Player.position, BaseEnemyRef.EnemySpeed * Time.deltaTime);
        }
        else
        {
            IsMoving = false;
            DistanceFromPlayer = Vector2.Distance(transform.position, PlayerController.Instance.Player.position);
            AttackState.WithinRange = true;
        }

        return this;
    }
}
