using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationBehaviour : MonoBehaviour
{
	public void PlayCubeDroppedOnStairSound()
	{
		this.PlayCubeDroppedSound();
	}

	public void OpenGameScene()
	{
		SceneManager.LoadSceneAsync("Game");
	}
}
