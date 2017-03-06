using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boat : MonoBehaviour
{

    private Rigidbody RB;
    public float shoreBounceForce;
    public float bounceBackForce = 10;

    public bool willBounce = true;

    public float bounceTimer = 0;

    public bool dummyBoat = false;
    public float yThresh = 0.481f;


    void BoatInit()
    {
        RB.constraints = RigidbodyConstraints.FreezePositionY |
            RigidbodyConstraints.FreezeRotationX |
            RigidbodyConstraints.FreezeRotationZ;
        RB.useGravity = false;

        dummyBoat = false;

        GetComponent<MeshCollider>().enabled = true;
        GetComponent<ConstantForce>().enabled = true;
        InvokeRepeating("decideCurrent", 1, 1);
    }

    void cancelCurrent()
    {
        if (dummyBoat) return;
        GetComponent<ConstantForce>().force = new Vector3(0, 0, -0.1f);
    }


    void decideCurrent()
    {
        if (dummyBoat) return;
        if (Random.Range(0, 10) == 7)
        {
            var randDir = new Vector3(Random.Range(-0.1f, 0.1f),
                Random.Range(-0.1f, 0.1f),
                Random.Range(-0.1f, 0.1f));
            GetComponent<ConstantForce>().force += randDir;
        }

        Invoke("cancelCurrent", 3);
    }

    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //if (dummyBoat && transform.GetChild(0).position.y <= yThresh)
        if (dummyBoat && transform.position.y <= yThresh)
        {
            //var p = transform.position;
            //transform.position = new Vector3(p.x, yThresh, p.y);
            BoatInit();
            
        }
    }

    void FixedUpdate()
    {
        var pos = transform.position;
        pos.y = Mathf.Clamp(transform.position.y, yThresh, 999);
        transform.position = pos;
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
        if (dummyBoat) return;

        bounceTimer += Time.deltaTime;

        if (c.gameObject.tag == "Ground" && willBounce && bounceTimer > 1)
        {
            //Bounce away from shore
            var p = transform.position;
            if (p.x < 0)
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
