﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject[] debris;

	public Transform waveMover;

	public Transform bounds1;
	public Transform bounds2;

	public bool randomRot = true;

	public Vector2 repeatRate = new Vector2(1,2);

	public bool increaseSpawnrate = false;
	public float spawnRateIncrease = 0.5f;
	public float spawnIncreaseRate = 5;

	// Use this for initialization
	void Start () 
	{
		spawnObject ();
		Invoke ("startIncreaseSpawn", 10);

		//Time.timeScale = 3;
	}
	
	// Update is called once per frame
	void Update () 
	{

		repeatRate.x = Mathf.Clamp (repeatRate.x, 0.2f, 999);
		repeatRate.y = Mathf.Clamp (repeatRate.y, 0.2f, 999);
//if (Input.GetButton ("Fire1")) {
//	Debug.Log ("1");
//}
//
//if (Input.GetButton ("Fire2")) {
//	Debug.Log ("2");
//}
//
//if (Input.GetButton ("Fire3")) {
//	Debug.Log ("3");
//}
//
//if (Input.GetButton ("Fire4")) {
//	Debug.Log ("4");
//}
//
//if (Input.GetButton ("Fire5")) {
//	Debug.Log ("5");
//}
//
//
	}

	void spawnObject()
	{
		var Xpos = Random.Range (bounds1.position.x, bounds2.position.x);
		var objToSpawn = Random.Range (0, debris.Length);

		var g = Instantiate (debris [objToSpawn], transform) as GameObject;

		if (randomRot)
			g.transform.rotation = Random.rotation;
		else
			g.transform.rotation = Quaternion.Euler (0, Random.Range (0, 360), 0);
		var p = transform.position;
		g.transform.position = new Vector3(Xpos,p.y,p.z);

		g.transform.parent = waveMover;

		Invoke("spawnObject",Random.Range(repeatRate.x,repeatRate.y));
	}

	void IncreaseSpawnRate()
	{
		if (repeatRate.x > 0.2f) 
			repeatRate.x -= spawnRateIncrease;
		
		if (repeatRate.y > 0.3f) 
			repeatRate.y -= spawnRateIncrease;
	}

	void startIncreaseSpawn()
	{
		if(increaseSpawnrate)
			InvokeRepeating ("IncreaseSpawnRate", spawnIncreaseRate,spawnIncreaseRate);
	}
}