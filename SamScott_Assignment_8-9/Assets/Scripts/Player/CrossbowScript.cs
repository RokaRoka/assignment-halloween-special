using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowScript : MonoBehaviour {
	
	enum WeaponMode {
		Ready, CoolDown, Reload
	}

	//reference to player
	private GameObject _player;

	//reference to camera
	private Camera _mainCamera;

	//reference to ammoHolder
	private GameObject _ammoUI;
	private AmmoScript _ammoUIScript;
	private int ammoCount = 0;

	//fire weapon buffer
	private bool shootBuffer = false;

	//weapons current mode
	private WeaponMode weaponStatus = WeaponMode.Ready;

	//weapon range
	public float range = 50f;

	// Use this for initialization
	void Start () {
		_player = transform.parent.gameObject;
		_mainCamera = Camera.main;
		_ammoUI = GameObject.FindGameObjectWithTag ("CrossbowAmmo");
		_ammoUIScript = _ammoUI.GetComponent<AmmoScript>();
		ammoCount = _ammoUIScript.AmmoReload();
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
		if (weaponStatus == WeaponMode.Ready) {
			//check for click
			if (Input.GetMouseButtonDown(0))
			{
				//if click, do ray on next fixed frame
				shootBuffer = true;
				//start sound effect

			}
			//check for reload
			else if (Input.GetKeyDown(KeyCode.R))
			{
				ammoCount = _ammoUIScript.AmmoReload();
			}
			//check for weapon to switch
			else if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				//switch weapon
			}
		}
	}

	private void FireCrossbow() {
		//if ammo, fire the ray
		if (ammoCount > 0) {
			//cast ray
			RaycastHit hit;
			if (Physics.Raycast(transform.position, transform.forward, out hit, range, 0))
			{
				//if (hit.transform.CompareTag("Enemy")) ; //hurt enemy or some shit
			}
			//no matter what, draw ray and lose ammo
			Debug.DrawRay(transform.position, transform.forward * range, Color.yellow, 1f, true);
			ammoCount = _ammoUIScript.AmmoFired();
		}
		else {
			//otherwise play clicking sound
		}

		shootBuffer = false;
	}

	private void OnDrawGizmos() {
		//draw ray for debugging
		if (Application.isPlaying) Gizmos.DrawRay(transform.position, transform.forward);
	}
}
