using Assets.Scripts.Infrastructure;
using UnityEngine;
using UnityEngine.UI;

public class MaxScoreBehaviour : MonoBehaviour
{
	public void Start()
	{
		this.GetComponent<Text>().text = CurrentUserInfo.MaxScore.ToString();
	}
}
