using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_666 : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] private float _damage = 40f;
    [SerializeField] private float _MaxHealth = 66f;

    private float _currentHealth;
    private Transform _player;
    private Rigidbody2D _rb;
    private Animator _animator;
    bool isAttackRight = true;
    bool isAttacking = false;
    bool isDead = false;

    void Start()
    {
        _currentHealth = _MaxHealth;
        _player = GameObject.FindWithTag("Player").transform;
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, _player.position);
            if (distanceToPlayer <= 10f)
            {
                if (distanceToPlayer <= 1.2f)
                {
                    AttackPlayer();
                }
            }
        }
    }


    void AttackPlayer()
    {
        isAttacking = true;
        _rb.velocity = Vector2.zero;
        _animator.SetTrigger("Attack");
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            isDead = true;
            _animator.SetTrigger("Death");
            Destroy(gameObject, 1.0f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerAttack player = collision.gameObject.GetComponent<PlayerAttack>();
            if (player != null)
            {
                player.TakeDamage(_damage);
            }
        }
    }
}
