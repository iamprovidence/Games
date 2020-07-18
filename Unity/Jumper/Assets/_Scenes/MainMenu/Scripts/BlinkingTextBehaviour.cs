using UnityEngine;
using UnityEngine.UI;

public class BlinkingTextBehaviour : MonoBehaviour
{
	private Text _text;
	private Outline _outline;

	private void Start()
	{
		_text = GetComponent<Text>();
		_outline = GetComponent<Outline>();
	}
	
	private void Update()
	{
		_text.color = ChangeColorAlpha(_text.color);
		_outline.effectColor = ChangeColorAlpha(_outline.effectColor, -0.3f);
	}

	private Color ChangeColorAlpha(Color color, float offset = 0)
	{
		var alpha = Mathf.PingPong(Time.time / 2.5f, 1) + offset;
		return new Color(color.r, color.g, color.b, alpha);
	}
}
