using UnityEngine;

public class CubeAnimationBehaviour : MonoBehaviour
{
	[SerializeField]
	private float _moveSpeed;
	[SerializeField]
	private float _rotateSpeed;
	[SerializeField]
	private float _changeColorSpeed;
	[SerializeField]
	private float _height;

	private Vector3 _startPosition;

	public void Start()
	{
		_startPosition = transform.position;
	}

	public void Update()
	{
		MoveUpDown();
		Rotate();
		ChangeColor();
	}

	private void MoveUpDown()
	{
		float offsetY = Mathf.Sin(Time.time * _moveSpeed) * _height;

		transform.position = new Vector3(_startPosition.x, _startPosition.y + offsetY, _startPosition.z);
	}

	private void Rotate()
	{
		transform.Rotate(Vector3.up * _rotateSpeed);
	}

	private void ChangeColor()
	{
		var hue = Mathf.Abs(Mathf.Sin(Time.time * _changeColorSpeed));

		GetComponent<Renderer>().material.color = Color.HSVToRGB(hue, 0.8f, 0.8f);
	}
}
