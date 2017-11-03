using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class WitchScript : MonoBehaviour
{

	//witch guts explosion
	
	private GameObject newSpell;
	public GameObject _spell;
	
	private Vector3 origin;

	private Vector3 destination;
	
	//how far the with travels
	private float t = 0;
	private float distance = 10.0f;
	
	//gameplay values
	private float travelSpeed = 3.0f;
	
	private float swaySpeed_Y = 2.0f;
	private float swayRange_Y = 0.5f;

	//spawn variables
	private float spawnFreq = 1.0f;
	private float spawnFreqMax = 10.0f;
	
	private int spawnCount = 0;
	
	// Use this for initialization
	void Start ()
	{
		transform.rotation = Quaternion.LookRotation(Vector3.right);
		
		origin = transform.position;
		destination = origin + (transform.forward * distance);
	}
	
	// Update is called once per frame
	void Update ()
	{
		UpdatePosition();
		UpdateTick();
	}

	private void UpdatePosition()
	{
		t += Time.deltaTime * travelSpeed;
		
		//if done with a loop, reset vars
		if (t >= distance * 2.0f)
		{
			t = 0;
			spawnCount = 0;
		}
		
		if (t >= distance)
		{
			//move towards origin
			transform.position = Vector3.Lerp(destination, origin, (t-distance)/distance);
		}
		else
		{
			//move towards desitnation
			transform.position = Vector3.Lerp(origin, destination, t/distance);
		
		}
		
		//update y pos
		transform.Translate(Vector3.up * (Mathf.Sin(t * swaySpeed_Y) / 2.0f) * swayRange_Y);
		//transform
		//update x pos
	
	}

	private void UpdateTick()
	{
		if (Mathf.FloorToInt(t / distance * spawnFreq) >= spawnCount)
		{
			spawnCount++;
			CreateSpell();
		}
	}

	private void CreateSpell()
	{
		Debug.Log("Spell cast!");
		newSpell = Instantiate(_spell, transform.position, Quaternion.identity);
	}
	
	
}
