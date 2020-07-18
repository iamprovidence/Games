using UnityEngine;

public class ShopButtonBehaviour : MonoBehaviour
{
	[SerializeField]
	private GameObject _buyButton;
	[SerializeField]
	private GameObject _selectCubeButton;
	[SerializeField]
	private SelectedCubeBehaviour _selectedCube;
	[SerializeField]
	private BuyCubeBehaviour _boughtCube;

	private void OnEnable()
	{
		_selectedCube.CubeSelected += UpdateButtons;
		_boughtCube.CubeBought += UpdateButtons;
	}

	private void OnDisable()
	{
		_selectedCube.CubeSelected -= UpdateButtons;
		_boughtCube.CubeBought -= UpdateButtons;
	}

	private void UpdateButtons(SellCubeBehaviour currentCube)
	{
		var isCubeAvailabe = currentCube.IsAvailable;

		_buyButton.SetActive(!isCubeAvailabe);
		_selectCubeButton.SetActive(isCubeAvailabe);
	}
}
