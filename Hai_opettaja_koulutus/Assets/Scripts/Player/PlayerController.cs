using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5f;

    private Rigidbody2D body;
    private Master controls;
    private Vector2 moveInput;

    private void Awake()
    {
        controls = new Master();
        body = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
       moveInput = controls.Player.Move.ReadValue<Vector2>();
       Vector2 movement = new Vector2(moveInput.x, moveInput.y) * moveSpeed * Time.fixedDeltaTime;
       body.MovePosition(body.position + movement);
    }
}
