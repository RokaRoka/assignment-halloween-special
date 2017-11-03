using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchSpellScript : MonoBehaviour
{
	private GameObject _spawnManage;
	private GameObject[] _allGraves;
	
	//destination position
	public Vector3 destination;

	//rigidbody
	private Rigidbody rb;

	//gameplay values
	private float travelSpeed = 0.5f;

	private float t = 0;
	
	private float swaySpeed_Y = 4.0f;
	private float swayRange_Y = 0.35f;
	
	// Use this for initialization
	void Start()
	{
		_spawnManage = GameObject.FindGameObjectWithTag("SpawnController");
		_allGraves = _spawnManage.GetComponent<SpawnManager>()._allGraves;
		
		int randomGrave = Mathf.FloorToInt(Random.Range (0.0f, (float)_allGraves.Length - 1));
		destination = _allGraves[randomGrave].transform.position;
		
		rb = GetComponent<Rigidbody>();
		rb.AddForce(destination - transform.position, ForceMode.Impulse);
	}

	
	private void CreatePumpkin() {
		_spawnManage.SendMessage ("SpawnPumpkinSpell", transform.position);
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Terrain"))
		{
			CreatePumpkin();
			Destroy(gameObject);
		}
	}
}
