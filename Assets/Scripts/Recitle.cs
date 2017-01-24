using System.Collections;
using System.Collections.Generic;
using InControl;
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

    public AudioClip[] splashSounds;


	// Use this for initialization
	void Start () 
	{
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
	    var controller = InputManager.ActiveDevice;

        if (isPlayerOne)
            UpdateReticle(controller.LeftTrigger.Value,
                controller.LeftStickX,
                controller.LeftStickY,
                isPlayerOne);
        else
            UpdateReticle(controller.RightTrigger.Value,
                controller.RightStickX,
                controller.RightStickY,
                isPlayerOne);

    }

    void UpdateReticle(float triggerVal, float horizontal, float vertical, bool p1)
    {
        isEnabled = triggerVal > 0.5f;
        transform.GetChild(0).gameObject.SetActive(isEnabled);

        if (pastEnabled && !isEnabled)
        {
            threwPos = transform.position;

            RS.spawnRock(transform.position, rockSpeed, GetComponent<Recitle>());

            Invoke("makeRipple", rockSpeed);
            pastEnabled = isEnabled;
            transform.position = startPos;
        }

        if (!pastEnabled && isEnabled)
        {
            transform.position = startPos;
        }

        pastEnabled = isEnabled;

        //Movement
        var delta = Vector3.zero;
        //Limit x movement
        if (isPlayerOne)
        {
            if (transform.position.x <= River.transform.position.x)
            {
                delta += new Vector3(horizontal, 0, 0);
            }
            else
            {
                var p = transform.position;
                transform.position = new Vector3(River.transform.position.x, p.y, p.z);
            }
        }
        else
        {
            if (transform.position.x >= River.transform.position.x)
            {
                delta += new Vector3(horizontal, 0, 0);
            }
            else
            {
                var p = transform.position;
                transform.position = new Vector3(River.transform.position.x, p.y, p.z);
            }
        }

        delta += new Vector3(0, 0, vertical);
        transform.position += delta * speedie;
    }

	public void makeRipple()
	{
	    GetComponent<AudioSource>().clip = splashSounds[Random.Range(0, splashSounds.Length)];
        GetComponent<AudioSource>().Play();

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
