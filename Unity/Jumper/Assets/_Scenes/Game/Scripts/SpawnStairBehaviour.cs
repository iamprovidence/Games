using System.Collections.Generic;
using UnityEngine;

public class SpawnStairBehaviour : MonoBehaviour
{
	[SerializeField]
	private CubeBehaviour _cubeBehaviour;

	[SerializeField]
	private float _chanceToCreateDiamondInPercent = 0.15f;
	[SerializeField]
	private GameObject _diamondPrefab;

	[SerializeField]
	private int _maxElementAmount = 4;
	[SerializeField]
	private Vector3 _createStairOffset = new Vector3(3f, -4.5f, 0f);
	[SerializeField]
	private GameObject _stairPrefab;
	private readonly LinkedList<GameObject> _stairs = new LinkedList<GameObject>();

	private void Start()
	{
		var firstStair = GameObject.Find("Stair");
		_stairs.AddFirst(firstStair);

		UpdateStairsAmount();
	}

	private void OnEnable()
	{
		_cubeBehaviour.CubeLanded += UpdateStairsAmount;
	}

	private void OnDisable()
	{
		_cubeBehaviour.CubeLanded += UpdateStairsAmount;
	}

	private void UpdateStairsAmount()
	{
		var createdStair = CreateNewStair();
		_stairs.AddLast(createdStair);

		if (Random.value < _chanceToCreateDiamondInPercent)
		{
			CreateNewDiamond(createdStair);
		}

		if (_stairs.Count > _maxElementAmount)
		{
			var stairToDestroy = PopFirstStair();
			Destroy(stairToDestroy);
		}
	}

	private GameObject CreateNewStair()
	{
		var createdStair = Instantiate(_stairPrefab, GetNewStairPosition(), GetNewStairRotation(), parent: transform);

		var stairScale = createdStair.transform.localScale;
		createdStair.transform.localScale = new Vector3(GetNewStairWidth(), stairScale.y, stairScale.z);

		return createdStair;
	}

	private GameObject CreateNewDiamond(GameObject stair)
	{
		var diamondPosition = new Vector3(0, 1.3f, 0);

		var createdDiamond = Instantiate(_diamondPrefab, diamondPosition, Quaternion.identity);
		createdDiamond.transform.SetParent(stair.transform, worldPositionStays: false);

		return createdDiamond;
	}

	private Vector3 GetNewStairPosition()
	{
		var lastStairPosition = GetLastStair().transform.position;

		return lastStairPosition + _createStairOffset;
	}

	private GameObject PopFirstStair()
	{
		var firstStair = _stairs.First.Value;
		_stairs.RemoveFirst();
		return firstStair;
	}

	private GameObject GetLastStair()
	{
		return _stairs?.Last?.Value ?? _stairPrefab;
	}

	private Quaternion GetNewStairRotation()
	{
		return _stairPrefab.transform.rotation;
	}

	private float GetNewStairWidth()
	{
		return Random.Range(0, 100) > 80 ? Random.Range(1.6f, 2.4f) : Random.Range(1.2f, 1.8f);
	}
}
