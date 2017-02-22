using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {

	public GameObject endGameUI;
    public Text scoreText;

    private float score;

    private InputDevice Controller;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update ()
	{
	    Controller = InputManager.ActiveDevice;

        score += Time.deltaTime;
		//Debug.Log (score);

		if (endGameUI.activeInHierarchy)
        {
			if (Controller.Action1.WasPressed) {
				SceneManager.LoadScene("gamescene");
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
