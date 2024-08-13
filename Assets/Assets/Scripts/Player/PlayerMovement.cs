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

    #region States
    #endregion
}
