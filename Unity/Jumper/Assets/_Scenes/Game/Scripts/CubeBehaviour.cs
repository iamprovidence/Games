using System;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehaviour : MonoBehaviour
{
	[SerializeField]
	private float _squeezeSpeed = 0.5f;
	[SerializeField]
	private List<CubeJumpBehaviour> _cubeJumpBehaviour = new List<CubeJumpBehaviour>();

	private bool _didJump = false;
	private bool _isPressed = false;
	private float _yCubePosition;

	public event Action CubeLanded;
	public event Action PlayerLose;

	private void Start()
	{
		GetComponent<Renderer>().material.color = CurrentUserInfo.GetSelectedCubeColor();
	}

	private void OnEnable()
	{
		foreach (var jumpBehaviour in _cubeJumpBehaviour)
		{
			jumpBehaviour.Pressed += OnPressed;
			jumpBehaviour.Released += OnRelease;
		}
	}

	private void OnDisable()
	{
		foreach (var jumpBehaviour in _cubeJumpBehaviour)
		{
			jumpBehaviour.Pressed -= OnPressed;
			jumpBehaviour.Released -= OnRelease;
		}
	}

	private void OnPressed()
	{
		_isPressed = true;
		_yCubePosition = transform.position.y;
	}

	private void OnRelease(float force)
	{
		_isPressed = false;
		_didJump = true;

		PushCubeUp(force);
	}

	private void FixedUpdate()
	{
		if (_isPressed)
		{
			TrySqueeze();
		}
		else
		{
			TryUnSqueeze();
		}

		if (IsLanded() && IsNextStair())
		{
			ResetCubeState();
			OnSuccesfullyLanded();
		}

		if (IsCubeFalling())
		{
			OnPlayerLose();
		}
	}

	private void TrySqueeze()
	{
		if (transform.localScale.y > 0.4f)
		{
			PressCube(-_squeezeSpeed);
		}
	}
	private void TryUnSqueeze()
	{
		if (transform.localScale.y < 1f)
		{
			PressCube(_squeezeSpeed * 4);
		}
		else
		{
			transform.localScale = new Vector3(1f, 1f, 1f);
		}
	}

	private void PressCube(float force)
	{
		transform.localPosition += new Vector3(0f, force * Time.deltaTime, 0f);
		transform.localScale += new Vector3(0f, force * Time.deltaTime, 0f);
	}

	private void PushCubeUp(float force)
	{
		GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * force);
		GetComponent<Rigidbody>().AddRelativeForce(Vector3.left * force);
	}

	private bool IsLanded()
	{
		return _didJump && GetComponent<Rigidbody>().IsSleeping();
	}

	private bool IsNextStair()
	{
		return !Algorithms.AreSimilar(_yCubePosition, transform.position.y);
	}

	private void ResetCubeState()
	{
		var localPosition = transform.localPosition;
		transform.localPosition = new Vector3(localPosition.x, localPosition.y, 0);

		transform.eulerAngles = new Vector3(0f, -180f, 0f);
	}

	private bool IsCubeFalling()
	{
		return transform.position.y < -15;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out DiamondBehaviour diamond))
		{
			Destroy(diamond.gameObject);

			++CurrentUserInfo.DiamondsAmount;

			this.PlayPickUpDiamondSound();
		}
	}

	protected virtual void OnSuccesfullyLanded()
	{
		_didJump = false;
		CubeLanded?.Invoke();
	}

	protected virtual void OnPlayerLose()
	{
		Destroy(gameObject);
		PlayerLose?.Invoke();
	}
}
