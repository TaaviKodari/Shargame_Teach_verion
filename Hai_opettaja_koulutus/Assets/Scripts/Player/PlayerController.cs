using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5f;

    public Transform gunTransform;

    private Rigidbody2D body;
    private Master controls;
    private Vector2 moveInput;

    private Vector2 aimInput;

    private void Awake()
    {
        controls = new Master();
        body = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        GameManager.Instance.playerController = this;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    #region Movement calls
    private void FixedUpdate()
    {
        if (CheckGameState() == false)
        {
            return;
        }

        Move();
    }

    private void Move()
    {
       moveInput = controls.Player.Move.ReadValue<Vector2>();
       Vector2 movement = new Vector2(moveInput.x, moveInput.y) * moveSpeed * Time.fixedDeltaTime;
       body.MovePosition(body.position + movement);
    }
    #endregion

    private void Update()
    {
       if(CheckGameState() == false)
       {
            return;
       }

        Shoot();
        Aim();
    }

    private bool CheckGameState()
    {
        if(GameManager.Instance.IsGamePlay())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Shoot()
    {
        if (controls.Player.Shoot.triggered)
        {
            GameObject bullet = BulletPoolManager.Instance.GetBullet();
            bullet.transform.position = gunTransform.position;
            bullet.transform.rotation = gunTransform.rotation;
        }
    }

    private bool UsingMouse()
    {
        if (Mouse.current.delta.ReadValue().sqrMagnitude > 0.1)
        {
            return true;
        }

        return false;
    }

    private void Aim()
    {
        aimInput = controls.Player.Aim.ReadValue<Vector2>();
        if (aimInput.sqrMagnitude > 0.1)
        {
            Vector2 aimDirection;
            if (UsingMouse())
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                mousePosition.z = 0;
                aimDirection = mousePosition - gunTransform.position;
            }
            else
            {
                aimDirection = aimInput;
            }
            float angle = (Mathf.Atan2(aimDirection.x, -aimDirection.y)) * Mathf.Rad2Deg;
            gunTransform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }


}
