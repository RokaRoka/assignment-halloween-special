using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using UnityEditor;
using UnityEngine;
using UnityEngineInternal;

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

	//audio source
	private AudioSource crossbowSounds;
	public AudioClip crossbowFire;
	
	public GameObject stickBolt;
	
	//reference to ammoHolder
	private GameObject _ammoUI;
	private AmmoScript _ammoUIScript;
	private int ammoCount = 0;

	//model offset
	private GameObject muzzleObject;
	
	private LineRenderer boltLine;

	//line renderer time vars
	private float line_t = 0;
	public float lineLifetime = 1f;
	
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
	public float shot_cooldown = 1f;
	public float reload_cooldown = 3f;

	// Use this for initialization
	void Start () {
		_gameManage = GameObject.FindGameObjectWithTag("GameController");
		_player = transform.parent.gameObject;
		_mainCamera = Camera.main;
		_ammoUI = GameObject.FindGameObjectWithTag ("CrossbowAmmo");
		_ammoUIScript = _ammoUI.GetComponent<AmmoScript>();
		ammoCount = _ammoUIScript.AmmoReload();

		crossbowSounds = GetComponent<AudioSource>();
		
		boltLine = GetComponent<LineRenderer>();
		muzzleObject = transform.GetChild (0).gameObject;
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
		if (line_t > 0)
		{
			line_t -= Time.deltaTime;
			if (line_t <= 0)
			{
				boltLine.enabled = false;
			}
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
				_gameManage.GetComponent<GameManager>().SwitchWeapon();
			}
		}
	}

	private void FireCrossbow() {
		//if ammo, fire the ray
		if (ammoCount > 0) {
			//cast ray
			RaycastHit hit;
			if (Physics.Raycast(muzzleObject.transform.position, muzzleObject.transform.forward, out hit, range))
			{
				Debug.Log(hit.transform.name);
				if (hit.transform.CompareTag("Enemy"))
				{
					hit.transform.GetComponent<EnemyHealth>().BloodSplatter(hit.point, Quaternion.LookRotation(hit.normal * -1));
					hit.transform.GetComponent<EnemyHealth>().TakeDamage(damage);
					Instantiate(stickBolt, hit.point, Quaternion.LookRotation(hit.normal * -1), hit.transform);
					
					Debug.Log("Hit!");
				} else if (hit.transform.CompareTag("SweetSpot"))
				{
					hit.transform.parent.GetComponent<EnemyHealth>().BloodSplatter(hit.point, Quaternion.LookRotation(hit.normal * -1));
					hit.transform.parent.GetComponent<EnemyHealth>().TakeDamage(damage * 2);
					
					Debug.Log("Hit SweetSpot!");
				}
			}
			//no matter what, draw ray and lose ammo
			
			Debug.DrawRay(muzzleObject.transform.position, muzzleObject.transform.forward * range, Color.yellow, 1f, true);
			StartHitLine();
			crossbowSounds.Play();
			
			ammoCount = _ammoUIScript.AmmoFired();
		}
		else {
			//otherwise play clicking sound
		}

		shootBuffer = false;
	}

	private void StartHitLine()
	{
		boltLine.SetPosition(0, muzzleObject.transform.position);
		boltLine.SetPosition(1, muzzleObject.transform.position + muzzleObject.transform.forward * range);
		boltLine.enabled = true;

		line_t = lineLifetime;
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
		if (Application.isPlaying) Gizmos.DrawRay(muzzleObject.transform.position, muzzleObject.transform.forward);
	}
}
