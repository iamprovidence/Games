using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Algorithms
{
	public static bool AreSimilar(float a, float b)
	{
		return Mathf.Round(a) == Mathf.Round(b);
	}

	public static IEnumerable<float> Sequence(float start, float stop, float step)
	{
		float current = start;

		while (step >= 0 ? stop > current : stop < current)
		{
			yield return current;
			current += step;
		}
	}

	public static float GetClosestNumber(float value, IEnumerable<float> numbers)
	{
		return numbers.Aggregate((x, y) => Mathf.Abs(x - value) < Mathf.Abs(y - value) ? x : y);
	}
}
