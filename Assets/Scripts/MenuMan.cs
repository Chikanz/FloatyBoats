using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine;

public class MenuMan : MonoBehaviour
{
    public bool debugMode = false;

    public Animator TitleAnim;
    public Animator CreditsAnim;
    public Animator CameraAnim;

    public Animator Player1Anim;
    public Animator Player2Anim;

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

        boatyBoat.GetComponent<boat>().willBounce = true;
        boatyBoat.GetComponent<ConstantForce>().enabled = false;

        if (debugMode)
        {
            transform.GetChild(1).gameObject.SetActive(false);
            ExitMenuInit();
        }
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

        if (controller.MenuWasPressed && isTutorial)
        {

            boatyBoat.transform.position = new Vector3(-1.77f, 0.481f, 4.18f);
            ExitMenuInit();
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


    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Boat" && isTutorial)
        {
            ExitMenuInit();
        }
    }

    void ExitMenuInit()
    {
        isTutorial = false;
        //Talkies parent
        SpeechBubbles[0].transform.parent.gameObject.SetActive(false);

        //Tutorial marker
        transform.GetChild(0).gameObject.SetActive(false);

        TitleAnim.SetTrigger("MenuExit");
        CreditsAnim.SetTrigger("MenuExit");
        Invoke("ExitMenu", 2);
    }

    void ExitMenu()
    { 

        //BGM
        transform.GetChild(1).parent = waterMover.GetComponent<Transform>();
        CameraAnim.SetTrigger("MenuExit");

        //Close all speech bubbles
        for (int i = 0; i < SpeechBubbles.Length; i++)
        {
            SpeechBubbles[i].SetActive(false);
        }

        //Set mover speeds
        groundMover.speed = groundSpeed;
        waterMover.speed = waterSpeed;
        
        toggleSpawners(true);

        fireflies.SetActive(true);

        boatyBoat.GetComponent<ConstantForce>().enabled = true;
        boatyBoat.GetComponent<boat>().willBounce = true;

        //Running animation
        Player1Anim.SetTrigger("Run");
        Player2Anim.SetTrigger("Run");
    }


}
