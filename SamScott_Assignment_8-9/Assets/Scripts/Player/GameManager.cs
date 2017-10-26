using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode {
	Game, Pause
}

public class GameManager : MonoBehaviour {

	public GameMode currentMode;

	private void Awake() {
		/*/switch mode to game mode
		SwitchMode(GameMode.Game);
		/*/
	}

	private void Start() {
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
}
