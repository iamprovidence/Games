using System;
using UnityEngine;

public class ColorOptionBehaviour : MonoBehaviour
{
	public event Action<Color> ColorSelected;

	public void SetColor(Color color)
	{
		gameObject.GetComponent<Renderer>().material.color = color;
	}

	public void OnMouseDown()
	{
		OnColorSelected(GetComponent<Renderer>().material.color);
	}

	protected virtual void OnColorSelected(Color color)
	{
		ColorSelected?.Invoke(color);
	}
}
