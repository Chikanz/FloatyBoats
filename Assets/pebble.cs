using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pebble : MonoBehaviour
{
    public Recitle rec;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider c)
    {
        Debug.Log(c.gameObject);
        Debug.Assert(rec != null);
        if (c.tag == "Water")
            rec.makeRipple(transform.position);
    }
}
