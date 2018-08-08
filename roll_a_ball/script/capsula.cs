using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class capsula : MonoBehaviour 
{
	void Update () 
	{
		transform.Rotate (new Vector3 (15, 35, 5) * Time.deltaTime);
	}

	/*public float speed, tilt;

	private Vector3 target = new Vector3 (2f, 0.7f, 1.3f);

	void Update () 
	{
		transform.position = Vector3.MoveTowards (transform.position, target, Time.deltaTime * speed);

		if (transform.position == target)
			target.y = (target.y == 0.7f) ? 0.3f : 0.7f;

		transform.Rotate (Vector3.up * tilt);
	}
		*/
}
