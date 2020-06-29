using System;
using UnityEngine;

public class TimerBehaviour : MonoBehaviour
{
	[SerializeField]
	private GameBehaviour _game;

	[SerializeField]
	private Color _startColor;
	[SerializeField]
	private Color _endColor;

	public event Action Elapsed;

	public void OnEnable()
	{
		_game.CorrectAnswer += ResetTimer;
		_game.PlayerLost += DestroyTimer;
	}

	public void Update()
	{
		UpdateTimer();
		ChangeColor();
	}

	public void OnDisable()
	{
		_game.CorrectAnswer -= ResetTimer;
		_game.PlayerLost -= DestroyTimer;
	}

	public void OnDestroy()
	{
		OnTimerElapsed();
	}

	private void UpdateTimer()
	{
		transform.position -= new Vector3(0.03f, 0, 0);

		if (transform.position.x < -8.5f) Destroy(this.gameObject);
	}

	private void ChangeColor()
	{
		var timerPosition = transform.position.x;
		var spentTime = Mathf.Abs(timerPosition);
		var lerpColorParameter = Mathf.InverseLerp(0, 8.5f, spentTime);

		GetComponent<Renderer>().material.color = Color.Lerp(_startColor, _endColor, lerpColorParameter);
	}

	private void ResetTimer(Color color, int score)
	{
		transform.position = new Vector3(0, transform.position.y, 0);

		GetComponent<Renderer>().material.color = _startColor;
	}
	private void DestroyTimer(int score)
	{
		gameObject.SetActive(false);
	}

	protected void OnTimerElapsed()
	{
		Elapsed?.Invoke();
	}
}
