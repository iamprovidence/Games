using System;
using UnityEngine;

public class SelectedCubeBehaviour : MonoBehaviour
{
	[SerializeField]
	private float _scaleFuctor;

	private void OnTriggerEnter(Collider other)
	{
		other.gameObject.transform.localScale += new Vector3(_scaleFuctor, _scaleFuctor, _scaleFuctor);

		this.PlayCurrentCubeChangedSound();

		OnCubeSelected(other.GetComponent<SellCubeBehaviour>());
	}

	private void OnTriggerExit(Collider other)
	{
		other.gameObject.transform.localScale -= new Vector3(_scaleFuctor, _scaleFuctor, _scaleFuctor);
	}

	#region CubeSelected
	private SellCubeBehaviour _selectedCube;
	private event Action<SellCubeBehaviour> _cubeSelected;
	public event Action<SellCubeBehaviour> CubeSelected
	{
		add
		{
			// behaviour event
			if (_selectedCube != null) value(_selectedCube);

			_cubeSelected += value;
		}
		remove
		{
			_cubeSelected -= value;
		}
	}

	protected void OnCubeSelected(SellCubeBehaviour selectedCube)
	{
		_selectedCube = selectedCube;

		_cubeSelected?.Invoke(selectedCube);
	}
	#endregion
}
