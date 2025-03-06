﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies_Move : MonoBehaviour
{
    [Header("Enemy Stats")] 
    [SerializeField] private float _speedBot = 4f;
    [SerializeField] private float _Site = 10f;
    [SerializeField] private float _attackRange = 1.2f;
    [SerializeField] private float _patrolDistance = 10f;
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _MaxHealth = 100f;

    private float _currentHealth;
    private Transform _player;
    private Rigidbody2D _rb;
    private Animator _animator;
    private Vector2 _startPosition;
    private bool _movingRight = true;
    bool isFacingRight = true;
    bool isAttacking = false;
    bool isDead = false;    

    void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _startPosition = transform.position;
        _currentHealth = _MaxHealth;
    }

    void Update()
    {
        if (_player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, _player.position);
            if (distanceToPlayer <= _Site)
            {
                if (distanceToPlayer <= _attackRange)
                {
                    AttackPlayer();
                }
                else if (!isAttacking)
                {
                    MoveTowardsPlayer();
                }
            }
            else if (!isAttacking)
            {
                Patrol();
            }
        }
    }

    void FixedUpdate()
    {
        if (!isAttacking)
        {
            _animator.SetFloat("XMove", Mathf.Abs(_rb.velocity.x));
            FlipSprite();
        }

        if (isDead)
        {
            _animator.SetBool("Dead", true);
        }
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

    private void MoveTowardsPlayer()
    {
        Vector2 direction = (_player.position - transform.position).normalized;
        _rb.velocity = direction * _speedBot;
    }

    private void Patrol()
    {
        if (_movingRight)
        {
            _rb.velocity = new Vector2(_speedBot, _rb.velocity.y);
            if (transform.position.x >= _startPosition.x + _patrolDistance)
            {
                _movingRight = false;
            }
        }
        else
        {
            _rb.velocity = new Vector2(-_speedBot, _rb.velocity.y);
            if (transform.position.x <= _startPosition.x - _patrolDistance)
            {
                _movingRight = true;
            }
        }
    }

    private void AttackPlayer()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            _rb.velocity = Vector2.zero;
            _animator.SetFloat("XMove", 0);
            _animator.SetTrigger("Attacking");
            StartCoroutine(ResetAttack());
        }
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(1f);
        isAttacking = false;
    }

    public void TakeDamage(float damage)
    {
        if (!isDead)
        {
            _currentHealth -= damage;
            Debug.Log(_currentHealth);
            if (_currentHealth <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        _rb.velocity = Vector2.zero;
        isDead = true;
        _animator.SetTrigger("Dead");
        Destroy(gameObject, 1f);
    }
}