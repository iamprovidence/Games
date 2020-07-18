using UnityEngine;

public class GameBehaviour : MonoBehaviour
{
	[SerializeField]
	private int _changeComplexityFrequency = 5;
	[SerializeField]
	private CubeBehaviour _cubeBehaviour;
	[SerializeField]
	private FlowBehaviour _loseButtons;
	[SerializeField]
	private float _losePanelPositionY = 0;
	[SerializeField]
	private float _losePanelSpped = 10;

	private int _score;
	private bool _isEasyMode = true;

	private void OnEnable()
	{
		_cubeBehaviour.CubeLanded += CountScore;
		_cubeBehaviour.PlayerLose += UpdateMaxScore;
		_cubeBehaviour.PlayerLose += ShowLosePanel;
	}

	private void OnDisable()
	{
		_cubeBehaviour.PlayerLose -= CountScore;
		_cubeBehaviour.PlayerLose -= UpdateMaxScore;
		_cubeBehaviour.PlayerLose -= ShowLosePanel;
	}

	private void UpdateMaxScore()
	{
		CurrentUserInfo.MaxScore = _score;
	}

	private void ShowLosePanel()
	{
		_loseButtons.FlowTo(_losePanelPositionY, _losePanelSpped);

		this.PlayLostSound();
	}

	private void CountScore()
	{
		++_score;

		if (_score % _changeComplexityFrequency == 0)
		{
			ToggleComplexity();
		}
	}

	private void ToggleComplexity()
	{
		if (_isEasyMode) MakeGameHarder();
		else			 MakeGameEasier();

		_isEasyMode = !_isEasyMode;
	}

	private void MakeGameHarder()
	{
		Camera.main.GetComponent<CameraBehaviour>().SetHardView();
	}

	private void MakeGameEasier()
	{
		Camera.main.GetComponent<CameraBehaviour>().SetDefaultView();
	}
}
