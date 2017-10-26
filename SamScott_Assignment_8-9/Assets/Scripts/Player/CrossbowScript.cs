using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowScript : MonoBehaviour {

	//reference to player
	private GameObject _player;

	//reference to camera
	private Camera _mainCamera;

	//reference to ammoHolder
	private GameObject _ammoUI;

	//fire weapon buffer
	private bool shootBuffer = false;

	// Use this for initialization
	void Start () {
		_player = transform.parent.gameObject;
		_mainCamera = Camera.main;
		_ammoUI = GameObject.FindGameObjectWithTag ("CrossbowAmmo");
	}
	
	// Update is called once per frame
	private void Update () {
		FollowCursor();
		CheckInput();
	}

	private void FixedUpdate() {
		if (shootBuffer) FireCrossbow ();
	}

	private void FollowCursor() {
		//get mouse position and convert to world
		Vector3 currentMousePosition = Input.mousePosition;
		currentMousePosition.z = _mainCamera.farClipPlane;
		//current mouse position is now in world
		currentMousePosition = _mainCamera.ScreenToWorldPoint(currentMousePosition);

		//adjust weapon rotation
		transform.LookAt(currentMousePosition, Vector3.up);

	}

	private void CheckInput() {
		//check for click
		if (Input.GetMouseButtonDown (0)) {
			//if click, do ray on next fixed frame
			shootBuffer = true;
			//start sound effect

		}
	}

	private void FireCrossbow() {
		//cast ray
		RaycastHit hit;
		if (Physics.Raycast (_player.transform.position, transform.forward, out hit, 20f, 0)) {

		}
		//no matter what, draw ray and lose ammo
		Debug.DrawRay(_player.transform.position, transform.forward * 20f, Color.yellow, 1f, true);

		shootBuffer = false;
	}

	private void OnDrawGizmos() {
		//draw ray for debugging
		if (Application.isPlaying) Gizmos.DrawRay(_player.transform.position, transform.forward);
	}
}
