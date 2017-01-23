using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {

	public GameObject endGame;
	public GameObject boat;
	public GameObject endGameUI;

	public float score;

	public Text scoreText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		score += Time.deltaTime;
		//Debug.Log (score);

		if (endGameUI.activeInHierarchy == true) {
			if (Input.GetButtonDown ("Fire2")) {
				SceneManager.LoadScene("gamescene");
			}
				
		}

	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject == boat){
			Destroy (boat.gameObject);
			endGameUI.SetActive(true);
			scoreText.text = score.ToString("0");
			Debug.Log ("endgame");
		}
	}
}
