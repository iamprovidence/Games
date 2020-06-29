using Assets.Scripts.Infrastructure;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{
	[SerializeField]
	private Sprite _mainColor;
	[SerializeField]
	private Sprite _pressedColor;

	private SpriteRenderer _spriteRendered;

	public void Start()
	{
		_spriteRendered = GetComponent<SpriteRenderer>();
	}

	public void OnMouseDown()
	{
		_spriteRendered.sprite = _pressedColor;
	}

	public void OnMouseUp()
	{
		_spriteRendered.sprite = _mainColor;
	}

	public void OnMouseUpAsButton()
	{
		AudioService.PlayButtonClickedSound(this);

		switch (gameObject.name)
		{
			case "Play": SceneManager.LoadScene("Play"); break;
			case "Exit": SceneManager.LoadScene("Menu"); break;
			case "Replay": SceneManager.LoadScene("Play"); break;
			case "Sound": CurrentUserInfo.ToggleSound(); break;
			case "Github": Application.OpenURL("https://github.com/iamprovidence"); break;
		}
	}
}
