using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recitle : MonoBehaviour 
{
	public bool isPlayerOne = true;

	public float speedie = 0.5f;

	public float waveRad = 0.5f;
	public float waveForce = 0.5f;

	public float makeWaveDelay = 0.3f;

	public GameObject River;

	bool isEnabled = false;
	bool pastEnabled = false;

	public GameObject ripple;

	public RockSpawner RS;
	public float rockSpeed;

	public Vector3 threwPos;
	public bool isThrowing = false;

	Vector3 startPos;

	public Vector3 RippleposOffset;


	// Use this for initialization
	void Start () 
	{
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isPlayerOne) 
		{
			isEnabled = Input.GetAxis ("Axis 5") > 0.5f;
			transform.GetChild(0).gameObject.SetActive(isEnabled);

			if (pastEnabled == true && isEnabled == false) 
			{
				threwPos = transform.position;

				RS.spawnRock (transform.position,rockSpeed);

				Invoke("makeRipple",rockSpeed);
				pastEnabled = isEnabled;
				transform.position = startPos;
			}

			if (pastEnabled == false && isEnabled == true) 
			{
				transform.position = startPos;
			}

			pastEnabled = isEnabled;

			//Movement
			var delta = Vector3.zero;
			//Limit x movement
			if (transform.position.x <= River.transform.position.x) {
				delta += new Vector3 (Input.GetAxis ("Horizontal"), 0, 0);
			} else {
				var p = transform.position;
				transform.position = new Vector3 (River.transform.position.x, p.y, p.z);
			}

			delta += new Vector3 (0, 0, Input.GetAxis ("Vertical"));
			transform.position += delta * speedie;
		}

		else if (!isPlayerOne) 
		{
			if (pastEnabled == true && isEnabled == false) 
			{
				threwPos = transform.position;
				RS.spawnRock (transform.position,rockSpeed);
				pastEnabled = isEnabled;
				transform.position = startPos;
				Invoke("makeRipple",rockSpeed);
			}
			
			if (pastEnabled == false && isEnabled == true) 
			{
				transform.position = startPos;
			}
			
			pastEnabled = isEnabled;

			isEnabled = Input.GetAxis ("Axis 6") > 0.5f;
			transform.GetChild(0).gameObject.SetActive(isEnabled);

			//Movement
			var delta = Vector3.zero;
			//Limit x movement
			if (transform.position.x >= River.transform.position.x) {
				delta += new Vector3 (Input.GetAxis ("Axis 3"), 0, 0);
			} else {
				var p = transform.position;
				transform.position = new Vector3 (River.transform.position.x, p.y, p.z);
			}

			delta += new Vector3 (0, 0, -Input.GetAxis ("Axis 4"));
			transform.position += delta * speedie;
		}
	}

	void makeRipple()
	{
		GetComponent<AudioSource> ().Play ();
		var pos = threwPos;
		var rip = Instantiate (ripple,pos - RippleposOffset,Quaternion.identity) as GameObject;
		Destroy (rip, 3);
		var rot = new Vector3(
			0,
			Random.Range(0,360),//~~~~~~~
			0);

		rip.transform.Rotate(rot);

		wavePush ();
		//Invoke("wavePush", 0.3f);
	}

	public void wavePush()
	{
		Debug.Log ("pusing at " + threwPos);
		Collider[] colliders = Physics.OverlapSphere(threwPos, waveRad);

		foreach (Collider hit in colliders) 
		{
			Rigidbody rb = hit.GetComponent<Rigidbody>();

			if (rb != null)
				rb.AddExplosionForce(waveForce, threwPos, waveRad, 3.0F);
		}
				
		//Invoke ("makeRipple", 0.5f); //tripple ripple
		//Invoke ("makeRipple", 0.7f);
	}


}
