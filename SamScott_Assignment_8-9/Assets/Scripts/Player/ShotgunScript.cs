﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShotgunScript : MonoBehaviour {

	enum WeaponMode
	{
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

	//reference to bullets
	public GameObject _bullet;
	
	//fire weapon buffer
	private bool shootBuffer = false;
	
	//weapons current mode
	private WeaponMode weaponStatus = WeaponMode.Ready;

	//model offset
	private GameObject muzzleObject;
	
	//weapon range
	public float range = 50f;
	
	//spread radius
	public float spreadRadius = 0.2f;

	public int spreadAmount = 5;
	
	//weapon cooldown
	private float currentWait = 0;
	public float shot_cooldown = 0.5f;
	public float reload_cooldown = 2f;
	
	// Use this for initialization
	void Start()
	{
		_gameManage = GameObject.FindGameObjectWithTag("GameController");
		_player = transform.parent.gameObject;
		_mainCamera = Camera.main;
		_ammoUI = GameObject.FindGameObjectWithTag("ShotgunAmmo");
		_ammoUIScript = _ammoUI.GetComponent<AmmoScript>();
		ammoCount = _ammoUIScript.AmmoReload();
		muzzleObject = transform.GetChild (0).gameObject;
	}

	// Update is called once per frame
	private void Update()
	{
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

	private void FixedUpdate()
	{
		if (shootBuffer && weaponStatus == WeaponMode.Ready) FireShotgun();
	}

	private void FollowCursor()
	{
		//get mouse position and convert to world
		Vector3 currentMousePosition = Input.mousePosition;
		currentMousePosition.z = _mainCamera.farClipPlane;
		//current mouse position is now in world
		currentMousePosition = _mainCamera.ScreenToWorldPoint(currentMousePosition);

		//adjust weapon rotation
		transform.LookAt(currentMousePosition, Vector3.up);
		transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);

	}
	
	private void CheckInput()
	{
		if (weaponStatus == WeaponMode.Ready)
		{
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
				_gameManage.GetComponent<GameManager>().SwitchWeapon();
			}
		}
	}

	private void FireShotgun()
	{
		//if ammo, fire the ray
		if (ammoCount > 0)
		{
			Vector3 muzzlePosition = muzzleObject.transform.position;
			Vector3 randomRot;
			Quaternion bulletRot;

			//instantiate bullets
			for (int i = 0; i < spreadAmount; i++)
			{
				randomRot = transform.forward;
				randomRot.x += Random.Range(0f, spreadRadius * 2f) - spreadRadius;
				randomRot.y += Random.Range(0f, spreadRadius * 2f) - spreadRadius;

				//make a look rotation
				bulletRot = Quaternion.LookRotation(randomRot);
				Instantiate(_bullet, muzzlePosition, bulletRot, transform);
				Debug.DrawRay(muzzlePosition, randomRot * range, Color.blue, 1f, true);
			}
			
			//no matter what, draw ray and lose ammo
			Debug.DrawRay(muzzlePosition, transform.forward * range, Color.yellow, 1f, true);
			ammoCount = _ammoUIScript.AmmoFired();
		}
		else
		{
			//otherwise play clicking sound
		}
		
		weaponStatus = WeaponMode.CoolDown;
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

	private void OnDrawGizmos()
	{
		//draw ray for debugging
		if (Application.isPlaying) Gizmos.DrawRay(transform.position, transform.forward);
	}
}
