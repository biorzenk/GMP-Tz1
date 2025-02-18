using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Number Field")]
    [SerializeField] float _velo;
    [SerializeField] float _velocity;
    [SerializeField] float _JumpForce;

    [Header("GroundCheck JumpForce")]
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] Transform _groundCheck;

    [Header("Throw Position")]
    [SerializeField] float _ThrowPos;

    [Header("Health")]
    [SerializeField] int _maxHealth;

    private Rigidbody2D _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>(); 
    }

    #region States

    public void Move(Vector3 direction)
    {
        transform.Translate(direction * _velo * Time.deltaTime);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, 0.1f, _groundLayer);
    }

    void Update()
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
        if (Input.GetKey(KeyCode.W) && IsGrounded())
        {
            _rigidbody.AddForce(Vector2.up * _JumpForce, ForceMode2D.Impulse);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _rigidbody.AddForce(Vector2.down * _JumpForce, ForceMode2D.Impulse);
        }
        if (direction != Vector3.zero)
        {
            Move(direction);
        }
    }

    #endregion
}
