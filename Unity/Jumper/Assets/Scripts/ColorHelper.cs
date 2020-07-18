using UnityEngine;
using RandomGenerator = UnityEngine.Random;

public static class ColorHelper
{
	public static Color Random()
	{
		return Color.HSVToRGB(RandomGenerator.value, 0.8f, 0.9f);
	}

	public static bool AreSimilar(Color color1, Color color2, float allowedThreshold = 0.1f)
	{
		return
			AreComponentSimilar(color1.r, color2.r) &&
			AreComponentSimilar(color1.g, color2.g) &&
			AreComponentSimilar(color1.b, color2.b);

		bool AreComponentSimilar(float c1, float c2)
		{
			return Mathf.Abs(c1 - c2) < allowedThreshold;
		}
	}

	public static Color ShiftColor(Color color, float colorIncrement)
	{
		Color.RGBToHSV(color, out float h, out float s, out float v);

		h = (h + colorIncrement) % 1f;
		s = RandomGenerator.Range(0.5f, 0.9f);
		v = RandomGenerator.Range(0.7f, 0.9f);

		return Color.HSVToRGB(h, s, v);
	}
}
