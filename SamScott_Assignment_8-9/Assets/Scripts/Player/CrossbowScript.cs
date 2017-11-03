using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CrossbowScript : MonoBehaviour {
	
	enum WeaponMode {
		Ready, CoolDown, Reload
	}
	//ref to game manager
	private GameObject _gameManage; 
	
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

	//waepon damage
	private int damage = 2;
	
	//weapon range
	public float range = 50f;
	
	//weapon cooldown
	private float currentWait = 0;
	public float shot_cooldown = 0.25f;
	public float reload_cooldown = 4f;

	// Use this for initialization
	void Start () {
		_gameManage = GameObject.FindGameObjectWithTag("GameController");
		_player = transform.parent.gameObject;
		_mainCamera = Camera.main;
		_ammoUI = GameObject.FindGameObjectWithTag ("CrossbowAmmo");
		_ammoUIScript = _ammoUI.GetComponent<AmmoScript>();
		ammoCount = _ammoUIScript.AmmoReload();
	}
	
	// Update is called once per frame
	private void Update () {
		FollowCursor();
		if (weaponStatus == WeaponMode.Ready)
		{
			CheckInput();
		}
		else if (weaponStatus == WeaponMode.CoolDown)
		{
			OnCooldown(shot_cooldown);
		} 
		else if (weaponStatus == WeaponMode.Reload)
		{
			OnCooldown(reload_cooldown);
		}
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
			else if (Input.GetKeyDown(KeyCode.Space))
			{
				//switch weapon
				_gameManage.SendMessage("SwitchWeapon");
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
				if (hit.transform.CompareTag("Enemy")) hit.transform.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
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
	
	private void OnCooldown(float cooldownTime)
	{
		currentWait += Time.deltaTime;
		if (currentWait >= cooldownTime)
		{
			weaponStatus = WeaponMode.Ready;
			currentWait = 0;
		}
	}

	private void OnDrawGizmos() {
		//draw ray for debugging
		if (Application.isPlaying) Gizmos.DrawRay(transform.position, transform.forward);
	}
}
