using UnityEngine;

public class StairBehaviour : MonoBehaviour
{
	[SerializeField]
	private bool _paintingEnabledForStair = true;

	private void Start()
	{
		if (CurrentUserInfo.IsPaintingEnabled && _paintingEnabledForStair)
		{
			GetComponent<MeshRenderer>().material.color = ColorHelper.Random();
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		this.PlayCubeDroppedSound();

		if (CurrentUserInfo.IsPaintingEnabled && _paintingEnabledForStair)
		{
			GetComponent<MeshRenderer>().material = collision.collider.GetComponent<MeshRenderer>().material;
		}
	}
}
