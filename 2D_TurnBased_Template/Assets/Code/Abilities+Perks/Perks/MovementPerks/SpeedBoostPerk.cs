using UnityEngine;

public class SpeedBoostPerk : UpgradePerk
{
    public float UpgradedMovementSpeed;
    public float NormalMovementSpeed;
    public PlayerMovement PlayerMovementRef;

    private void Start()
    {
        NormalMovementSpeed = PlayerMovementRef.DefaultSpeed;
    }
    public override void EnablePerk()
    {
        PlayerMovementRef.DefaultSpeed = UpgradedMovementSpeed;
        PerksController.Instance.AddPerkToList(this);
    }

    public override void DisablePerk()
    {
        PlayerMovementRef.DefaultSpeed = NormalMovementSpeed;
    }
}
