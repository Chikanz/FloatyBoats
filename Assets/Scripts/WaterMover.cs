using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMover : MonoBehaviour 
{
	public float speed = 0.1f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		var p = new Vector3 (0, 0, speed);
		transform.position += p * Time.deltaTime;
	}
}
