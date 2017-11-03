using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	//gameplay vars
	public int maxHealth = 2;

	private int currentHealth = 0;
	
	// Use this for initialization
	void Start ()
	{
		currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TakeDamage(int value)
	{
		
	}
}
