using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {

	public GameObject endGameUI;
    public Text scoreText;

    public GameObject Boat;

    private float score;

    private MenuMan MM;

    private InputDevice Controller;

    public RockSpawner RS;
    public Transform boatSpawn;
    public Recitle R;

	// Use this for initialization
	void Start ()
    {
        MM = FindObjectOfType<MenuMan>();
    }
	
	// Update is called once per frame
	void Update ()
	{
	    Controller = InputManager.ActiveDevice;

        if(!MM.isTutorial)
            score += Time.deltaTime;
		//Debug.Log (score);

		if (endGameUI.activeInHierarchy)
        {
			if (Controller.Action1.WasPressed)
            {
                //SceneManager.LoadScene("gamescene");
                //Chunk muncher
                var chunks = GameObject.FindGameObjectsWithTag("Chunk");
                foreach (var c in chunks)
                    Destroy(c);

                var debris = GameObject.FindGameObjectsWithTag("Debris");
                foreach (var d in debris)
                    Destroy(d);

                endGameUI.SetActive(false);
                score = 0;
                RS.resetThrow(boatSpawn.position, R, 1f);
            }
				
		}

	}

	void OnTriggerEnter(Collider other)
    {
		if (other.tag == "Boat")
        {
			Destroy(other.gameObject);

			endGameUI.SetActive(true);
			scoreText.text = "Score: " + score.ToString("0");
			Debug.Log ("endgame");
		}
	}
}
