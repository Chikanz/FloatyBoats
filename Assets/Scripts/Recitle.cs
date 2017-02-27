using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine;

public class Recitle : MonoBehaviour
{
    public Animator playerAnim;

	public bool isPlayerOne = true;

	public float speedie = 0.5f;

	public float waveRad = 0.5f;
	public float waveForce = 0.5f;

	public float makeWaveDelay = 0.3f;

	public GameObject River;
    public Transform ReticleHitPoint;

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

    public Material mat;

    bool usingKeyboard = true;

	// Use this for initialization
	void Start () 
	{
        transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = mat;
        transform.GetChild(0).GetChild(1).GetComponent<MeshRenderer>().material = mat;
        startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
	    InputDevice controller = InputManager.ActiveDevice;

	    if (anyButton(controller) && usingKeyboard)
	    {
            usingKeyboard = false;
        }
        else if (Input.anyKey && !usingKeyboard)
	    {
            usingKeyboard = true;
	    }

        if (isPlayerOne && usingKeyboard)
            UpdateReticle(keyBoardThrow(true),
                keyBoardInputP1(false),
                keyBoardInputP1(true),
                isPlayerOne);
        else if(isPlayerOne && !usingKeyboard)
            UpdateReticle(controller.LeftTrigger.Value,
                    controller.LeftStickX,
                    controller.LeftStickY,
                    isPlayerOne);
        else if(!isPlayerOne && usingKeyboard)
            UpdateReticle(keyBoardThrow(false),
                keyBoardInputP2(false),
                keyBoardInputP2(true),
                isPlayerOne);
        else if(!isPlayerOne && !usingKeyboard)
            UpdateReticle(controller.RightTrigger.Value,
                    controller.RightStickX,
                    controller.RightStickY,
                    isPlayerOne);

    }

    private float keyBoardInputP1(bool horizontal)
    {
        if (horizontal)
        {
            if (Input.GetKey(KeyCode.W))
                return 1;
            if (Input.GetKey(KeyCode.S))
                return -1;
        }
        else
        {
            if (Input.GetKey(KeyCode.A))
                return -1;
            if (Input.GetKey(KeyCode.D))
                return 1;
        }
        return 0;
    }

    private float keyBoardInputP2(bool horizontal)
    {
        if (horizontal)
        {
            if (Input.GetKey(KeyCode.UpArrow))
                return 1;
            if (Input.GetKey(KeyCode.DownArrow))
                return -1;
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftArrow))
                return -1;
            if (Input.GetKey(KeyCode.RightArrow))
                return 1;
        }
        return 0;
    }

    private float keyBoardThrow(bool P1)
    {
        if (P1)
        {
            if (Input.GetKey(KeyCode.F))
                return 1.0f;
            else
                return 0.0f;
        }
        else
        {
            if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.RightShift))
                return 1.0f;
            else
                return 0.0f;
        }

    }

    void UpdateReticle(float triggerVal, float horizontal, float vertical, bool p1)
    {
        isEnabled = triggerVal > 0.5f;
        transform.GetChild(0).gameObject.SetActive(isEnabled);

        if (pastEnabled && !isEnabled)
        {
            if (RS.isThrowing) return;
            threwPos = transform.position;

            RS.spawnRock(ReticleHitPoint.position, rockSpeed, GetComponent<Recitle>());

            playerAnim.SetTrigger("Throw");

            //Invoke("makeRipple", rockSpeed);
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
        delta += new Vector3(horizontal, 0, vertical);
        if(usingKeyboard)
            delta.Normalize();
        transform.position += delta * speedie;

        //THE CLAMPS
        var p = transform.position;
        if (isPlayerOne)
        {
            transform.position = new Vector3(
                Mathf.Clamp(p.x, -999, River.transform.position.x),
                p.y,
                Mathf.Clamp(p.z, -999, 6.5f)
            );
        }
        else
        {
            transform.position = new Vector3(
                Mathf.Clamp(p.x, River.transform.position.x, 999),
                p.y,
                Mathf.Clamp(p.z, -999, 6.5f)
            );
        }
    }

    public bool anyButton(InputDevice c)
    {
        if (c.Action1.WasPressed ||
            c.Action2.WasPressed ||
            c.Action3.WasPressed ||
            c.Action4.WasPressed ||
            c.LeftTrigger.WasPressed||
            c.RightTrigger.WasPressed ||
            c.LeftStickX.Value > 0 ||
            c.LeftStickY.Value > 0
        )
        {
            return true;
        }
        return false;
    }

	public void makeRipple(Vector3 pos)
	{
	    GetComponent<AudioSource>().clip = splashSounds[UnityEngine.Random.Range(0, splashSounds.Length)];
        GetComponent<AudioSource>().Play();

	    //var pos = threwPos;
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

		    if (rb != null && hit.tag != "Pebble")
		    {
		        Vector3 colliderHitPoint = hit.ClosestPointOnBounds(threwPos);

                // Gets a vector that points from the player's position to the target's.
                //(from https://docs.unity3d.com/Manual/DirectionDistanceFromOneObjectToAnother.html)
                var heading = threwPos - hit.transform.position;
                var distance = heading.magnitude;
                var direction = heading / distance; // This is now the normalized direction.

                //var waveMulti = Map(distance, waveRad, 0.0f, 0.0f, 1.0f);
		        float waveMulti = waveRad - Mathf.Clamp(distance,0.0f,waveRad);
		        waveMulti = Map(waveMulti, 0.0f, waveRad, 0.0f, 1.0f);

                //Debug.Log("Distance:" + distance.ToString());
                //Debug.Log("WaveMulti:" + waveMulti.ToString());
		        //Debug.Log(direction*(waveForce*-waveMulti));

                rb.AddForceAtPosition(direction* (waveForce * -waveMulti),colliderHitPoint);
                //rb.AddForce(direction * (waveForce * -waveMulti));

            }
				//rb.AddExplosionForce(waveForce, threwPos, waveRad,0, ForceMode.Impulse);
		}
				
		//Invoke ("makeRipple", 0.5f); //tripple ripple
		//Invoke ("makeRipple", 0.7f);
	}

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(threwPos, waveRad);
        Gizmos.DrawWireSphere(transform.position, waveRad);
    }

    public float Map(float value, float OldMin, float OldMax, float NewMin, float NewMax)
    {
        float oldRange = (OldMax - OldMin);
        float newRange = (NewMax - NewMin);
        float newValue = (((value - OldMin) * newRange) / oldRange) + NewMin;

        return (newValue);
    }


}
