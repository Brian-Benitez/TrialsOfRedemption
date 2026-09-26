using UnityEngine;

public class SpeedBoostPerk : UpgradePerk
{
    public float UpgradedMovementSpeed;
    public float NormalMovementSpeed;
    public PlayerMovement PlayerMovementRef;

    private void Start()
    {
        NormalMovementSpeed = PlayerMovementRef.FullSpeed;
    }
    public override void EnablePerk()
    {
        PlayerMovementRef.FullSpeed = UpgradedMovementSpeed;
        PlayerMovementRef.PlayerSpeed = UpgradedMovementSpeed;
    }

    public override void DisablePerk()
    {
        PlayerMovementRef.PlayerSpeed = NormalMovementSpeed;
    }
}
