using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
	public static class CurrentUserInfo
	{
		public static int MaxScore
		{
			get
			{
				return PlayerPrefs.GetInt("Score");
			}
			set
			{
				if (value > MaxScore)
				{
					PlayerPrefs.SetInt("Score", value);
				}
			}
		}

		public static bool IsSoundEnabled
		{
			get
			{
				return PlayerPrefs.GetString("Music") == "yes";
			}
			set
			{
				var isSoundEnabled = value ? "yes" : "no";
				PlayerPrefs.SetString("Music", isSoundEnabled);
			}
		}

		public static void ToggleSound()
		{
			IsSoundEnabled = !IsSoundEnabled;
		}
	}
}
