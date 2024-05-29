using System;
using System.Collections;
using Unity.Netcode;
using System.Collections.Generic;
using UnityEngine;

public class HumanMovement : NetworkBehaviour//using netcode connection
{
	#pragma warning disable 649
	[SerializeField] private CharacterController _controller;
	[SerializeField] private float _speed = 11f;
	[SerializeField] private float _gravity = -30f;
	[SerializeField] private float _jumpHeight = 3.5f;
	[SerializeField] private LayerMask _groundMask;

    private Camera playerCamera;
    private Vector3 _verticalVelocity = Vector3.zero;
	private PlayerInput _playerInput;
	private Vector2 _horizontalInput;
	private bool _isGrounded;
	private bool _jump;

    private void Start()
    {
        playerCamera = GetComponentInChildren<Camera>();
        if (playerCamera != null)
        {
            playerCamera.enabled = IsOwner;
        }
    }

    private void Awake()
	{
		_playerInput = new();
		_playerInput.Human.HorizontalMovement.performed += ctx => _horizontalInput = ctx.ReadValue<Vector2>();
		_playerInput.Human.Jump.performed += _ => OnJumpPressed();
	}
	
	private void OnEnable()
	{
		_playerInput.Enable();
	}
	
	private void OnDisable()
	{
		_playerInput.Disable();
	}
	
	private void Update()
	{
        if (!IsOwner) return;//check if own the object
        _isGrounded = Physics.CheckSphere(transform.position, 0.1f, _groundMask);
        if (_isGrounded)
        {
            _verticalVelocity.y = 0;
        }

        Vector3 horizontalVelocity = (transform.right * _horizontalInput.x + transform.forward * _horizontalInput.y) * _speed;

        _controller.Move(horizontalVelocity * Time.deltaTime);

        if (_jump)
        {
            if (_isGrounded)
            {
                _verticalVelocity.y = Mathf.Sqrt(-2f * _jumpHeight * _gravity);
            }

            _jump = false;
        }

        _verticalVelocity.y += _gravity * Time.deltaTime;
        _controller.Move(_verticalVelocity * Time.deltaTime);
	}
	
	private void OnJumpPressed ()
	{
		_jump = true;
	}
}
