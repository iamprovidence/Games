using UnityEngine;
using UnityEngine.UI;

public class UpdatePriceBehaviour : MonoBehaviour
{
	[SerializeField]
	private Text _priceText;
	[SerializeField]
	private SelectedCubeBehaviour _selectedCube;
	[SerializeField]
	private BuyCubeBehaviour _boughtCube;

	private void OnEnable()
	{
		_selectedCube.CubeSelected += UpdatePrice;
		_selectedCube.CubeSelected += ShowHidePrice;

		_boughtCube.CubeBought += UpdatePrice;
		_boughtCube.CubeBought += ShowHidePrice;
	}


	private void OnDisable()
	{
		_selectedCube.CubeSelected -= UpdatePrice;
		_selectedCube.CubeSelected -= ShowHidePrice;

		_boughtCube.CubeBought += UpdatePrice;
		_boughtCube.CubeBought += ShowHidePrice;
	}

	private void ShowHidePrice(SellCubeBehaviour currentCube)
	{
		var isCubeAvailable = currentCube.IsAvailable;

		for (int i = 0; i < transform.childCount; ++i)
		{
			var child = transform.GetChild(i);
			child.gameObject.SetActive(!isCubeAvailable);
		}
	}

	private void UpdatePrice(SellCubeBehaviour currentCube)
	{
		_priceText.text = currentCube.Price.ToString();
	}
}
