using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowScript : MonoBehaviour {

	//reference to camera
	private Camera _mainCamera;

	// Use this for initialization
	void Start () {
		_mainCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		FollowCursor();
		CheckInput();
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

		//if click, do ray on next fixed frame
	}
}
