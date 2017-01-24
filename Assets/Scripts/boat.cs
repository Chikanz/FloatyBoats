using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boat : MonoBehaviour
{
    private Rigidbody RB;

	// Use this for initialization
	void Start () 
	{
		RB = GetComponent<Rigidbody> ();
	}
	

	void Update () 
	{
			
        //Random noise


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
