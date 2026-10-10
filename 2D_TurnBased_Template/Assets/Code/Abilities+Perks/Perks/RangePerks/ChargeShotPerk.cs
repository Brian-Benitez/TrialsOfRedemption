using Unity.Cinemachine;
using UnityEngine;

public class ChargeShotPerk : UpgradePerk
{
    public bool IsUsingChargeShot = false;
    bool IsUpdatedDamgAmount = false;
    float _damageMultipler = 2f;
    float _normalDamg;
    float newRangeDamg;
    public float CurrentHoldTimer = 0f;
    float _maxHoldTimer = 3f;
    public BaseCharacter RangeAttkDamRef;

    private void Start()
    {
        _normalDamg = RangeAttkDamRef.RangeDamg;
    }

    private void Update()
    {
        if(IsUsingChargeShot)
        {
            ChargeShotFunctionality();
        }
    }

    void ChargeShotFunctionality()
    {
        if (Input.GetMouseButtonUp(1))
        {
            Debug.Log("let go of charge shot!");
            CurrentHoldTimer = 0f;
            RangeAttkDamRef.RangeDamg = _normalDamg;
            IsUpdatedDamgAmount = false;
        }
        if (Input.GetMouseButton(1))
        {
            if (CurrentHoldTimer >= _maxHoldTimer)
            {
                Debug.Log("charge shot ready!");
                CurrentHoldTimer = _maxHoldTimer;
                if (!IsUpdatedDamgAmount)
                {
                    _normalDamg = RangeAttkDamRef.RangeDamg;
                    newRangeDamg = _damageMultipler * RangeAttkDamRef.RangeDamg;
                    RangeAttkDamRef.RangeDamg = newRangeDamg;
                    IsUpdatedDamgAmount = true;
                }
            }
            else
            {
                CurrentHoldTimer += Time.deltaTime;
            }
        }
    }
    public override void EnablePerk()
    {
        IsUsingChargeShot = true;
        PerksController.Instance.AddPerkToList(this);
    }

    public override void DisablePerk()
    {
        IsUsingChargeShot = false;
    }
}
