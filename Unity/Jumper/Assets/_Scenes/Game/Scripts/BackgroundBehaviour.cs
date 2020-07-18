using System.Collections;
using UnityEngine;

public class BackgroundBehaviour : MonoBehaviour
{
	private static readonly string COLOR_ONE = "_Color1";
	private static readonly string COLOR_TWO = "_Color2";

	private Material _gradientShader;

	[SerializeField]
	private float _lerpSpeed = 0.5f;
	[SerializeField]
	private float _colorIncrement = 0.2f;

	private Color _currentColor1;
	private Color _currentColor2;

	[SerializeField]
	private CubeBehaviour _cubeBehaviour;

	private void OnEnable()
	{
		_cubeBehaviour.CubeLanded += ChangeBackground;
	}

	private void OnDisable()
	{
		_cubeBehaviour.PlayerLose -= ChangeBackground;
	}

	private void Start()
	{
		_gradientShader = GetComponent<Skybox>().material;

		_currentColor1 = _gradientShader.GetColor(COLOR_ONE);
		_currentColor2 = _gradientShader.GetColor(COLOR_TWO);
	}

	private void ChangeBackground()
	{
		StartCoroutine(ChangeBackgroundCoroutine());
	}

	private IEnumerator ChangeBackgroundCoroutine()
	{
		var targetColor1 = ColorHelper.ShiftColor(_currentColor1, _colorIncrement);
		var targetColor2 = _currentColor1;

		while (!ColorHelper.AreSimilar(_currentColor1, targetColor1))
		{
			_currentColor1 = Color.Lerp(_currentColor1, targetColor1, Time.deltaTime * _lerpSpeed);
			_currentColor2 = Color.Lerp(_currentColor2, targetColor2, Time.deltaTime * _lerpSpeed);

			_gradientShader.SetColor(COLOR_ONE, _currentColor1);
			_gradientShader.SetColor(COLOR_TWO, _currentColor2);

			yield return null;
		}
	}
}
