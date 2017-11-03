using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour {

	//UI refs
	public GameObject _waveUI;
	
	//SpawnPoint Gameobject list
	public GameObject _masterGrave;
	public GameObject _graveHolderMaster;
	public GameObject[] _allGraves;
	
	//EnemyGameObjects
	public GameObject _enemyHolder;
	private GameObject newEnemy;
	//Pumpkin
	public GameObject _pumpkin;
	//ZombieBirdmanSpawner
	public GameObject _birdman;
	//Witch
	public GameObject _witch;

	//wave number
	public int waveNum = 1;

	public float waveDifficultyIncrement = 2f;
	//progress through wave
	private float t = 0f;

	private bool t_stopped = false;
	
	private float waveLength = 40f;
	private int waveTick = 0;
	

	private void Awake() {
		_allGraves = new GameObject[_graveHolderMaster.transform.childCount];
	}

	// Use this for initialization
	private void Start () {
		for (int i = 0; i < _allGraves.Length; i++) {
			_allGraves[i] = _graveHolderMaster.transform.GetChild(i).gameObject;
		}	
	}
	
	// Update is called once per frame
	private void Update () {
		if (!t_stopped) t += Time.deltaTime;
		//Debug.Log ("t Time: " + t);
		UpdateTick();
	}

	private void UpdateTick() {
		//Debug.Log ("Wave tick is "+waveTick+". Next wave is at: "+(waveLength/4f));
		if (t >= waveLength / 4f && waveTick < 1)
		{
			waveTick = 1;
			//SPAWN PUMPKIN WAVE TICK 1
			Debug.Log ("WaveTick: " + waveTick);
			int randomGrave = Mathf.FloorToInt(Random.Range (0.0f, (float)_allGraves.Length - 1));
			Debug.Log("Random number: "+randomGrave);
			newEnemy = EnemySpawn(_pumpkin, _allGraves[randomGrave].transform.position);
			
		} else if (t >= (waveLength / 4f) * 2f && waveTick < 2)
		{
			waveTick = 2;
			//SPAWN WITCH WAVE TICK 2
			int randomGrave = Mathf.FloorToInt(Random.Range (0.0f, (float)_allGraves.Length - 1));
			newEnemy = EnemySpawn(_witch, _allGraves[randomGrave].transform.position + transform.up * 6f + Vector3.forward * 6f);
			
		} else if (t >= (waveLength / 4f) * 3f && waveTick < 3) 
		{
			waveTick = 3;
			//SPAWN 2 PUMPKIN WAVE TICK 3
			int randomGrave = Mathf.FloorToInt(Random.Range (0.0f, (float)_allGraves.Length - 1));
			newEnemy = EnemySpawn(_pumpkin, _allGraves[randomGrave].transform.position);
			randomGrave = Mathf.FloorToInt(Random.Range (0.0f, (float)_allGraves.Length - 1));
			newEnemy = EnemySpawn(_pumpkin, _allGraves[randomGrave].transform.position);
			
		} else if (t >= waveLength && waveTick < 4) 
		{
			waveTick = 4;
			//SPAWN BIRDMAN
			int randomGrave = Mathf.FloorToInt(Random.Range (0.0f, (float)_allGraves.Length - 1));
			newEnemy = EnemySpawn(_birdman, _masterGrave.transform.position);
			//pause ticking until all enemies are defeated
			t_stopped = true;
		}
	}

	private void WaveComplete()
	{
		waveNum++;
		UpdateWaveUI();
		t = 0;
		t_stopped = false;
		
		//make game LONGER and HARDER
		waveLength += waveNum * waveDifficultyIncrement;
	}

	public void CheckWaveComplete()
	{
		if (t_stopped && _enemyHolder.transform.childCount == 0)
		{
			WaveComplete();
		}
	}

	private void UpdateWaveUI()
	{
		_waveUI.GetComponent<Text>().text = "Wave " + waveNum;
		_waveUI.transform.GetChild(0).GetComponent<Text>().text = "Wave " + waveNum;
	}
	
	private GameObject EnemySpawn(GameObject toSpawn, Vector3 position) {
		Debug.Log ("Spawning " + _pumpkin.name);
		return Instantiate (toSpawn, position, Quaternion.identity, _enemyHolder.transform);
	}
	
	//specifically for the witch
	public void SpawnPumpkinSpell(Vector3 position) {
		newEnemy = EnemySpawn(_pumpkin, position);
	}
}
