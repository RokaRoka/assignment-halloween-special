using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdmanScript : MonoBehaviour {

	//birdman guts explosion

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
	private float riseTime = 5.0f;
	//time it should take to reach the player
	private float journeyTime = 20f;

	//gameplay values
	public float travelSpeed = 1.0f;

	private float rotSway_X = 2f;

	public float attackSpeed = 1f;

	public float baseHitStun = 1f;

	//otehr script
	private EnemyHealth _enemyHealthScript;
	
	// Use this for initialization
	void Start () {
		_enemyHealthScript = GetComponent<EnemyHealth>();
		
		//get bounds
		Bounds birdmanBounds = transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().bounds;
		float birdmanHeight = birdmanBounds.size.y/2f;
		origin = transform.position + Vector3.down * birdmanHeight;
		risePosition = transform.position + Vector3.up * birdmanHeight;

		transform.position = origin;

	}

	// Update is called once per frame
	void Update () {
		switch (currentState) {
		case EnemyState.Rising:
			//Debug.Log ("Start rising!");
			RiseFromGrave ();
			break;
		case EnemyState.Moving:
			UpdatePosition ();
			break;
		case EnemyState.Attacking:
			//Debug.Log ("Attack, hiyah!");//do an attack
			UpdateAttack();
			break;
		default:
			//owch! get hit.
			break;
		}
	}

	private void RiseFromGrave() {
		t += Time.deltaTime * travelSpeed;
		if (t >= riseTime) {
			Debug.Log ("Has risen!!");
			rising = false;
			origin = transform.position;
			destination = GameObject.FindGameObjectWithTag("Player").transform.position + Vector3.forward * 2f;
			currentState = EnemyState.Moving;
			t = 0;
		} else {
			//move towards risen position
			transform.position = Vector3.Lerp(origin, risePosition, t/riseTime);
		}
	}

	private void UpdatePosition() {
		t += Time.deltaTime * travelSpeed;
		if (t >= journeyTime) {
			//attacking mode
			currentState = EnemyState.Attacking;
			t = 0;
		}
		else {
			//move towards player
			transform.position = Vector3.Lerp(origin, destination, t/journeyTime);
		}
	}

	private void UpdateAttack() {
		//attack the player every ___ seconds
		t += Time.deltaTime;
		if (t >= 10f/attackSpeed) {
			//Attack function!
			_enemyHealthScript.DealDamage();
			Debug.Log("Sycthe attack!");
			t -= attackSpeed;
		}
	}
	
}
