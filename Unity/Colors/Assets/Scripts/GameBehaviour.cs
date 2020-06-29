using Assets.Scripts.Infrastructure;
using System;
using UnityEngine;

public class GameBehaviour : MonoBehaviour
{
	[SerializeField]
	private TimerBehaviour _timer;

	[SerializeField]
	private MainColorBehaviour _mainColor;
	[SerializeField]
	private ColorOptionsBehaviour _colorOptions;

	private int _currentScore;

	public event Action<Color, int> CorrectAnswer;
	public event Action<int> PlayerLost;

	public void OnEnable()
	{
		_timer.Elapsed += PlayerLose;
		_colorOptions.ColorSelected += ColorSelected;

		OnPlayerCorrect(ColorHelper.Random(), _currentScore);
	}

	public void OnDisable()
	{
		_timer.Elapsed -= PlayerLose;
		_colorOptions.ColorSelected -= ColorSelected;
	}

	private void ColorSelected(Color color)
	{
		if (IsCorrectColor(color))
		{
			NextRound();
		}
		else
		{
			PlayerLose();
		}
	}

	private bool IsCorrectColor(Color color)
	{
		return _mainColor.CurrentColor == color;
	}

	private void NextRound()
	{
		++_currentScore;
		AudioService.PlayCorrectAnswerSound(this);
		OnPlayerCorrect(ColorHelper.Random(), _currentScore);
	}

	private void PlayerLose()
	{
		_colorOptions.ColorSelected -= ColorSelected;
		CurrentUserInfo.MaxScore = _currentScore;
		OnPlayerLost(_currentScore);
	}

	protected virtual void OnPlayerCorrect(Color nextColor, int score)
	{
		CorrectAnswer?.Invoke(nextColor, score);
	}

	protected virtual void OnPlayerLost(int score)
	{
		PlayerLost?.Invoke(score);
	}
}
