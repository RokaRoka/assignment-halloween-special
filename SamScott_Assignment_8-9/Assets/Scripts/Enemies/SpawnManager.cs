﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

	//SpawnPoint Gameobject list
	public GameObject _graveHolderMaster;
	private GameObject[] _allGraves;

	//EnemyGameObjects
	public GameObject _enemyHolder;
	private GameObject newEnemy;
	//Pumpkin
	public GameObject _pumpkin;
	//ZombieBirdmanSpawner
	public GameObject _birdman;
	//Witch
	public GameObject _witch;

	//Spawn smoke

	//wave number
	public int waveNum = 1;
	//progress through wave
	private float t = 0f;
	private float waveLength = 40f;
	private int waveTick = 0;

	private void Awake() {
		_allGraves = new GameObject[_graveHolderMaster.transform.childCount];
	}

	// Use this for initialization
	private void Start () {
		for (int i = 0; i < 2; i++) {
			_allGraves[i] = _graveHolderMaster.transform.GetChild(i).gameObject;
		}	
	}
	
	// Update is called once per frame
	private void Update () {
		t += Time.deltaTime;
		Debug.Log ("t Time: " + t);
		UpdateTick();
	}

	private void UpdateTick() {
		Debug.Log ("Wave tick is "+waveTick+". Next wave is at: "+(waveLength/4f));
		if (t >= waveLength / 4f && waveTick < 1) {
			waveTick = 1;
			Debug.Log ("WaveTick: " + waveTick);
			int randomGrave = Mathf.FloorToInt(Random.Range (0.0f, (float)_allGraves.Length - 1));
			newEnemy = EnemySpawn(_pumpkin, _allGraves[randomGrave].transform.position, _allGraves [randomGrave].transform.rotation);
		} else if (t >= (waveLength / 4f) * 2f && waveTick < 2) {
			waveTick = 2;
		} else if (t >= (waveLength / 4f) * 3f && waveTick < 3) {
			waveTick = 3;
		} else if (t >= waveLength && waveTick < 4) {
			//pause ticking until all enemies are defeated
			waveTick = 4;
		}
	}

	private GameObject EnemySpawn(GameObject toSpawn, Vector3 position, Quaternion rotation) {
		Debug.Log ("Spawning " + _pumpkin.name);
		return Instantiate (toSpawn, position, rotation);
	}

	//specifically for the witch
	public void SpawnPumpkinSpell(Vector3 position, Vector3 direction) {
		Quaternion newRot = Quaternion.LookRotation (direction);
		newEnemy = EnemySpawn(_pumpkin, position, newRot);
	}
}
