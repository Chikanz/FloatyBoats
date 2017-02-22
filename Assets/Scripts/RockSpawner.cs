//Thrown rock spawner

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour {

	public GameObject rockObj;
	float time;
    public float spawnDelay = 0.3f;

	public bool isThrowing = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	public void spawnRock(Vector3 endPos, float speed, Recitle rec)
	{
		if (isThrowing) return;
        isThrowing = true;
	    StartCoroutine(SpawnDelayedRock(endPos, speed, rec, spawnDelay));
	}

    IEnumerator SpawnDelayedRock(Vector3 endPos, float speed, Recitle rec, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        
        var thrownRock = Instantiate(rockObj, transform.position, Random.rotation);
        Destroy(thrownRock, 3);
        var res = calculateBestThrowSpeed(transform.position, endPos, speed);
        thrownRock.GetComponent<Rigidbody>().AddForce(res, ForceMode.VelocityChange);
        thrownRock.GetComponent<pebble>().rec = rec;
        isThrowing = false;
        //Destroy (thrownRock, time);
    }

    //http://answers.unity3d.com/questions/248788/calculating-ball-trajectory-in-full-3d-world.html
    //OUR SAVIOR
    private Vector3 calculateBestThrowSpeed(Vector3 origin, Vector3 target, float timeToTarget) {
		// calculate vectors
		Vector3 toTarget = target - origin;
		Vector3 toTargetXZ = toTarget;
		toTargetXZ.y = 0;

		// calculate xz and y
		float y = toTarget.y;
		float xz = toTargetXZ.magnitude;

		// calculate starting speeds for xz and y. Physics forumulase deltaX = v0 * t + 1/2 * a * t * t
		// where a is "-gravity" but only on the y plane, and a is 0 in xz plane.
		// so xz = v0xz * t => v0xz = xz / t
		// and y = v0y * t - 1/2 * gravity * t * t => v0y * t = y + 1/2 * gravity * t * t => v0y = y / t + 1/2 * gravity * t
		float t = timeToTarget;
		float v0y = y / t + 0.5f * Physics.gravity.magnitude * t;
		float v0xz = xz / t;

		// create result vector for calculated starting speeds
		Vector3 result = toTargetXZ.normalized;        // get direction of xz but with magnitude 1
		result *= v0xz;                                // set magnitude of xz to v0xz (starting speed in xz plane)
		result.y = v0y;                                // set y to v0y (starting speed of y plane)

		return result;
	}
}
