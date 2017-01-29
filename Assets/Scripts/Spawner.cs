using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject[] SpawnedItems;

	public Transform ParentMover;

	public Transform bounds1;
	public Transform bounds2;

	public bool randomRot = false;
    public bool randomYRot = false;

    public Vector2 repeatRate = new Vector2(1,2);

	public bool increaseSpawnrate = false;
	public float spawnRateIncrease = 0.5f;
	public float spawnIncreaseRate = 5;

	// Use this for initialization
	void Start () 
	{
		spawnObject ();
        if(increaseSpawnrate)
		    Invoke ("startIncreaseSpawn", 10);

		//Time.timeScale = 3;
	}
	
	// Update is called once per frame
	void Update () 
	{
	    if (increaseSpawnrate)
	    {
	        repeatRate.x = Mathf.Clamp(repeatRate.x, 0.2f, 999);
	        repeatRate.y = Mathf.Clamp(repeatRate.y, 0.2f, 999);
	    }
	}

	void spawnObject()
	{
		var Xpos = Random.Range (bounds1.position.x, bounds2.position.x);
		var objToSpawn = Random.Range (0, SpawnedItems.Length);

		var g = Instantiate (SpawnedItems [objToSpawn], transform) as GameObject;

		if (randomRot)
			g.transform.rotation = Random.rotation;
		else if(randomYRot)
			g.transform.rotation = Quaternion.Euler (0, Random.Range (0, 360), 0);

		var p = transform.position;
		g.transform.position = new Vector3(Xpos,p.y,p.z);

		g.transform.parent = ParentMover;

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
