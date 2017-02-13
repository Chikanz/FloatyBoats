using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boat : MonoBehaviour
{

    private Rigidbody RB;
    public Transform River;
    public float shoreBounceForce;
    public float bounceBackForce = 10;

    public bool willBounce = true;

    public float bounceTimer = 0;


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
        //bounceTimer -= Time.deltaTime;
    }

    private void OnCollisionEnter(Collision c)
    {
        //if (c.gameObject.tag == "Floatie" && willBounce &&
        //    transform.position.z < c.transform.position.z)
        //{
        //    Debug.Log("floatie bouncy!");
        //    RB.AddForce(Vector3.back*bounceBackForce);
        //    CancelInvoke();
        //    GetComponent<ConstantForce>().force = Vector3.zero;
        //    Invoke("cancelCurrent", 3);
        //    bounceTimer = 1;
        //}
    }

    private void OnCollisionStay(Collision c)
    {
        bounceTimer += Time.deltaTime;

        if (c.gameObject.tag == "Ground" && willBounce && bounceTimer > 1)
        {
            //Bounce away from shore
            var p = transform.position;
            if (p.x < River.position.x)
                RB.AddForce(Vector3.right * shoreBounceForce);
            else
                RB.AddForce(Vector3.left * shoreBounceForce);

            bounceTimer = 0;
            Debug.Log("ground Bouncy!");
        }

        if (c.gameObject.tag == "Floatie" && willBounce && bounceTimer > 1)
        {
            Debug.Log("floatie bouncy!");
            RB.AddForce(Vector3.back*bounceBackForce);
            CancelInvoke();
            GetComponent<ConstantForce>().force = Vector3.zero;
            Invoke("cancelCurrent", 3);
            bounceTimer = 0;
        }
    }
}
