using UnityEngine;

public class SelectCubeBehaviour : MonoBehaviour
{
	[SerializeField]
	private SelectedCubeBehaviour _selectedCube;

	private SellCubeBehaviour _cubeToSelect;

	private void OnEnable()
	{
		_selectedCube.CubeSelected += UpdateCurrentCube;
	}

	private void OnDisable()
	{
		_selectedCube.CubeSelected -= UpdateCurrentCube;
	}

	private void UpdateCurrentCube(SellCubeBehaviour currentCube)
	{
		_cubeToSelect = currentCube;

		HideButton();
	}

	private void OnMouseUpAsButton()
	{
		SelectCubeAsCurrent();
	}

	private void SelectCubeAsCurrent()
	{
		CurrentUserInfo.SelectedCube = _cubeToSelect.Name;

		HideButton();
	}

	private void HideButton()
	{
		var hideSelectButton = _cubeToSelect.IsSelected || !_cubeToSelect.IsAvailable;

		gameObject.SetActive(!hideSelectButton);
	}
}
