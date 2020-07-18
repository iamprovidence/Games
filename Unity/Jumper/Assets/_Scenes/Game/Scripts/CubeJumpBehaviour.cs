using System;
using UnityEngine;

public class CubeJumpBehaviour : MonoBehaviour
{
	public event Action Pressed;
	public event Action<float> Released;

	private float _startTime;

	private void OnMouseDown()
	{
		Pressed?.Invoke();
		_startTime = Time.time;
	}

	private void OnMouseUp()
	{
		var force = GetForce();
		Released?.Invoke(force);
	}

	private float GetForce()
	{
		float difference = Time.time - _startTime;
		float force = difference < 3f ? 190f * difference : 300f;

		return Mathf.Max(60f, force);
	}
}
