using System.Collections;
using UnityEngine;

public class MoveStairsBehaviour : MonoBehaviour
{
	[SerializeField]
	private CubeBehaviour _cubeBehaviour;

	[SerializeField]
	private float _spped = 5;
	[SerializeField]
	private Vector3 _stairOffset;
	private Vector3 _target;

	public void OnEnable()
	{
		SetUpTarget();
		_cubeBehaviour.CubeLanded += MoveStairs;
	}

	public void OnDisable()
	{
		_cubeBehaviour.CubeLanded -= MoveStairs;
	}

	private void MoveStairs()
	{
		StartCoroutine(MoveStairsCoroutine());
	}

	private IEnumerator MoveStairsCoroutine()
	{
		while (transform.position != _target)
		{
			transform.position = Vector3.MoveTowards(transform.position, _target, Time.deltaTime * _spped);
			yield return null;
		}

		SetUpTarget();
	}

	private void SetUpTarget()
	{
		_target = transform.position + _stairOffset;
	}
}
