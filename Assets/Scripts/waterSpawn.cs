using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterSpawn : MonoBehaviour {
    
	public GameObject[] water;
	public Transform waterMover;
	public Vector3 rot; 

	public bool fixYdepth = true;
	public float repeat; //14 for exact chunk spawning

    // Use this for initialization
    void Start () 
	{
		//Time.timeScale = 2;

		spawnWater ();
		InvokeRepeating ("spawnWater", repeat, repeat);
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	void spawnWater()
	{
		var g = Instantiate (water[Random.Range(0,water.Length)],
            transform.position,
            Quaternion.identity) as GameObject;
		g.transform.rotation = Quaternion.Euler (rot);

		g.transform.parent = waterMover;
		if (fixYdepth) {
			var p = new Vector3 (0, Random.Range (-0.01f, 0.01f), 0);
			g.transform.position += p;
		}

	}
}
