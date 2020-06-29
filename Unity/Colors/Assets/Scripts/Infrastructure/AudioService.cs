
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
	public static class AudioService
	{
		public static void PlayCorrectAnswerSound(GameBehaviour target)
		{
			if (CurrentUserInfo.IsSoundEnabled)
			{
				target.GetComponent<AudioSource>().Play();
			}
		}

		public static void PlayButtonClickedSound(ButtonBehaviour target)
		{
			if (CurrentUserInfo.IsSoundEnabled)
			{
				target.GetComponent<AudioSource>().Play();
			}
		}

		public static void PlayLostSound(LostMenuBehaviour target)
		{
			if (CurrentUserInfo.IsSoundEnabled)
			{
				target.GetComponent<AudioSource>().Play();
			}
		}
	}
}
