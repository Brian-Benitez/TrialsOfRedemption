using UnityEngine;

public class SpeedBoostPerk : UpgradePerk
{
    public bool IsUsingSpeedBoost = false;
    public float UpgradedMovementSpeed;
    public PlayerMovement PlayerMovementRef;

    public override void EnablePerk()
    {
        IsUsingSpeedBoost = true;
        PerksController.Instance.AddPerkToList(this);
    }

    public override void DisablePerk()
    {
        IsUsingSpeedBoost = false;
    }
}
