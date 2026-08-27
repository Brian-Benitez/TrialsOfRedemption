using Unity.Cinemachine;
using UnityEngine;

public class PlayerRangeWeapon : MonoBehaviour
{
    [Header("Players arrows info")]
    public float SpeedOfArrow;
    public float TimeBtwAttack;
    public GameObject PlayersArrowGO;
    public Rigidbody2D PlayerArrowPrefabsRB;
    public Transform ShotPoint;
    public bool CanRangeAttackAgain;

    [Header("For upgrades below")]
    public bool IsUsingShotgunPerk = false;
    public Transform ShotPointTwo;
    public Transform ShotPointThree;
    public float LoweredRangeDistance;

    public CinemachineImpulseSource rangeImpulseSource;
    private float _maxTimeBtwAttacks;
    private PlayerMovement PlayerMovementRef;
    private Rigidbody2D _arrowPrefabRB;
    private Rigidbody2D _arrowPrefabRBTwo;
    private Rigidbody2D _arrowPrefabRBThree;

    private void Start()
    {
        _maxTimeBtwAttacks = TimeBtwAttack;
        PlayerMovementRef = GetComponentInParent<PlayerMovement>();
        PlayersArrowGO.GetComponent<Projectile>().LifeTimeOfProjectile = 1.5f;
        PlayersArrowGO.GetComponent<Projectile>().DistanceOfProjectile = 1.5f;
    }


    private void Update()
    {
        if (PlayerMovementRef.IsDashing)
            return;

        if(Input.GetMouseButton(1) && Input.GetMouseButtonDown(0) && PlayerAmmoController.Instance.DoesPlayerHaveAmmo() && CanRangeAttackAgain)
        {
            CameraShakeManager.Instance.ShakeCamera(rangeImpulseSource);
            PlayerShootsArrowAction();
            PlayerAmmoController.Instance.RemoveAmmo();
            RestartTimerForRangeAttacks();
        }

        if(TimeBtwAttack <= 0)
        {
            CanRangeAttackAgain = true;
            return;
        }
        else
        {
            TimeBtwAttack -= Time.deltaTime;
            CanRangeAttackAgain = false;
        }

    }

    public void PlayerShootsArrowAction()
    {
        if(IsUsingShotgunPerk)
        {
            _arrowPrefabRB = Instantiate(PlayerArrowPrefabsRB, ShotPoint.position, transform.rotation);
            _arrowPrefabRB.linearVelocity = _arrowPrefabRB.transform.up * SpeedOfArrow;
            _arrowPrefabRBTwo = Instantiate(PlayerArrowPrefabsRB, ShotPointTwo.position, transform.rotation);
            _arrowPrefabRBTwo.linearVelocity = _arrowPrefabRB.transform.up * SpeedOfArrow;
            _arrowPrefabRBThree = Instantiate(PlayerArrowPrefabsRB, ShotPointThree.position, transform.rotation);
            _arrowPrefabRBThree.linearVelocity = _arrowPrefabRB.transform.up * SpeedOfArrow;

        }
        else
        {
            _arrowPrefabRB = Instantiate(PlayerArrowPrefabsRB, ShotPoint.position, transform.rotation);
            _arrowPrefabRB.linearVelocity = _arrowPrefabRB.transform.up * SpeedOfArrow;
        }
        
    }

    public void ChangeArrowsDurations()
    {
        PlayersArrowGO.GetComponent<Projectile>().LifeTimeOfProjectile -= Mathf.Clamp(LoweredRangeDistance, 0, 9);
        PlayersArrowGO.GetComponent<Projectile>().DistanceOfProjectile -= Mathf.Clamp(LoweredRangeDistance, 0, 9);
    }
    public void NormalArrowDurations()
    {
        IsUsingShotgunPerk = false;
        PlayersArrowGO.GetComponent<Projectile>().LifeTimeOfProjectile += Mathf.Clamp(LoweredRangeDistance, 0, 9);
        PlayersArrowGO.GetComponent<Projectile>().DistanceOfProjectile += Mathf.Clamp(LoweredRangeDistance, 0, 9);
    }
    void RestartTimerForRangeAttacks() => TimeBtwAttack = _maxTimeBtwAttacks;

}
