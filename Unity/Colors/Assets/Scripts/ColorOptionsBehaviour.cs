using Assets.Scripts.Infrastructure;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ColorOptionsBehaviour : MonoBehaviour
{
	[SerializeField]
	private GameBehaviour _game;

	[SerializeField]
	private ColorOptionBehaviour[] _colorOptions;

	public event Action<Color> ColorSelected;

	public void OnEnable()
	{
		_game.CorrectAnswer += ChangeColor;
		foreach (var colorOption in _colorOptions)
		{
			colorOption.ColorSelected += OnColorSelected;
		}
	}

	public void OnDisable()
	{
		_game.CorrectAnswer -= ChangeColor;
		foreach (var colorOption in _colorOptions)
		{
			colorOption.ColorSelected -= OnColorSelected;
		}
	}

	private void ChangeColor(Color color, int score)
	{
		foreach (var colorOption in _colorOptions)
		{
			var similarColor = ColorHelper.GetSimilar(color, complexity: score);
			colorOption.SetColor(similarColor);
		}

		var randomOptionValue = Random.Range(0, _colorOptions.Length);
		_colorOptions[randomOptionValue].SetColor(color);
	}

	protected virtual void OnColorSelected(Color color)
	{
		ColorSelected?.Invoke(color);
	}
}
