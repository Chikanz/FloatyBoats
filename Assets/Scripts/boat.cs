using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boat : MonoBehaviour {

	public Transform River;
	Rigidbody RB;
	public float correctionForce = 5;
	public float deadZone = 5;


	// Use this for initialization
	void Start () 
	{
		RB = GetComponent<Rigidbody> ();
	}
	

	void Update () 
	{

		//Rot Correction, over compensates :c
		//if (transform.rotation.y < 0 + deadZone) {			
		//	RB.AddTorque (new Vector3 (0, correctionForce * RB.velocity.magnitude, 0));
		//	Debug.Log ("correcting left");
		//}
		//
		//else if (transform.rotation.y > 0 + deadZone) {
		//	RB.AddTorque (new Vector3 (0, -correctionForce * RB.velocity.magnitude, 0));
		//	Debug.Log ("correcting Right");
		//}
			
		//Trying to GetType boat Touch float lol
		//RaycastHit hit;
		//Vector3 down = transform.TransformDirection(-Vector3.up);
		//if (Physics.Raycast (transform.position, down, out hit, 100, 0)) 
		//{
		//	var p = transform.position;
		//	transform.position = new Vector3 (p.x, hit.point.y, p.z);
		//}
		//
		//Debug.DrawRay(transform.position, down, Color.green);
	}
		
}
