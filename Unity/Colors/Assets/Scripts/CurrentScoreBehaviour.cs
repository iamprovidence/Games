using UnityEngine;
using UnityEngine.UI;

public class CurrentScoreBehaviour : MonoBehaviour
{
	[SerializeField]
	private GameBehaviour _game;
	[SerializeField]
	private Text _text;

	public void OnEnable()
	{
		_game.CorrectAnswer += UpdateScore;
	}

	public void OnDisable()
	{
		_game.CorrectAnswer -= UpdateScore;
	}

	private void UpdateScore(Color color, int score)
	{
		_text.text = score.ToString();
	}

}
