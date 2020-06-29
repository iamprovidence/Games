using Assets.Scripts.Infrastructure;
using UnityEngine;

public class SoundButtonBehaviour : MonoBehaviour
{
	[SerializeField]
	private GameObject _soundEnabledImage;
	[SerializeField]
	private GameObject _soundDisabledImage;

	public void Start()
	{
		UpdateSoundIcon();
	}

	public void OnMouseUpAsButton()
	{
		UpdateSoundIcon();
	}

	private void UpdateSoundIcon()
	{
		var isSoundEnabled = CurrentUserInfo.IsSoundEnabled;

		_soundEnabledImage.SetActive(isSoundEnabled);
		_soundDisabledImage.SetActive(!isSoundEnabled);
	}
}
