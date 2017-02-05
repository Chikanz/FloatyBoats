using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine;

public class MenuMan : MonoBehaviour
{
    public Animator TitleAnim;
    public Animator Credit;
    public Animator SpeechAnim;

    public GameObject[] SpeechBubbles;
    public int speechCounter = 0;

    // Use this for initialization
    void Start ()
    {
        Invoke("StartMessage", 1);
    }
	
	// Update is called once per frame
	void Update ()
    {
        var controller = InputManager.ActiveDevice;

        if (controller.Action1.WasPressed && speechCounter < SpeechBubbles.Length)
        {
            for (int i = 0; i < SpeechBubbles.Length; i++)
            {
                SpeechBubbles[i].SetActive(false);
            }
            speechCounter++;
            SpeechBubbles[speechCounter].SetActive(true);
        }
    }

    void StartMessage()
    {
        SpeechBubbles[speechCounter].SetActive(true);
    }

    void ExitMenu()
    {
        
    }


}
