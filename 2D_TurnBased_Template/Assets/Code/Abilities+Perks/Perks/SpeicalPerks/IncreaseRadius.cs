using UnityEngine;

public class IncreaseRadius : UpgradePerk
{
    [Header("Stats Info")]
    public float AddedRadiusForSpeical;
    public float OldRadiusSpeical;
    float _oldWaitTime;
    public float AdditionalWaitTime;
    public PlayerMeleeAttack PlayerMeleeAttackRef;

    private void Start()
    {
        OldRadiusSpeical = PlayerMeleeAttackRef.SpeicalRange;
        _oldWaitTime = PlayerMeleeAttackRef._maxwaitTimeForSpeical;
    }
    public override void EnablePerk()
    {
        if (!IsPerkActive)
        {
            PlayerMeleeAttackRef.SpeicalRange += AddedRadiusForSpeical;
            PlayerMeleeAttackRef._maxwaitTimeForSpeical += AdditionalWaitTime;//increase wait time .16f
            PerksController.Instance.AddPerkToList(this.gameObject);
        }
        else
        {
            Debug.Log("cannot use perk again already in use!");
        }
        
    }

    public override void DisablePerk()
    {
        if(IsPerkActive)
        {
            PlayerMeleeAttackRef.SpeicalRange = OldRadiusSpeical;
            PlayerMeleeAttackRef._maxwaitTimeForSpeical = _oldWaitTime;
            Debug.Log("radius is decrease");
        }
    }
}
