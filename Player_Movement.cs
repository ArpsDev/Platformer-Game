using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;

    [Header("Layer Masks")]
    [SerializeField] private LayerMask Ground;

    [Header("Ground Collision Variables")]
    [SerializeField] private float GroundRayCastLength;
    private bool OnGround;
    
    [Header("Movement Variables")]
    [SerializeField] private float MovementAccel = 70f;
    [SerializeField] private float MaxSpeed = 12f;
    [SerializeField] private float GroundLinearDrag = 7f;
    private float HorizontalDir;
    private bool ChangingDir => (rb.velocity.x > 0f && HorizontalDir < 0f) || (rb.velocity.x < 0f && HorizontalDir > 0f);

    [Header("Jump Variables")]
    [SerializeField] private float JumpForce = 12f;
    [SerializeField] private float AirLinearDrag = 2.5f;
    [SerializeField] private float FallMultiplier = 8f;
    [SerializeField] private float LowJumpFallMultiplier = 5f;
    [SerializeField] private int ExtraJumps = 1;
    private int ExtraJumpValue;
    private bool CanJump => Input.GetButtonDown("Jump") && (OnGround || ExtraJumpValue > 0) || IsWallSliding && Input.GetButtonDown("Jump");

    [Header("Wall Jump")]
    public float WallJumpTime = 0.2f;
    public float WallSlideSpeed = 0.3f;
    public float WallDistance = 0.5f;
    bool IsWallSliding = false;
    RaycastHit2D WallCheckHit;
    float JumpTime;

    [Header("Private Variables")]
    float mx = 0f;
    bool IsFacingRight;

    [Header("Animation")]
    public Animator Anim;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HorizontalDir = GetInput().x;

        if (CanJump) Jump();


        
    }

    private void FixedUpdate()
    {
        MoveCharacter();
       
        ApplyGroundLinearDrag();
       
        CheckCollisions();
        
        if (OnGround)
        {
            ExtraJumpValue = ExtraJumps;
            ApplyGroundLinearDrag();
        }
        else
        {
            ApplyAirLinearDrag();

            fallMultiplier();   
        }

        //Flip Player
        mx = Input.GetAxis("Horizontal");

        if (mx < 0)
        {
            IsFacingRight = false;
            transform.localScale = new Vector2(-1, transform.localScale.y);
        }
        else
        {
            IsFacingRight = true;
            transform.localScale = new Vector2(1, transform.localScale.y);
        }
        
        //Wall Jump
        if (IsFacingRight)
        {
            WallCheckHit = Physics2D.Raycast(transform.position, new Vector2(WallDistance, 0), WallDistance, Ground);
            Debug.DrawRay(transform.position, new Vector2(WallDistance, 0), Color.blue);
        }
        else
        {
            WallCheckHit = Physics2D.Raycast(transform.position, new Vector2(-WallDistance, 0), WallDistance, Ground);
            Debug.DrawRay(transform.position, new Vector2(-WallDistance, 0), Color.blue);
        }

        if (WallCheckHit && !OnGround && mx != 0)
        {
            IsWallSliding = true;
            JumpTime = Time.time + WallJumpTime;
        }
        else if (JumpTime < Time.time)
        {
            IsWallSliding = false;
            Anim.SetBool("IsJumping", false);
        }
   
        if (IsWallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, WallSlideSpeed, float.MaxValue));
            Anim.SetBool("IsJumping", true);
        }
    }

    private Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void MoveCharacter()
    {
        rb.AddForce(new Vector2(HorizontalDir, 0f) * MovementAccel);

        if (Mathf.Abs(rb.velocity.x) > MaxSpeed)
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * MaxSpeed, rb.velocity.y);

        Anim.SetFloat("Speed", Mathf.Abs(HorizontalDir));
    }

    private void ApplyGroundLinearDrag()
    {
        if (Mathf.Abs(HorizontalDir) < 0.4f || ChangingDir)
        {
            rb.drag = GroundLinearDrag;
        }
        else
        {
            rb.drag = 0f;
        }
    }

    private void ApplyAirLinearDrag()
    {
        rb.drag = AirLinearDrag;
    }
    
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);

        if (!OnGround)
        {
            ExtraJumpValue--;
        }
    }

    private void fallMultiplier()
    {
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = FallMultiplier;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.gravityScale = LowJumpFallMultiplier;
        }
        else
        {
            rb.gravityScale = 1f;
        }
    }
    
    private void CheckCollisions()
    {
        OnGround = Physics2D.Raycast(transform.position * GroundRayCastLength, Vector2.down, GroundRayCastLength, Ground);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * GroundRayCastLength);
    }
}
