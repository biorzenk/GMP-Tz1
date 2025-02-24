using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Number Field")]
    [SerializeField] public float velo;
    [SerializeField] public float jumpForce;

    [Header("GroundCheck JumpForce")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;

    [Header("Health")]
    [SerializeField] public int maxHealth;

    private Rigidbody2D rigidbody2D;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    #region States

    public void Move(Vector3 direction)
    {
        transform.Translate(direction * velo * Time.deltaTime);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        if (IsGrounded() && Input.GetKey(KeyCode.K))
        {
            rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void HandleInput()
    {
        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.A))
        {
            direction = Vector3.left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            direction = Vector3.right;
        }

        if (direction != Vector3.zero)
        {
            Move(direction);
        }
    }

    #endregion
}