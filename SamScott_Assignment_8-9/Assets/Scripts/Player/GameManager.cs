using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode {
	Game, Pause
}

public enum PlayerWeapon
{
	Crossbow, Shotgun
}

public class GameManager : MonoBehaviour {

	public GameMode currentMode;
	public PlayerWeapon currentWeapon;
	
	//player weapons

	public GameObject _crossbow;
	public GameObject _crossbowAmmoUI;
	public GameObject _shotgun;
	public GameObject _shotgunAmmoUI;
	
	private void Awake() {
		/*/switch mode to game mode
		SwitchMode(GameMode.Game);
		/*/
	}

	private void Start() {
		currentWeapon = PlayerWeapon.Crossbow;
		//switch mode to game mode
		SwitchMode(GameMode.Game);
	}

	public void SwitchMode(GameMode newMode) {
		currentMode = newMode;
		//if game, resume play and confine cursor
		if (newMode == GameMode.Game) {
			Cursor.lockState = CursorLockMode.Confined;
			Debug.Log("Mode is Game!");
		}
		//if pause, stop play and free cursor
		//else if (newMode == GameMode.Pause) Cursor.lockState = CursorLockMode.None;
	}

	public void SwitchWeapon()
	{
		if (currentWeapon == PlayerWeapon.Crossbow)
		{
			currentWeapon = PlayerWeapon.Shotgun;
			//disable crossbow
			_crossbow.SetActive(false);
			_crossbowAmmoUI.SetActive(false);
			//enable shotgun
			_shotgun.SetActive(true);
			_shotgunAmmoUI.SetActive(true);
		}
		else if (currentWeapon == PlayerWeapon.Shotgun)
		{
			currentWeapon = PlayerWeapon.Crossbow;
			//disable crossbow
			_crossbow.SetActive(true);
			_crossbowAmmoUI.SetActive(true);
			//enable shotgun
			_shotgun.SetActive(false);
			_shotgunAmmoUI.SetActive(false);
		}
	}
}
