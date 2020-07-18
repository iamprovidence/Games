using UnityEngine;

public class AnimatedCubeBehaviour : MonoBehaviour
{
	private void Start()
	{
		GetComponent<Renderer>().material.color = CurrentUserInfo.GetSelectedCubeColor();
	}
}
