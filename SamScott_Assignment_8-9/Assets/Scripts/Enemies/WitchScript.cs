using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class WitchScript : MonoBehaviour
{

	private Vector3 origin;

	private Vector3 destination;
	
	//how far the with travels
	private float t = 0;
	private float distance = 10.0f;
	
	//gameplay values
	private float travelSpeed = 3.0f;
	
	private float swaySpeed_Y = 2.0f;
	private float swayRange_Y = 0.5f;
	
	// Use this for initialization
	void Start ()
	{
		origin = transform.position;
		destination = origin + (transform.forward * distance);
	}
	
	// Update is called once per frame
	void Update ()
	{
		UpdatePosition();
	}

	private void UpdatePosition()
	{
		t += Time.deltaTime * travelSpeed;

		if (t >= distance * 2.0f) t -= distance * 2.0f;
			
		if (t >= distance)
		{
			//move towards origin
			transform.position = Vector3.Lerp(destination, origin, t/(distance*2.0f));
		}
		else
		{
			//move towards desitnation
			transform.position = Vector3.Lerp(origin, destination, t/distance);
		
		}
		
		//update y pos
		transform.Translate(Vector3.up * (Mathf.Sin(t * swaySpeed_Y) / 2.0f) * swayRange_Y);
		//transform
		//update x pos
	
	}
}
