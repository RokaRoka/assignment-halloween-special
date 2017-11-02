using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

	//SpawnPoint Gameobject list
	private GameObject[] _allGraves;

	//EnemyGameObjects
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
	private float waveLength = 60f;

	// Use this for initialization
	private void Start () {
		
	}
	
	// Update is called once per frame
	private void Update () {
		
	}

	private void WaveTick() {
		if (t < waveLength / 4f) {
			//EnemySpawn(_pumpkin, )
		} else if (t < (waveLength / 4f) * 2f) {

		} else if (t < (waveLength / 4f) * 3f) {

		} else {

		}
	}

	private GameObject EnemySpawn(GameObject toSpawn, Vector3 position, Quaternion rotation) {
		return Instantiate (toSpawn, position, rotation);
	}

	//specifically for the witch
	public void SpawnPumpkinSpell(Vector3 position, Vector3 direction) {
		Quaternion newRot = Quaternion.LookRotation (direction);
		GameObject newEnemy = EnemySpawn(_pumpkin, position, newRot);
	}
}
