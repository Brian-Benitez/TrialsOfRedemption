using System.Collections;
using UnityEngine;

public class DKRangeAttack : State
{
    [SerializeField]
    public GameObject RangeAttackPos;
    [SerializeField]
    public GameObject Player;
    public int DKRangeDamage;

    [Header("Enemys attack range and T.T.A")]
    public float RangeAttackRange;
    public float TimeBtwAttack;
    private float _maxTimeBtwAttacks;
    public float WindUpTimeForRange;
    private int AttackCount;
    public int MaxAttackCount;

    [Header("Bools Conditions to attack")]
    public bool CanRangeAttack = false;
    public bool IsAttackingNow = false;
    public bool IsDoneFullAttack = true;

    [Header("Layermasks for what can be hit")]
    public LayerMask WhatisHittable;

   
    DKChaseState dKChaseState;
    public BossStagesController _bossStagesControllerRef;
    private void Start()
    {
        _maxTimeBtwAttacks = TimeBtwAttack;
        dKChaseState = GetComponentInParent<DKChaseState>();
    }

    void Update()
    {
        if (CanRangeAttack && _bossStagesControllerRef.IsFinalStage && !IsAttackingNow && AttackCount < MaxAttackCount)
        {
            StartCoroutine(WindUpRangeAttack());
        }
        if(AttackCount >= MaxAttackCount)
        {
            AttackCount = 0;
            IsDoneFullAttack = true;
        }
        if (TimeBtwAttack <= 0 && !IsAttackingNow)
        {
            CanRangeAttack = true;
            IsDoneFullAttack = false;
            return;
        }
        else if(IsDoneFullAttack)
        {
            TimeBtwAttack -= Time.deltaTime;
            CanRangeAttack = false;
        }
    }

    void RangeAttack()
    {
        Collider2D[] enemiesToDamges = Physics2D.OverlapCircleAll(RangeAttackPos.transform.position, RangeAttackRange, WhatisHittable);
        
        if (enemiesToDamges.Length == 0)
        {
            Debug.Log("i hit no one:(");
        }
        
        for (int i = 0; i < enemiesToDamges.Length; i++)
        {
            enemiesToDamges[i].GetComponent<BaseCharacter>().TakeDamage(DKRangeDamage);
            Debug.Log("Enemy hit " + enemiesToDamges[i].gameObject.name + "for " + DKRangeDamage);
        }

    }

    public IEnumerator WindUpRangeAttack()
    {
        RangeAttackPos.transform.position = PlayerController.Instance.Player.position;
        IsAttackingNow = true;
        Debug.Log("Winding up attack " + WindUpTimeForRange + " Seconds");
        yield return new WaitForSeconds(WindUpTimeForRange);
        RangeAttack();
        RestartTimerForRangeAttacks();
        IsAttackingNow = false;
        AttackCount++;
    }
    public void RestartTimerForRangeAttacks() => TimeBtwAttack = _maxTimeBtwAttacks;

    public override State RunCurrentState()
    {
        if (!CanRangeAttack && !IsAttackingNow)
            return dKChaseState;

        return this;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
    }
}
