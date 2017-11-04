using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyUpScript : MonoBehaviour {

	//t and journey
	private float t = 0;

	private float journey = 1f;
	
	// Update is called once per frame
	void Update ()
	{
		t += Time.deltaTime;

		if (t >= journey)
		{
			Destroy(gameObject);
		}
		else
		{
			transform.Translate(0, 0.2f/(1f + t*2f), 0);
		}
	}
}
