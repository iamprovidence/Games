using UnityEngine;
using UnityEngine.UI;

public class DiamondsBehaviour : MonoBehaviour
{
	[SerializeField]
	private Text _diamondsAmount;

	private void OnEnable()
	{
        CurrentUserInfo.DiamondsAmountChange += UpdateDiamondsCounter;
	}

	private void Awake()
	{
		UpdateDiamondsCounter(CurrentUserInfo.DiamondsAmount);
	}

	private void OnDisable()
	{
		CurrentUserInfo.DiamondsAmountChange -= UpdateDiamondsCounter;
	}

	private void UpdateDiamondsCounter(int diamondsAmount)
	{
		_diamondsAmount.text = diamondsAmount.ToString();
	}
}
