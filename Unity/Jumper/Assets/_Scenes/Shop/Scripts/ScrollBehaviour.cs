using UnityEngine;

public class ScrollBehaviour : MonoBehaviour
{
	[SerializeField]
	private GameObject _cubes;
	[SerializeField]
	private float _startCubesPositionX;
	[SerializeField]
	private float _endCubesPositionX;
	[SerializeField]
	private float _cubesDistance;
	[SerializeField]
	private float _autoScrollSpeed = 10f;

	private Vector3 _offset;

	private void Update()
	{
		ScrollToClosestCube();
	}

	private void OnMouseDown()
	{
		Cursor.visible = false;

		var userStarScrollPosition = new Vector3(Input.mousePosition.x, 0, 0);
		_offset = _cubes.transform.position - Camera.main.ScreenToWorldPoint(userStarScrollPosition);
	}

	private void OnMouseDrag()
	{
		var userCurrentScrollPosition = new Vector3(Input.mousePosition.x, 0, 0);

		_cubes.transform.position = Camera.main.ScreenToWorldPoint(userCurrentScrollPosition) + _offset;
	}

	private void OnMouseUp()
	{
		Cursor.visible = true;
	}

	private void ScrollToClosestCube()
	{
		var autoScrollSpeed = Time.deltaTime * _autoScrollSpeed;
		var cubesPosition = _cubes.transform.position;

		var allowedPositionX = Algorithms.Sequence(_startCubesPositionX, _endCubesPositionX, _cubesDistance);
		var closestCubePositionX = Algorithms.GetClosestNumber(cubesPosition.x, allowedPositionX);

		var targetPosition = new Vector3(closestCubePositionX, cubesPosition.y, cubesPosition.z);
		_cubes.transform.position = Vector3.MoveTowards(cubesPosition, targetPosition, autoScrollSpeed);
	}
}
