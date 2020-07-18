using UnityEngine;
using UnityEngine.UI;

public class UpdateScoreBehaviour : MonoBehaviour
{
	[SerializeField]
	private CubeBehaviour _cubeBehaviour;
	[SerializeField]
	private Text _text;

	private int _score;

	private void OnEnable()
	{
		_cubeBehaviour.CubeLanded += IncreaseScore;
	}

	private void OnDisable()
	{
		_cubeBehaviour.CubeLanded += IncreaseScore;
	}

	private void IncreaseScore()
	{
		++_score;

		_text.text = _score.ToString();
	}
}
