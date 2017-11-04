using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnParticleScript : MonoBehaviour {

	//particles
	private ParticleSystem pSystem;
	
	// Use this for initialization
	void Start ()
	{
		pSystem = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!pSystem.isPlaying && pSystem.particleCount == 0) Destroy(gameObject);
	}
	
}
