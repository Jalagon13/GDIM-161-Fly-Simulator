using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanInput : MonoBehaviour
{
	private PlayerInput _playerInput;
	private Vector2 _horizontalInput;
	
	private void Awake()
	{
		_playerInput = new();
		_playerInput.Human.HorizontalMovement.performed += ctx => _horizontalInput = ctx.ReadValue<Vector2>();
	}
	
	private void OnEnable()
	{
		_playerInput.Enable();
	}
	
	private void OnDisable()
	{
		_playerInput.Disable();
	}
}
