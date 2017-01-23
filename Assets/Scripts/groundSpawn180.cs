using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundSpawn180 : MonoBehaviour {

	public GameObject water;
	public Transform waterMover;

	public float repeat;

	// Use this for initialization
	void Start () 
	{
		spawnWater ();
		InvokeRepeating ("spawnWater", repeat, repeat);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void spawnWater()
	{
		var g = Instantiate (water, transform.position, Quaternion.Euler(0,0,0)) as GameObject;
		g.transform.parent = waterMover;
		var p = new Vector3 (0, -1f,0 );
		g.transform.position += p;

	}
}
