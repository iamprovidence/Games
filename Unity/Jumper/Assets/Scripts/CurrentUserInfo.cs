using System;
using UnityEngine;

public static class CurrentUserInfo
{
	static CurrentUserInfo()
	{
		IsPaintingEnabled = true;
		IsSoundEnabled = true;

		BuyCube(CubesColors.DefaultCube);
		BuyCube(CubesColors.FreeCube);
	}

	#region MaxScore
	public static int MaxScore
	{
		get
		{
			return PlayerPrefs.GetInt("MaxScore");
		}
		set
		{
			if (value > MaxScore)
			{
				PlayerPrefs.SetInt("MaxScore", value);
			}
		}
	}
	#endregion

	#region Diamonds
	public static event Action<int> DiamondsAmountChange;

	public static int DiamondsAmount
	{
		get
		{
			return PlayerPrefs.GetInt("DiamondsAmount");
		}
		set
		{
			PlayerPrefs.SetInt("DiamondsAmount", value);
			DiamondsAmountChange?.Invoke(value);
		}
	}
	#endregion

	#region Sound
	public static bool IsSoundEnabled
	{
		get
		{
			return PlayerPrefs.GetString("Music") == "yes";
		}
		set
		{
			var isEnabled = value ? "yes" : "no";
			PlayerPrefs.SetString("Music", isEnabled);
		}
	}

	public static void ToggleSound()
	{
		IsSoundEnabled = !IsSoundEnabled;
	}
	#endregion

	#region Painting
	public static bool IsPaintingEnabled
	{
		get
		{
			return PlayerPrefs.GetString("Painting") == "yes";
		}
		set
		{
			var isEnabled = value ? "yes" : "no";
			PlayerPrefs.SetString("Painting", isEnabled);
		}
	}

	public static void TogglePainting()
	{
		IsPaintingEnabled = !IsPaintingEnabled;
	}
	#endregion

	#region AvailableCubes
	public static bool IsCubeAvailable(string cubeName)
	{
		return PlayerPrefs.GetString(key: cubeName, defaultValue: "no") == "yes";
	}

	public static void BuyCube(string cubeName)
	{
		PlayerPrefs.SetString(key: cubeName, value: "yes");
	}

	public static void ClearAvailableCubes()
	{
		PlayerPrefs.DeleteKey(CubesColors.RedCube);
		PlayerPrefs.DeleteKey(CubesColors.GoldCube);
		PlayerPrefs.DeleteKey(CubesColors.GreenCube);
	}
	#endregion

	#region SelectedCube
	public static string SelectedCube
	{
		get
		{
			var selectedCube = PlayerPrefs.GetString("SelectedCube");

			return string.IsNullOrWhiteSpace(selectedCube) ? CubesColors.DefaultCube : selectedCube;
		}
		set
		{
			PlayerPrefs.SetString("SelectedCube", value);
		}
	}

	public static bool IsCurrentColorSelected(string cubeName)
	{
		return cubeName == SelectedCube;
	}

	public static Color GetSelectedCubeColor()
	{
		return CubesColors.GetColor(SelectedCube);
	}
	#endregion
}