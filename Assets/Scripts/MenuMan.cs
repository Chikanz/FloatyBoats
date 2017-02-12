using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine;

public class MenuMan : MonoBehaviour
{
    public Animator TitleAnim;
    public Animator CreditsAnim;

    public GameObject boatyBoat;

    public GameObject fireflies;
    private Vector3 particleSpeed;

    private float groundSpeed;
    private float waterSpeed;

    public WaterMover waterMover;
    public WaterMover groundMover;

    public GameObject[] SpeechBubbles;
    public int speechCounter = 0;

    public Spawner[] Spawners;
    public waterSpawn[] WaterSpawners;

    public bool isTutorial = true;

    void Start()
    {
        Init();
    }

    void Init()
    {
        //Delayed starts
        Invoke("StartMessage", 5.3f);
        TitleAnim.SetTrigger("Reset");
        Invoke("IntroCredits", 2.1f);

        //Get speeds
        groundSpeed = groundMover.speed;
        waterSpeed = waterMover.speed;

        toggleSpawners(false);

        groundMover.speed = 0;
        waterMover.speed /= 2;

        //Particles
        fireflies.SetActive(false);

        boatyBoat.GetComponent<boat>().willBounce = false;
        boatyBoat.GetComponent<ConstantForce>().enabled = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        var controller = InputManager.ActiveDevice;

        if (controller.Action1.WasPressed && speechCounter < SpeechBubbles.Length - 1)
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

    void IntroCredits()
    {
        CreditsAnim.SetTrigger("Reset");
    }

    void toggleSpawners(bool b)
    {
        foreach (var s in Spawners)
            s.enabled = b;
        foreach (var ws in WaterSpawners)
            ws.enabled = b;
    }

    void ExitMenu()
    {
        groundMover.speed = groundSpeed;
        waterMover.speed = waterSpeed;

        toggleSpawners(true);

        fireflies.SetActive(true);

        boatyBoat.GetComponent<ConstantForce>().enabled = true;
        boatyBoat.GetComponent<boat>().willBounce = true;


        //Running animation

        //Menu animation
    }


}
