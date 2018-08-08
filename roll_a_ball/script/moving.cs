using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moving : MonoBehaviour 
{
	public float speed;
	private Rigidbody rb;
	public Text text, wintext;
	private int count = 0;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		wintext.gameObject.SetActive (false);
		setCount ();
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

		rb.AddForce(movement * speed);
		if (count == 7) 
		{
			wintext.gameObject.SetActive (true);
		}
	}

	void OnTriggerEnter (Collider other) 
	{
		if (other.tag == "point") 
		{
			Destroy (other.gameObject);
			++count;
			setCount ();
		}
	} 

	void setCount()
	{
		text.text = "Counter = " + count.ToString();
	}
}
