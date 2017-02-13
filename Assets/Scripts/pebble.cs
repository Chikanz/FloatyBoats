using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pebble : MonoBehaviour
{
    public Recitle rec;
    private float childTimer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Water")
        {
            rec.makeRipple(transform.position);
            Destroy(gameObject);
        }

    }

    private void OnCollisionStay(Collision c)
    {
        childTimer += Time.deltaTime;
        if (childTimer > 1)
        {
            if (c.gameObject.tag == "Ground")
                transform.parent = c.transform;
        }
    }
}
