using UnityEngine;

public class PaintingButtonBehaviour : MonoBehaviour
{
	[SerializeField]
	private GameObject _enabledImage;
	[SerializeField]
	private GameObject _disabledImage;

	public void Start()
	{
		UpdateIcon();
	}

	public void OnMouseUpAsButton()
	{
		UpdateIcon();
	}

	private void UpdateIcon()
	{
		var isEnabled = CurrentUserInfo.IsPaintingEnabled;

		_enabledImage.SetActive(isEnabled);
		_disabledImage.SetActive(!isEnabled);
	}
}
