using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemis_Move : MonoBehaviour
{
    [SerializeField] private float _speedBot = 2f; // Tốc độ di chuyển

    private Transform _player;
    private Rigidbody2D _rb;

    void Start()
    {
        _player = GameObject.Find("Player").transform;
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (_player != null)
        {
            Vector2 direction = (_player.position - transform.position).normalized; // Hướng đến Player
            _rb.velocity = direction * _speedBot; // Áp dụng vận tốc
        }
    }
}
