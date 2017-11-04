using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	//damaged
	public GameObject _bloodSplat;

	public GameObject _deathParticles;
	
	//score popup
	public GameObject _textPopUp;
	
	//game manager reference
	private GameManager _gameManager;
	private SpawnManager _spawnManger;
	
	//gameplay vars
	public int maxHealth = 2;

	public int scoreValue = 200;

	private int currentHealth = 0;
	
	// Use this for initialization
	void Start ()
	{
		_gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
		_spawnManger = GameObject.FindGameObjectWithTag ("SpawnController").GetComponent<SpawnManager>();

		currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DealDamage()
	{
		_gameManager.PlayerTakeDamage();
	}
	
	public void TakeDamage(int value)
	{
		currentHealth -= value;
		Debug.Log("Ow! my health is "+currentHealth);
		if (currentHealth <= 0) Death();
	}

	public void BloodSplatter(Vector3 position, Quaternion rotation)
	{
		//spawn explosion of bits
		Instantiate(_bloodSplat, position, rotation, transform);
	}

	private void Death()
	{
		//add score
		_gameManager.IncreaseScore(scoreValue);
		//create explosion prefab
		Instantiate(_deathParticles, transform.position, transform.rotation);
		GameObject someText = Instantiate(_textPopUp, transform.position, Quaternion.identity);
		someText.GetComponent<TextMesh>().text = scoreValue.ToString();

		_spawnManger.CheckWaveComplete();

		//destroy
		Destroy(gameObject);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Bullet"))
		{
			BloodSplatter(other.transform.position, Quaternion.Inverse(other.transform.rotation));
			//change to hit animation
			//cause hit stun
		}
	}
}
