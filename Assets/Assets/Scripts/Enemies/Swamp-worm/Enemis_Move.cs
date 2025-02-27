using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemis_Move : MonoBehaviour
{
    [SerializeField] private float _speedBot = 2f;
    [SerializeField] private float _Site = 20f;

    private Transform _player;
    private Rigidbody2D _rb;
    private Animator _animator;
    bool isFacingRight = true;
    bool isAttacking = false;

    void Start()
    {
        _player = GameObject.Find("Player").transform;
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, _player.position);
            if (distanceToPlayer <= _Site)
            {
                Vector2 direction = (_player.position - transform.position).normalized;
                _rb.velocity = direction * _speedBot;
            }
            else
            {
                _rb.velocity = Vector2.zero;
            }
        }
    }

    void FixedUpdate()
    {
        _rb.velocity = new Vector2(_speedBot, _rb.velocity.y);
        _animator.SetFloat("XMove", Mathf.Abs(_rb.velocity.x));
        FlipSprite();
    }

    private void FlipSprite()
    {
        if (isFacingRight && _rb.velocity.x < 0f || !isFacingRight && _rb.velocity.x > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1;
            transform.localScale = ls;
        }
    }
}
