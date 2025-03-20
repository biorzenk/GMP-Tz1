using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Data : MonoBehaviour
{
    [Header("Gravity")]
    [HideInInspector] public float gravityScale;
    [HideInInspector] public float gravityStrength;

    [Space(5)]
    public float fallGravityMult;
    public float maxFallSpeed;
    [Space(5)]
    public float fastFallGravityMult;
    public float maxFastFallSpeed;

    [Space(20)]

    [Header("Run")]
    public float runMaxSpeed;
    public float runAcceleration;
    [HideInInspector] public float runAccelAmount;
    public float runDecceleration;
    [HideInInspector] public float runDeccelAmount;

    [Space(5)]
    [Range(0f, 1)] public float accelInAir;
    [Range(0f, 1)] public float deccelInAir;

    [Space(5)]
    public bool doConserveMomentum = true;

    [Space(20)]
    [Header("Jump")]
    public float jumpHeight;
    public float jumpTimeToApex;
    [HideInInspector] public float jumpForce;

    [Header("Both Jump")]
    public float jumpCutGravityMult;
    [Range(0f, 1)] public float jumpHangGravityMult;
    public float jumpHangTimeThreshold;
    [Space(5)]
    public float jumpHangAccelerationMult;
    public float jumpHanngMaxSpeedMult;

    [Header("Wall Jump")]
    public Vector2 walJumpForce;
    [Space(5)]
    [Range(0f, 1f)] public float wallJumpRunLerp;
    [Range(0f, 1.5f)] public float wallJumpTime;
    public bool doTurnOnWallJump;

    [Space(20)]

    [Header("Slide")]
    public float slideSpeed;
    public float slideAccel;

    [Header("Assists")]
    [Range(0.01f, 0.5f)] public float coyoteTime;
    [Range(0.01f, 0.5f)] public float JumpInputBufferTime;

    private void OnValidate()
    {
        gravityStrength = -(2 * jumpHeight) / (jumpTimeToApex * jumpTimeToApex);

        gravityScale = gravityStrength / Physics2D.gravity.y;

        runAccelAmount = (50 * runAcceleration) / runMaxSpeed;
        runDeccelAmount = (50 * runDecceleration) / runMaxSpeed;

        jumpForce = Mathf.Abs(gravityStrength) * jumpTimeToApex;

        runAcceleration = Math.Clamp(runAcceleration, 0.01f, runMaxSpeed);
        runDecceleration = Math.Clamp(runDecceleration, 0.01f, runMaxSpeed);
    }
           
}
