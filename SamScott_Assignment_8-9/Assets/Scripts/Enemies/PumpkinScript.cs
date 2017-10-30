using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinScript : MonoBehaviour {

	//original postion
	private Vector3 origin;
	//destination position (i.e. player)
	public Vector3 destination;

	//step value
	private float t = 0f;
	//time it should take to reach the player
	private float journeyTime = 10f;

	// Use this for initialization
	void Start () {
		origin = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void UpdatePosition() {
		t += Time.deltaTime;
		if (t >= journeyTime) {
			//blow the heck up
		}
		else {
			//move towards player
			transform.position = Vector3.Lerp(origin, destination, t);
			//update y pos
			//transform
			//update x pos
		}
	}
}
