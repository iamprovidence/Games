using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStarBehaviour : MonoBehaviour
{
	[SerializeField]
	private float _spawnIntervalInSeconds = 1.5f;
	[SerializeField]
	private float _destroyIntervalInSeconds = 1.5f;
	[SerializeField]
	private GameObject _star;

	private Queue<GameObject> _starQueue = new Queue<GameObject>(10);

	public void Start()
	{
		StartCoroutine(DestroyStar());
		StartCoroutine(SpawStar());
	}

	private IEnumerator SpawStar()
	{
		while (true)
		{
			var position = GetRandomPosition();

			var createdStar = Instantiate(_star, position, Quaternion.Euler(0, 0, Random.Range(0, 360f)));

			_starQueue.Enqueue(createdStar);

			yield return new WaitForSeconds(_spawnIntervalInSeconds);
		}
	}
	private Vector3 GetRandomPosition()
	{
		var x = Random.Range(0, Screen.width);
		var y = Random.Range(0, Screen.height);
		var z = Camera.main.farClipPlane / 2;

		var position = new Vector3(x, y, z);

		return Camera.main.ScreenToWorldPoint(position);
	}

	private IEnumerator DestroyStar()
	{
		while (true)
		{
			var starToDestroy = TryGetStar();

			TryDestroy(starToDestroy);

			yield return new WaitForSeconds(_destroyIntervalInSeconds);
		}
	}

	private GameObject TryGetStar()
	{
		try
		{
			return _starQueue.Dequeue();
		}
		catch
		{
			return null;
		}
	}

	private void TryDestroy(GameObject gameObject)
	{
		if (gameObject != null) Destroy(gameObject);
	}
}
