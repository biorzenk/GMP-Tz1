using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_666 : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] private float _damage = 40f;
    [SerializeField] private float _MaxHealth = 66f;
    [SerializeField] private float _Site = 10f;
    [SerializeField] private float _attackRange = 1.2f;

    private float _currentHealth;
    private Transform _player;
    private Rigidbody2D _rb;
    private Animator _animator;
    bool isAttacking = false;
    bool isDead = false;
    private bool isFacingRight = true;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        _currentHealth = _MaxHealth;
        _player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (_player != null && !isDead)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, _player.position);
            if (distanceToPlayer <= _Site)
            {
                if (distanceToPlayer <= _attackRange)
                {
                    AttackPlayer();
                }
            }
            else
            {
                TowardsPlayer();
            }
        }
    }
    void TowardsPlayer()
    {
        Vector2 direction = (_player.position - transform.position).normalized;

        if (direction.x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (direction.x < 0 && isFacingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 ls = transform.localScale;
        ls.x *= -1;
        transform.localScale = ls;
    }
    void AttackPlayer()
    {
        isAttacking = true;
        if (isFacingRight)
        {
            _animator.SetFloat("AttackDirection", 1f);
        }
        else
        {
            _animator.SetFloat("AttackDirection", -1f);
        }

        _animator.SetTrigger("Attack");
        StartCoroutine(ResetAttack());
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(1f);
        isAttacking = false;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            isDead = true;
            _animator.SetTrigger("Die");
            Destroy(gameObject, 1.0f);
        }
    }

    private void Die()
    {
        isDead = true;
        _rb.velocity = Vector2.zero;
        _animator.SetTrigger("Death");
        Destroy(gameObject, 1.0f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerAttack player = collision.gameObject.GetComponent<PlayerAttack>();
            if (player != null)
            {
                AttackPlayer();
                player.TakeDamage(_damage);
                Debug.Log(player._currentHealth);
            }
        }
    }
}
