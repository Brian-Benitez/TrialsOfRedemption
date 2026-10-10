using UnityEngine;

public class MeleeDamageBoostPerk : UpgradePerk
{
    public float DamageAdditionToAllMeleeAttks;
    private float _newDamageForNormalAttk;
    private float _newDamageForSpeicalAttk;
    private float _defaultNormalAttkDamg;
    private float _defaultSpeicalAttkDamg;
    public PlayerMeleeAttack PlayerMeleeAttackRef;

    private void Start()
    {
        _defaultNormalAttkDamg = PlayerMeleeAttackRef.PlayerLightAttkDamg;
        _defaultSpeicalAttkDamg = PlayerMeleeAttackRef.PlayerSpecialDamg;
    }
    public override void EnablePerk()
    {
        _newDamageForNormalAttk += DamageAdditionToAllMeleeAttks + _defaultNormalAttkDamg;
        _newDamageForSpeicalAttk += DamageAdditionToAllMeleeAttks + _defaultSpeicalAttkDamg;

        PlayerMeleeAttackRef.PlayerLightAttkDamg = _newDamageForNormalAttk;
        PlayerMeleeAttackRef.PlayerSpecialDamg = _newDamageForSpeicalAttk;
        PerksController.Instance.AddPerkToList(this);
    }

    public override void DisablePerk()
    {
        _newDamageForNormalAttk = 0;
        _newDamageForSpeicalAttk = 0;
        PlayerMeleeAttackRef.PlayerLightAttkDamg = _defaultNormalAttkDamg;
        PlayerMeleeAttackRef.PlayerSpecialDamg = _defaultSpeicalAttkDamg;
    }
}
