using System.Collections;
using UnityEngine;

public class FlowBehaviour : MonoBehaviour
{
	[SerializeField]
	private float _endPositionY;
	[SerializeField]
	private float _speed;

	private void Start()
	{
		StartCoroutine(FlowToCoroutine(_endPositionY, _speed));
	}

	public void FlowTo(float endPositionY, float speedY)
	{
		StartCoroutine(FlowToCoroutine(endPositionY, speedY));
	}

	private IEnumerator FlowToCoroutine(float endPositionY, float speedY)
	{
		while(!MoveNextStep(endPositionY, speedY))
		{
			yield return null;
		}
	}

	private bool MoveNextStep(float endPositionY, float speedY)
	{
		var transform = GetComponent<RectTransform>();
		var currentPositionY = transform.offsetMin.y;

		if (!Algorithms.AreSimilar(currentPositionY, endPositionY))
		{
			transform.offsetMin += new Vector2(-transform.offsetMin.x, speedY);
			transform.offsetMax += new Vector2(-transform.offsetMax.x, speedY);

			return false;
		}

		return true;
	}
}
