using System.Collections.Generic;
using UnityEngine;

public class CubesColors : MonoBehaviour
{
	public static readonly string DefaultCube = "DefaultCube";
	public static readonly string FreeCube = "FreeCube";
	public static readonly string RedCube = "RedCube";
	public static readonly string GoldCube = "GoldCube";
	public static readonly string GreenCube = "GreenCube";

	public static Color GetColor(string cubeKey) => _cubeColors[cubeKey];

	private static readonly IDictionary<string, Color> _cubeColors = new Dictionary<string, Color>()
	{

		[DefaultCube] = Color.HSVToRGB(0f, 0f, 1f),
		[FreeCube] = Color.HSVToRGB(0.55f, 0.85f, 0.8f),
		[RedCube] = Color.HSVToRGB(0.027f, 0.85f, 0.8f),
		[GoldCube] = Color.HSVToRGB(0.13f, 1f, 0.9f),
		[GreenCube] = Color.HSVToRGB(0.39f, 0.9f, 0.85f),
	};
}
