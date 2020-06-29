using Assets.Scripts.Infrastructure;
using UnityEngine;
using UnityEngine.UI;

public class LostMenuBehaviour : MonoBehaviour
{
	[SerializeField]
	private GameBehaviour _game;

	[SerializeField]
	private GameObject _overlay;
	[SerializeField]
	private Text _text;

	public void OnEnable()
	{
		_game.PlayerLost += ShowPannel;
	}

	public void OnDisable()
	{
		_game.PlayerLost -= ShowPannel;
	}

	private void ShowPannel(int score)
	{
		if (_overlay.activeSelf) return;

		AudioService.PlayLostSound(this);
		_text.text = $"You lose with score of {score}";

		_overlay.SetActive(true);
	}
}
