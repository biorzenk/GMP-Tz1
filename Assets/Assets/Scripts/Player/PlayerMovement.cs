using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] public float _Velo;
    [SerializeField] public float _JumpForce;



    private Rigidbody2D _rb;
    float horizontalInput;
    bool isGrounded = false;
    private Animator animator;
    bool isFacingRight = true;
    private bool canAttack = true;

    #region States

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }                                   

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        FlipSprite();
        if (Input.GetButtonDown("Jump") && isGrounded) 
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _JumpForce);
            isGrounded = false;
            animator.SetBool("isJumping", true);
            canAttack = false;
            animator.SetBool("Attack", false);
        }
    }

    void FixedUpdate()
    {
        _rb.velocity = new Vector2(horizontalInput * _Velo, _rb.velocity.y);
        animator.SetFloat("Xvelocity", Mathf.Abs(_rb.velocity.x));
        animator.SetFloat("Yvelocity", _rb.velocity.y); 
    }

    private void FlipSprite()
    {
        if (isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1;
            transform.localScale = ls;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {   
        isGrounded = true;
        animator.SetBool("isJumping", false);
    }

   
    #endregion
}