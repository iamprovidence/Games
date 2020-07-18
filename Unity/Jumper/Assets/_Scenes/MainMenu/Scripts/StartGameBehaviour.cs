using UnityEngine;
using UnityEngine.UI;

public class StartGameBehaviour : MonoBehaviour
{
	[SerializeField]
	private Animation _animateCube;
	[SerializeField]
	private Animation _startGameAnimation;

	[SerializeField]
	private Text _gameName;
	[SerializeField]
	private Text _startGameText;

	[SerializeField]
	private FlowBehaviour _buttons;

	private void OnMouseUpAsButton()
	{
		TryStartAnimation();
	}

	private void TryStartAnimation()
	{
		if (_startGameAnimation.isPlaying) return;

		_animateCube.Stop();
		_startGameAnimation.Play();

		_startGameText.enabled = false;
		_gameName.text = "0";
		_buttons.FlowTo(endPositionY: -250f, speedY: -5f);
	}
}
