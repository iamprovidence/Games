using System.Linq;
using UnityEngine;

public static class AudioService
{
	public static void PlayButtonClickedSound(this ButtonAudioBehaviour target)
	{
		if (CurrentUserInfo.IsSoundEnabled)
		{
			target.GetComponent<AudioSource>().PlayEvenIfDestroyed();
		}
	}

	public static void PlayLostSound(this GameBehaviour target)
	{
		if (CurrentUserInfo.IsSoundEnabled)
		{
			target.GetComponent<AudioSource>().Play();
		}
	}

	public static void PlayPickUpDiamondSound(this CubeBehaviour target)
	{
		if (CurrentUserInfo.IsSoundEnabled)
		{
			target.GetComponent<AudioSource>().Play();
		}
	}

	public static void PlayDestroyDiamondSound(this DiamondBehaviour target)
	{
		if (CurrentUserInfo.IsSoundEnabled)
		{
			target.GetComponent<AudioSource>().PlayEvenIfDestroyed();
		}
	}

	public static void PlayBuyCubeSound(this BuyCubeBehaviour target)
	{
		if (CurrentUserInfo.IsSoundEnabled)
		{
			target
				.GetComponents<AudioSource>()
				.Single(a => a.clip.name == "buy_cube")
				.PlayEvenIfDestroyed();
		}
	}

	public static void PlayPurchaseRejectedSound(this BuyCubeBehaviour target)
	{
		if (CurrentUserInfo.IsSoundEnabled)
		{
			target
				.GetComponents<AudioSource>()
				.Single(a => a.clip.name == "purchase_rejected")
				.Play();
		}
	}

	public static void PlayGetFreeCoinsSound(this ButtonAudioBehaviour target)
	{
		if (CurrentUserInfo.IsSoundEnabled)
		{
			target.GetComponent<AudioSource>().Play();
		}
	}

	public static void PlayCurrentCubeChangedSound(this SelectedCubeBehaviour target)
	{
		if (CurrentUserInfo.IsSoundEnabled)
		{
			target.GetComponent<AudioSource>().Play();
		}
	}

	public static void PlayCubeDroppedSound(this StairBehaviour target)
	{
		if (CurrentUserInfo.IsSoundEnabled)
		{
			target.GetComponent<AudioSource>().Play();
		}
	}

	public static void PlayCubeDroppedSound(this AnimationBehaviour target)
	{
		if (CurrentUserInfo.IsSoundEnabled)
		{
			target.GetComponent<AudioSource>().Play();
		}
	}

	private static void PlayEvenIfDestroyed(this AudioSource audioSource)
	{
		var audio = audioSource.clip;

		AudioSource.PlayClipAtPoint(audio, Vector3.zero);
	}
}
