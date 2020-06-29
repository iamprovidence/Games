using UnityEngine;

public class MainColorBehaviour : MonoBehaviour
{
	[SerializeField]
	private GameBehaviour _game;
	[SerializeField]
	private GameObject _mainColorObject;

	public Color CurrentColor
	{
		get
		{
			return _mainColorObject.GetComponent<Renderer>().material.color;
		}
		private set
		{
			_mainColorObject.GetComponent<Renderer>().material.color = value;
		}
	}

	public void OnEnable()
	{
		_game.CorrectAnswer += ChangeColor;
	}

	public void OnDisable()
	{
		_game.CorrectAnswer -= ChangeColor;
	}

	private void ChangeColor(Color color, int score)
	{
		CurrentColor = color;
	}
}
