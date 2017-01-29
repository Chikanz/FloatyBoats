using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimDelay : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	    var delay = Random.Range(0.0f, 1.0f);
        Invoke("playAgain", delay);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void playAgain()
    {
        GetComponent<Animator>().SetTrigger("Start");
    }
}
