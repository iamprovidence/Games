using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
	public void SetHardView()
	{
		GetComponent<Animation>().Play("MakeGameHarder");
	}

	public void SetDefaultView()
	{
		GetComponent<Animation>().Play("MakeGameEasier");
	}
}
