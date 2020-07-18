using System;
using UnityEngine;

public class BuyCubeBehaviour : MonoBehaviour
{
	[SerializeField]
	private SelectedCubeBehaviour _selectedCube;

	private SellCubeBehaviour _cubeToBuy;

	public event Action<SellCubeBehaviour> CubeBought;

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
		_cubeToBuy = currentCube;
	}

	private void OnMouseUpAsButton()
	{
		TryBuyCube();
	}

	private void TryBuyCube()
	{
		if (CurrentUserInfo.DiamondsAmount >= _cubeToBuy.Price)
		{
			CurrentUserInfo.BuyCube(_cubeToBuy.gameObject.name);
			CurrentUserInfo.DiamondsAmount -= _cubeToBuy.Price;

			this.PlayBuyCubeSound();

			OnCubeBought(_cubeToBuy);
		}
		else
		{
			this.PlayPurchaseRejectedSound();
		}
	}

	protected void OnCubeBought(SellCubeBehaviour boughtCube)
	{
		CubeBought?.Invoke(boughtCube);
	}
}
