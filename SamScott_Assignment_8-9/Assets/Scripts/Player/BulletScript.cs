using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class BulletScript : MonoBehaviour {

	//speed
	private float speed = 300f;
	
	//damage
	private int damage = 1;
	
	//Distance counter
	private Vector3 origin;
	private Vector3 endStep;
	
	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.Impulse);
		origin = transform.position;
		endStep = origin + (transform.forward * transform.parent.GetComponent<ShotgunScript>().range);
		transform.parent = null;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (transform.position.magnitude >= endStep.magnitude)
		{
			Destroy(gameObject);
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("Enemy"))
		{
			other.gameObject.SendMessage("TakeDamage", damage);	
		}
		Destroy(gameObject);
	}
}
