using UnityEngine;
using UnityEngine.UI;

public class MaxScoreBehaviour : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Text>().text = $"TOP: {CurrentUserInfo.MaxScore}";
    }
}
