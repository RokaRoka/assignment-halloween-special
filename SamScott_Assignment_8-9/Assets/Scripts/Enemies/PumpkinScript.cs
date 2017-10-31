using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PumpkinScript : MonoBehaviour {

	//original postion
	private Vector3 origin;
	//destination position (i.e. player)
	private Vector3 destination;

	//step value
	private float t = 0f;
	//time it should take to reach the player
	private float journeyTime = 10f;

	//gameplay values
	private float travelSpeed = 1.0f;
	
	private float swaySpeed_X = 1.0f;

	private float swaySpeed_Y = 5.0f;
	
	// Use this for initialization
	void Start () {
		origin = transform.position;
		destination = GameObject.FindGameObjectWithTag("Player").transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		UpdatePosition();
	}

	private void UpdatePosition() {
		t += Time.deltaTime * travelSpeed;
		if (t >= journeyTime) {
			//blow the heck up
			Destroy(gameObject);
		}
		else {
			//move towards player
			transform.position = Vector3.Lerp(origin, destination, t/journeyTime);
			//update y pos
			transform.Translate(Vector3.up * Mathf.Sin(t*swaySpeed_Y));
			//transform
			//update x pos
		}
	}
}
