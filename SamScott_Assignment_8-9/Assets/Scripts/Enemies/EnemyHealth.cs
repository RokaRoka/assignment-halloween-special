using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	//damaged
	public GameObject _gutsExplosion;
	
	//game manager reference
	private GameManager _gameManager;
	
	//gameplay vars
	public int maxHealth = 2;

	public int scoreValue = 200;

	private int currentHealth = 0;
	
	// Use this for initialization
	void Start ()
	{
		_gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
		currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TakeDamage(int value)
	{
		currentHealth -= value;
		Debug.Log("Ow! my health is "+currentHealth);
		if (currentHealth <= 0) Death();
	}

	private void Death()
	{
		//add score
		_gameManager.IncreaseScore(scoreValue);
		//create explosion prefab
		//destroy
		Destroy(gameObject);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Bullet"))
		{
			//spawn explosion of bits
		}
	}
}
