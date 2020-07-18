using UnityEngine;

public class SellCubeBehaviour : MonoBehaviour
{
	[SerializeField]
	private int _price;

	public string Name => gameObject.name;
	public int Price => _price;
	public bool IsAvailable => CurrentUserInfo.IsCubeAvailable(gameObject.name);
	public bool IsSelected => CurrentUserInfo.IsCurrentColorSelected(gameObject.name);

	private void Start()
	{
		GetComponent<Renderer>().material.color = CubesColors.GetColor(gameObject.name);
	}
}
