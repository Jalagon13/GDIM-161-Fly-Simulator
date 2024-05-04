using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
	#pragma warning disable 649
	[SerializeField] private float _sensitivityX = 8f;
	[SerializeField] private float _sensitivityY = 0.5f;
	[SerializeField] private Transform _playerCamera;
	[SerializeField] private float xClamp = 85f;
	
	private float _mouseX, _mouseY;
	private Vector2 _mouseInput;
	private float _xRotation = 0f;
	
	private PlayerInput _playerInput;
	
	private void Awake()
	{
		_playerInput = new();
		_playerInput.Human.MouseX.performed += ctx => _mouseInput.x = ctx.ReadValue<float>();
		_playerInput.Human.MouseY.performed += ctx => _mouseInput.y = ctx.ReadValue<float>();
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
		transform.Rotate(Vector3.up, _mouseX * Time.deltaTime);
		RecieveInput(_mouseInput);
		_xRotation -= _mouseY;
		_xRotation = Mathf.Clamp(_xRotation, -xClamp, xClamp);
		Vector3 targetRotation = transform.eulerAngles;
		targetRotation.x = _xRotation;
		_playerCamera.eulerAngles = targetRotation;
	}
	
	private void RecieveInput(Vector2 mouseInput)
	{
		_mouseX = mouseInput.x * _sensitivityX;
		_mouseY = mouseInput.y * _sensitivityY;
	}
}
