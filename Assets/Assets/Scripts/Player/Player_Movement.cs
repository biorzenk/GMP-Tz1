using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public Player_Data Data;

    #region STATE PARAMETERS
    private Rigidbody2D _rb;
    private Animator _animator;
    private bool IsFacingRight = true;
    private bool isJumping;
    private bool isWallJumping;
    private bool isSliding;

    private float LastOnGroundTime;
    private float LastOnWallTime;
    private float LastOnWallRightTime;
    private float LastOnWallLeftTime;

    // Jump
    private bool _isJumpCut;
    private bool _isJumpFalling;

    // Wall Jump
    private float _wallJumpStartTime;
    private float _lastWallJumpDir;

    private Vector2 _moveInput;
    private float LastPressedJumpTime;

    // Dash
    private int dashesLeft;
    private bool _dashRefilling;
    private Vector2 _lastDashDir;
    private bool _isDashAttacking;
    #endregion

    #region INPUT PARAMETERS
    private float LastPressedDashTime;
    #endregion

    #region LAYER & TAGS
    [Header("Layer & Tags")]
    [SerializeField] private LayerMask _groundLayer;
    #endregion

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        SetGravityScale(Data.gravityScale);
    }

    private void Update()
    {
        LastOnGroundTime -= Time.deltaTime;
        LastOnWallLeftTime -= Time.deltaTime;
        LastOnWallRightTime -= Time.deltaTime;
        LastOnWallTime -= Time.deltaTime;
        LastPressedJumpTime -= Time.deltaTime;

        _moveInput.x = Input.GetAxis("Horizontal");
        _moveInput.y = Input.GetAxis("Vertical");

        if (_moveInput.x != 0)
        {
            CheckDirectionToFace(_moveInput.x > 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.J))
        {
            OnJumpInput();
        }

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.C) || Input.GetKeyUp(KeyCode.J))
        {
            OnJumpUpInput();
        }

        CheckCollisions();
        UpdateAnimations();
    }

    private void CheckCollisions()
    {
        Collider2D groundCheck = Physics2D.OverlapBox(transform.position, new Vector2(0.5f, 0.1f), 0, _groundLayer);
        if (groundCheck != null)
        {
            LastOnGroundTime = Data.coyoteTime;
            isJumping = false;
        }
    }

    private void OnJumpInput()
    {
        if (LastOnGroundTime > 0)
        {
            isJumping = true;
            LastOnGroundTime = 0;
            _rb.velocity = new Vector2(_rb.velocity.x, Data.jumpForce);
        }
    }

    private void OnJumpUpInput()
    {
        if (_rb.velocity.y > 0)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * Data.jumpCutGravityMult);
        }
    }

    private void UpdateAnimations()
    {
        _animator.SetBool("isJumping", isJumping);
        _animator.SetFloat("Speed", Mathf.Abs(_moveInput.x));
    }

    private void SetGravityScale(float scale)
    {
        _rb.gravityScale = scale;
    }

    private void CheckDirectionToFace(bool isMovingRight)
    {
        if (isMovingRight != IsFacingRight)
        {
            IsFacingRight = isMovingRight;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
}
