using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speeder : MonoBehaviour
{
    public WaterMover wm;
    public GameObject b;
    public waterSpawn ws;

    private float increaseClock = 0;
    public float increaseTime = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay(Collider c)
    {
        if (c.tag == "Boat")
        {
            increaseClock += Time.deltaTime;
            if (increaseClock > increaseTime)
            {
                b.GetComponent<ConstantForce>().force -= new Vector3(0, 0, 0.05f);
                wm.speed -= 0.03f;
                ws.repeat -= 0.5f;
                ws.repeat = Mathf.Clamp(ws.repeat,1,999);
                ws.updateRate = true;
                increaseClock = 0;
            }
        }
    }
}
