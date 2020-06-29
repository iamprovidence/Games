using UnityEngine;
using RandomGenerator = UnityEngine.Random;

namespace Assets.Scripts.Infrastructure
{
	public static class ColorHelper
	{
		public static Color Random()
		{
			return Color.HSVToRGB(RandomGenerator.value, 0.8f, 0.8f);
		}

		public static Color GetSimilar(Color color, int complexity)
		{
			return new Color(GetRandomColorComponent(color.r), GetRandomColorComponent(color.g), GetRandomColorComponent(color.b));

			float GetRandomColorComponent(float currentColorComponent)
			{
				var maxColorCoefficient = GetComplexityCoefficient();
				var randomCoefficient = RandomGenerator.Range(0.01f, maxColorCoefficient);

				return Mathf.Min(currentColorComponent + randomCoefficient, 1f);
			}

			float GetComplexityCoefficient()
			{
				if (complexity < 3) return 0.3f;
				if (complexity < 5) return 0.2f;
				if (complexity < 8) return 0.1f;

				return 0.05f;
			}
		}
	}
}
