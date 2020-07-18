using System.Collections;
using UnityEngine;

public class DiamondBehaviour : MonoBehaviour
{
	[SerializeField]
	private float _destroyDiamondDelayInSeconds = 8f;
	[SerializeField]
	private float _rotateSpeed = 1f;

	private void Start()
	{
		StartCoroutine(DestroyDiamondCoroutine(_destroyDiamondDelayInSeconds));
	}

	private IEnumerator DestroyDiamondCoroutine(float delayInSeconds)
	{
		yield return new WaitForSeconds(delayInSeconds);
		
		this.PlayDestroyDiamondSound();

		Destroy(gameObject);
	}

	private void Update()
	{
		Rotate();
	}

	private void Rotate()
	{
		transform.Rotate(Vector3.down * _rotateSpeed);
	}
}
