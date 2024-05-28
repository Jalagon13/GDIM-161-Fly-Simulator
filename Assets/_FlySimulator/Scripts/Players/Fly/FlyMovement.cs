using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class FlyMovement : NetworkBehaviour//using netcode connection
{
	#pragma warning disable 649
	[SerializeField] private CharacterController _controller;
	[SerializeField] private float _horizontalSpeed = 11f;
	[SerializeField] private float _verticalSpeed = 7f;
	[SerializeField] private float _sensitivityX = 8f;
	[SerializeField] private float _sensitivityY = 0.5f;
	
	private PlayerInput _playerInput;
	private Vector2 _horizontalInput;
	private float _verticalInput;
	private Vector2 _mouseInput;

	private void Awake()
	{
		_playerInput = new();
		_playerInput.Fly.HorizontalMovement.performed += ctx => _horizontalInput = ctx.ReadValue<Vector2>();
		_playerInput.Fly.VerticalMovement.performed += ctx => _verticalInput = ctx.ReadValue<float>();
		_playerInput.Fly.MouseX.performed += ctx => _mouseInput.x = ctx.ReadValue<float>();
		_playerInput.Fly.MouseY.performed += ctx => _mouseInput.y = ctx.ReadValue<float>();
	}

	private void Start()
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
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

		Vector3 horizontalVelocity = (transform.right * _horizontalInput.x + transform.forward * _horizontalInput.y) * _horizontalSpeed;
		_controller.Move(horizontalVelocity * Time.deltaTime);

		Vector3 verticalVelocity = _verticalInput * _verticalSpeed * transform.up;
		_controller.Move(verticalVelocity * Time.deltaTime);
		
		float mouseX = _mouseInput.x * _sensitivityX * Time.deltaTime;
		float mouseY = _mouseInput.y * _sensitivityY * Time.deltaTime;
		transform.Rotate(0, mouseX, 0, Space.World);
        transform.Rotate(-mouseY, 0, 0, Space.Self);
    }
}