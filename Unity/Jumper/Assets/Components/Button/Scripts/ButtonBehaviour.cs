using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{
	private static readonly string GuthubUrl = "https://github.com/iamprovidence";
	private static readonly float ScaledFuctor = 1.1f;

	[SerializeField]
	private Sprite _defaultSprite;
	[SerializeField]
	private Sprite _pressedSprite;

	public void OnMouseUp()
	{
		GetComponent<SpriteRenderer>().sprite = _defaultSprite;

		transform.localScale /= ScaledFuctor;
	}

	public void OnMouseDown()
	{
		GetComponent<SpriteRenderer>().sprite = _pressedSprite;

		transform.localScale *= ScaledFuctor;
	}

	public void OnMouseUpAsButton()
	{
		var buttonName = gameObject.name;

		switch (buttonName)
		{
			case "Home": SceneManager.LoadSceneAsync("MainMenu"); break;
			case "Restart": SceneManager.LoadSceneAsync("Game"); break;
			case "Github": Application.OpenURL(GuthubUrl); break;
			case "Settings": ToggleInnerMenuItems(); break;
			case "Sound": CurrentUserInfo.ToggleSound(); break;
			case "Painting": CurrentUserInfo.TogglePainting(); break;
			case "Shop": SceneManager.LoadSceneAsync("Shop"); break;
			case "ResetAll": CurrentUserInfo.ClearAvailableCubes(); break;
			case "FreeCoins": AddFreeDiamonds(); break;
			case "Buy": /* Implemented in shop */; break;
			case "SelectCube": /* Implemented in shop */; break;

			default: throw new InvalidOperationException($"Wrong button name [{buttonName}]");
		}
	}

	private void ToggleInnerMenuItems()
	{
		for (int i = 0; i < transform.childCount; ++i)
		{
			var child = transform.GetChild(i);

			if (child.TryGetComponent(out ButtonBehaviour button))
			{
				var isChildActive = child.gameObject.activeSelf;

				child.gameObject.SetActive(!isChildActive);
			}
		}
	}

	private void AddFreeDiamonds()
	{
		CurrentUserInfo.DiamondsAmount += 5;
	}
}
