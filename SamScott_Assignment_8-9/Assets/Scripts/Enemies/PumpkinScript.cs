﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PumpkinScript : MonoBehaviour {

	//pumpkin guts explosion
	
	//enemy state here
	private EnemyState currentState = EnemyState.Rising;

	//original postion
	private Vector3 origin;

	private bool rising = true;
	//rise destination
	private Vector3 risePosition;
	//destination position (i.e. player)
	private Vector3 destination;
	
	//step value
	private float t = 0f;

	//time is should take to reach rise position
	private float riseTime = 1f;
	//time it should take to reach the player
	private float journeyTime = 10f;

	//gameplay values
	private float travelSpeed = 1.0f;
	
	private float swaySpeed_X = 2f;
	private float swayRange_X = 0.25f;
	
	private float swaySpeed_Y = 3.0f;
	private float swayRange_Y = 1.5f;

	public float baseHitStun = 1f;
	
	// Use this for initialization
	void Start () {
		origin = transform.position;
		risePosition = transform.position + Vector3.up * 2f;

	}
	
	// Update is called once per frame
	void Update () {
		switch (currentState) {
		case EnemyState.Rising:
			RiseFromGrave ();
			break;
		case EnemyState.Moving:
			UpdatePosition ();
			break;
		case EnemyState.Attacking:
			Debug.Log ("Attack, hiyah!");//do an attack
			break;
		default:
			//owch! get hit.
			break;
		}
	}

	private void RiseFromGrave() {
		t += Time.deltaTime * travelSpeed;
		if (t >= riseTime) {
			rising = false;
			origin = transform.position;
			destination = GameObject.FindGameObjectWithTag("Player").transform.position + Vector3.forward;
			currentState = EnemyState.Moving;
			t = 0;
		} else {
			//move towards player
			transform.position = Vector3.Lerp(origin, risePosition, t/riseTime);
		}
	}

	private void UpdatePosition() {
		t += Time.deltaTime * travelSpeed;
		if (t >= journeyTime) {
			//blow the heck up
			//player damage
			Destroy(gameObject);
		}
		else {
			//move towards player
			transform.position = Vector3.Lerp(origin, destination, t/journeyTime);
			//update x and y pos
			transform.Translate((Vector3.up * Mathf.Sin(t*swaySpeed_Y)) * swayRange_Y + Vector3.right * Mathf.Sin(t*swaySpeed_X) * swayRange_X);
		}
	}
}
