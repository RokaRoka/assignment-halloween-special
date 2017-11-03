using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	//damaged
	public GameObject _gutsExplosion;
	
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
		currentHealth -= value;
		if (currentHealth <= 0) Death();
	}

	private void Death()
	{
		//add score
		
		//create explosion prefab
		//destroy
		Destroy(gameObject);
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("Bullet"))
		{
			//spawn explosion of bits
		}
	}
}
