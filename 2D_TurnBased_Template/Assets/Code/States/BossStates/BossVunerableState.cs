using UnityEngine;

public class BossVunerableState : MonoBehaviour
{
    public bool IsVunerable = false;
    public float AttkCooldown;
    public float MaxCooldown;
    private bool _startCountdown = false;
    public StunState StunStateRef;
    public AttackState AttackStateRef;

    private void Update()
    {
        if(AttackStateRef.AttackCooldownTimer > AttackStateRef.MaxTimerOfCooldown)
            _startCountdown = true;
        if(_startCountdown)
        {
            AttkCooldown += Time.deltaTime;
            if (AttkCooldown >= MaxCooldown)
            {
                IsVunerable = false;
                StunStateRef.InstanteStun = false;
                AttkCooldown = 0;
                _startCountdown = false;
            }
            else
            {
                IsVunerable = true;
                StunStateRef.InstanteStun = true;
            }
        }
           
    }
}
