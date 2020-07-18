using UnityEngine;

public class StarAnimationBehaviour : MonoBehaviour
{
	[SerializeField]
	private float _movementSpeed = 0.1f;
	private SpriteRenderer _star;

	private void Start()
	{
		_star = GetComponent<SpriteRenderer>();
	}

	private void Update()
	{
		_star.color = ChangeColorAlpha(_star.color);

		Move();
	}

	private Color ChangeColorAlpha(Color color, float offset = 0)
	{
		var alpha = Mathf.PingPong(Time.time / 1.5f, 1) + offset;
		return new Color(color.r, color.g, color.b, alpha);
	}

	private void Move()
	{
		transform.position += transform.up * Time.deltaTime * _movementSpeed;
	}
}
