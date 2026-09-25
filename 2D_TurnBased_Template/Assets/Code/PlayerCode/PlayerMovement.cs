using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float PlayerSpeed;
    public float FullSpeed;
    public float HalfSpeed;
    public GameObject PlayerObject;
    public Rigidbody2D Rb;

    public bool StopPlayerMovement = false;

    [Header("Dash Settings")]
    public KeyCode DashInputKey;
    public float DashSpeed;
    public float DashDuration;
    public float DashCoolDown;
    public float DashCoolDownUpgrade;
    [Header("Amount Of Dashes")]
    public int UsedDashes;
    public int MaxUsedDashes;
    public float TimerForMaxUsedDashes;
    public float MaxTimeForMaxUsedDashes;
    public bool IsDashing;
    public bool CanDash = true;
    public bool IsDashPaused = false;

    Vector2 moveDirection;
    Vector2 mousePosition;
    float Horizontal;
    [Header("Animator info")]
    public FlipSprite FlipSpriteRef;
    public PlayerAnimationController PlayerAnimationControllerRef;
    //public PlayerStunnedState PlayerStunnedStateRef;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1) || Input.GetMouseButtonDown(0)) //|| PlayerStunnedStateRef.IsPlayerStuuned)
        {
            Rb.linearVelocity = new Vector2(0, 0);
            moveDirection = Vector2.zero;
        }

        if (IsDashing)
            return;


        if (moveDirection == Vector2.zero || StopPlayerMovement)
            PlayerAnimationControllerRef.IsNotMoving();
        else
        {
            PlayerAnimationControllerRef.IsMoving();
        }

        if (StopPlayerMovement || XPController.Instance.IsUpgrading)
        {
            moveDirection = Vector2.zero;
            Rb.linearVelocity = new Vector2(0, 0);
            return;
        }
        else
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");
            moveDirection = new Vector2(moveX, moveY).normalized;
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
  

        if(UsedDashes == MaxUsedDashes)
        {
            if(TimerForMaxUsedDashes >= MaxTimeForMaxUsedDashes)
            {
                IsDashPaused = false;
                TimerForMaxUsedDashes = 0;
                UsedDashes = 0;
            }
            else
            {
                IsDashPaused = true;
                TimerForMaxUsedDashes += Time.deltaTime;
            }
        }

        if (Input.GetKeyDown(DashInputKey) && CanDash && !IsDashPaused)
        {
            StartCoroutine(Dash());
            UsedDashes++;//note:this makes it so if player dashes once it will still be there until 3 dashes.
        }
    }

    private void FixedUpdate()
    {
        if (IsDashing)
            return;

        Rb.linearVelocity = new Vector2(moveDirection.x * PlayerSpeed, moveDirection.y * PlayerSpeed);
    }

    public IEnumerator Dash()
    {
        CanDash = false;
        IsDashing = true;
        Rb.linearVelocity = new Vector2(moveDirection.x * DashSpeed, moveDirection.y * DashSpeed);
        yield return new WaitForSeconds(DashDuration);
        IsDashing = false;
        yield return new WaitForSeconds(DashCoolDown);
        CanDash = true;
    }
    /// <summary>
    /// Slows player down and cannot dash
    /// </summary>
    public void SlowPlayer()
    {
        PlayerSpeed = HalfSpeed;
        IsDashPaused = true;
    }
    /// <summary>
    /// unslows player and can dash
    /// </summary>
    public void UnSlowPlayer()
    {
        PlayerSpeed = FullSpeed;
        IsDashPaused = false;
    }

    /// <summary>
    /// Stops player from moving and dashing
    /// </summary>
    public void TurnOnStopPlayerMovement()
    {
        StopPlayerMovement = true;
        IsDashPaused = true;
        FlipSpriteRef.IsMeleeing = true;
    }
    /// <summary>
    /// Lets players move and dash again.
    /// </summary>
    public void TurnOffStopPlayerMovement()
    {
        StopPlayerMovement = false;
        IsDashPaused = false;
        FlipSpriteRef.IsMeleeing = false;
    }
}
