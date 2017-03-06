using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine;

public class pause : MonoBehaviour
{
    private MenuMan MM;
    private EndGame EG;
    private bool ispaused = false;
    private bool canPause = false;
	void Start ()
	{
	    MM = FindObjectOfType<MenuMan>();
        EG = FindObjectOfType<EndGame>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Skip an update after tutorial has ended to avoid pausing at the same time
        if (MM.isTutorial) return;
        else if(!canPause)
        {
            canPause = true;
            return;
        }

        var controller = InputManager.ActiveDevice;
        if (controller.MenuWasPressed && !EG.endGameUI.activeInHierarchy)
        {
            controller.Vibrate(0, 0);
            togglePause(!ispaused);
        }	
	}

    void togglePause(bool p)
    {
        ispaused = p;
        transform.GetChild(0).gameObject.SetActive(p);
        Time.timeScale = p ? 0 : 1;
    }
}
