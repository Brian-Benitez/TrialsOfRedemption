using UnityEngine;

public class IncreaseRadius : UpgradePerk
{
    [Header("Stats Info")]
    public float AddedRadiusForSpeical;
    public float OldRadiusSpeical;
    public float AdditionalWaitTime;
    public PlayerMeleeAttack PlayerMeleeAttackRef;

    private void Start()
    {
        OldRadiusSpeical = PlayerMeleeAttackRef.SpeicalRange;
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
            PlayerMeleeAttackRef._maxwaitTimeForSpeical -= AdditionalWaitTime;
            Debug.Log("radius is decrease");
        }
    }
}
