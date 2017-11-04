using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour {

	//Audio manager
	public GameObject _audioManage;
	
	//UI refs
	public GameObject _waveUI;
	
	//SpawnPoint Gameobject list
	public GameObject _masterGrave;
	private GameObject[] _graveEyes = new GameObject[2];
	public GameObject _graveHolderMaster;
	public GameObject[] _allGraves;
	
	//Spawnfire gameobject
	public GameObject _spawnFire;
	
	//EnemyGameObjects
	public GameObject _enemyHolder;
	private GameObject currentBirdman;
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
		_graveEyes = GameObject.FindGameObjectsWithTag("Statue");
	}
	
	// Update is called once per frame
	private void Update () {
		if (!t_stopped) t += Time.deltaTime;
		UpdateTick();
	}

	private void UpdateTick()
	{
		int randomGrave = 0;
		int randomEnemyAmount = 0;
		
		//Debug.Log ("Wave tick is "+waveTick+". Next wave is at: "+(waveLength/4f));
		if (t >= waveLength / 5f && waveTick < 1)
		{
			waveTick = 1;
			//SPAWN PUMPKIN WAVE TICK 1
			Debug.Log("wavetick is "+waveTick);
			randomGrave = Mathf.FloorToInt(Random.Range (0.0f, (float)_allGraves.Length - 1));
			newEnemy = EnemySpawn(_pumpkin, _allGraves[randomGrave].transform.position);
			
			//SPAWN MORE RANDO ENEMIES PAST WAVE 2
			randomEnemyAmount = Mathf.FloorToInt(Random.Range(0.0f, Mathf.FloorToInt(waveNum / 3f)));
			for (int i = 0; i < randomEnemyAmount; i++)
			{
				randomGrave = Mathf.FloorToInt(Random.Range (0.0f, (float)_allGraves.Length - 1));
				newEnemy = EnemySpawn(_pumpkin, _allGraves[randomGrave].transform.position);
			}
		} else if (t >= (waveLength / 5f) * 2f && waveTick < 2)
		{
			waveTick = 2;
			//SPAWN WITCH WAVE TICK 2
			Debug.Log("wavetick is "+waveTick);
			randomGrave = Mathf.FloorToInt(Random.Range (0.0f, (float)_allGraves.Length - 1));
			newEnemy = EnemySpawn(_witch, _allGraves[randomGrave].transform.position + transform.up * 6f + Vector3.forward * 6f);
			
		} else if (t >= (waveLength / 5f) * 3f && waveTick < 3) 
		{
			waveTick = 3;
			//SPAWN 2 PUMPKIN WAVE TICK 3
			Debug.Log("wavetick is "+waveTick);
			randomGrave = Mathf.FloorToInt(Random.Range (0.0f, (float)_allGraves.Length - 1));
			newEnemy = EnemySpawn(_pumpkin, _allGraves[randomGrave].transform.position);
			randomGrave = Mathf.FloorToInt(Random.Range (0.0f, (float)_allGraves.Length - 1));
			newEnemy = EnemySpawn(_pumpkin, _allGraves[randomGrave].transform.position);
			
			//SPAWN MORE RANDO ENEMIES PAST WAVE 2
			randomEnemyAmount = Mathf.FloorToInt(Random.Range(0.0f, Mathf.FloorToInt(waveNum / 3f)));
			for (int i = 0; i < randomEnemyAmount; i++)
			{
				randomGrave = Mathf.FloorToInt(Random.Range (0.0f, (float)_allGraves.Length - 1));
				newEnemy = EnemySpawn(_pumpkin, _allGraves[randomGrave].transform.position);
			}
		} else if (t >= (waveLength / 5f) * 4f && waveTick < 4) 
		{
			waveTick = 4;
			//SPAWN BIRDMAN
			Debug.Log("wavetick is "+waveTick);
			currentBirdman = EnemySpawn(_birdman, _masterGrave.transform.position);
			BirdmanSpawns();
		}
	}

	private GameObject EnemySpawn(GameObject toSpawn, Vector3 position) {
		Debug.Log ("Spawning " + toSpawn.name);
		Instantiate(_spawnFire, position, toSpawn.transform.rotation);
		return Instantiate (toSpawn, position, toSpawn.transform.rotation, _enemyHolder.transform);
	}
	
	//specifically for the witch
	public void SpawnPumpkinSpell(Vector3 position) {
		newEnemy = EnemySpawn(_pumpkin, position);
	}
	
	private void WaveComplete()
	{
		//_audioManage.GetComponent<AudioSource>().Stop();
		waveNum++;
		UpdateWaveUI();
		t = 0;
		waveTick = 0;
		t_stopped = false;

		for (int i = 0; i < 2; i++) {
			_graveEyes[i].GetComponent<LensFlare>().enabled = true;
		}
		
		//make game LONGER and HARDER
		waveLength += waveNum * waveDifficultyIncrement;
	}

	public void CheckWaveComplete()
	{
		//work oon this
		if (t_stopped && currentBirdman == null)
		{
			WaveComplete();
		}
	}

	private void UpdateWaveUI()
	{
		_waveUI.GetComponent<Text>().text = "Wave " + waveNum;
		_waveUI.transform.GetChild(0).GetComponent<Text>().text = "Wave " + waveNum;
	}

	private void BirdmanSpawns() {
		//start music
		if (!_audioManage.GetComponent<AudioSource>().isPlaying) _audioManage.GetComponent<AudioSource>().Play();
		for (int i = 0; i < 2; i++) {
			_graveEyes[i].GetComponent<LensFlare>().enabled = true;
		}
		//pause ticking until all enemies are defeated
		t_stopped = true;
	}

}
