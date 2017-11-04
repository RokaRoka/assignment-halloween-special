using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode {
	Game, Pause
}

public enum PlayerWeapon
{
	Crossbow, Shotgun
}

public enum EnemyState {
	Rising,
	Moving,
	Hit,
	Attacking,
	Thriller
}

public class GameManager : MonoBehaviour {

	public GameMode currentMode;
	public PlayerWeapon currentWeapon;
	
	//player weapons
	public GameObject _crossbow;
	public GameObject _crossbowAmmoUI;
	public GameObject _shotgun;
	public GameObject _shotgunAmmoUI;
	
	//player score
	public GameObject _scoreUI;
	private int playerScore = 0;
	
	//player health
	public GameObject _healthUI;
	private GameObject[] _heartsUI = new GameObject[3];
	private int playerHealth = 3;
	
	private void Awake() {
		/*/switch mode to game mode
		SwitchMode(GameMode.Game);
		/*/
	}

	private void Start() {
		for (int i = 0; i < _heartsUI.Length; i++)
		{
			_heartsUI[i] = _healthUI.transform.GetChild(i).gameObject;
		}
		
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

	private void UpdateScoreUI()
	{
		_scoreUI.GetComponent<Text>().text = "Score: "+playerScore;
	}
	
	public void IncreaseScore(int value)
	{
		playerScore += value;
		UpdateScoreUI();
	}

	public void PlayerTakeDamage()
	{
		playerHealth--;
		_heartsUI[playerHealth].SetActive(false);
		if (playerHealth == 0)
		{
			//run gameover
		}
	}

	public void PlayerHealed()
	{
		playerHealth = 3;
		for (int i = 0; i < _heartsUI.Length; i++)
		{
			_heartsUI[i].SetActive(true);
		}
	}
}
