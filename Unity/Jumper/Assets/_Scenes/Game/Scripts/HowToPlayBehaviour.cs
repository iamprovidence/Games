using UnityEngine;

public class HowToPlayBehaviour : MonoBehaviour
{
	[SerializeField]
	private CubeBehaviour _cubeBehaviour;

	private void OnEnable()
	{
		_cubeBehaviour.CubeLanded += HideHint;
	}

	private void OnDisable()
	{
		_cubeBehaviour.CubeLanded += HideHint;
	}

	private void HideHint()
	{
		gameObject.SetActive(false);
	}
}
