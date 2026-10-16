using UnityEngine;

public class BossSoulPickUp : BasePickUp
{
    public void PickUpBossSoul()
    {
        AudioController.Instance.PlayItemPickUpSound();
        SoulsBankController.Instance.PayoutBossSoulToPlayer();
    }
}
