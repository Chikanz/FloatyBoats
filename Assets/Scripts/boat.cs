using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boat : MonoBehaviour
{

    private Rigidbody RB;
    public Transform River;
    public float shoreBounceForce;
    public float bounceBackForce = 10;

    public bool willBounce = false;


    // Use this for initialization
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        InvokeRepeating("decideCurrent", 1, 1);
    }

    void cancelCurrent()
    {
        GetComponent<ConstantForce>().force = new Vector3(0, 0, -0.1f);
    }


    void decideCurrent()
    {
        if (Random.Range(0, 10) == 7)
        {
            var randDir = new Vector3(Random.Range(-0.1f, 0.1f),
                Random.Range(-0.1f, 0.1f),
                Random.Range(-0.1f, 0.1f));
            GetComponent<ConstantForce>().force += randDir;
        }

        Invoke("cancelCurrent", 3);
    }


    void Update()
    {

        //Random noise


        //Trying to GetType boat Touch float lol
        //RaycastHit hit;
        //Vector3 down = transform.TransformDirection(-Vector3.up);
        //if (Physics.Raycast (transform.position, down, out hit, 100, 0)) 
        //{
        //	var p = transform.position;
        //	transform.position = new Vector3 (p.x, hit.point.y, p.z);
        //}
        //
        //Debug.DrawRay(transform.position, down, Color.green);
    }

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Ground")
        {
            //Bounce away from shore
            var p = transform.position;
            if (p.x < River.position.x)
                RB.AddForce(Vector3.right*shoreBounceForce);
            else
                RB.AddForce(Vector3.left*shoreBounceForce);

            Debug.Log("Bouncy!");
        }

        if (c.gameObject.tag == "Floatie" && willBounce)
        {
            Debug.Log("bouncy!");
            RB.AddForce(Vector3.back*bounceBackForce);
            CancelInvoke();
            GetComponent<ConstantForce>().force = Vector3.zero;
            Invoke("cancelCurrent", 3);
        }
    }

    private void OnCollisionStay(Collision c)
    {
        if (c.gameObject.tag == "Floatie" && willBounce)
        {
            Debug.Log("bouncy!");
            RB.AddForce(Vector3.back*bounceBackForce);
            CancelInvoke();
            GetComponent<ConstantForce>().force = Vector3.zero;
            Invoke("cancelCurrent", 3);
        }
    }
}
