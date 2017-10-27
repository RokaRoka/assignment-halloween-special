using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoScript : MonoBehaviour {

	//array of ammo images
	public int ammoCount = 0;
	private GameObject[] ammoImages;


	private void Awake()
	{
		ammoImages = new GameObject[transform.childCount];
		//cycle through ammo images
		for (int i = 0; i < transform.childCount; i++)
		{
			ammoImages[i] = transform.GetChild(i).gameObject;
		}
		Debug.Log(ammoImages.Length);
	}

	// Use this for initialization
	void Start () {
		
	}

	public int AmmoFired()
	{
		ammoCount--;
		ammoImages[ammoCount].SetActive(false);
		return ammoCount;
	}

	public int AmmoReload()
	{
		ammoCount = ammoImages.Length;
		UpdateUI();
		return ammoCount;
	}

	private void UpdateUI() {
		for (int i = 0; i < ammoCount; i++)
		{
			ammoImages[i].SetActive(true);
		}
	}
}
