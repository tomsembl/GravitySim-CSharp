using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class TwoBodiesGravity : MonoBehaviour
{
    public float gMultiplier;
    public Vector3 initialVelocity1;
    public Vector3 initialVelocity2;
    public Rigidbody rb1;
    public Rigidbody rb2;
    private bool hasStartedForRealz = false;

    // applies force of gravity
    void Gravity(Rigidbody rb1, Rigidbody rb2, float timeDelta) {
        float distance = Vector3.Distance(rb1.transform.position,rb2.transform.position);
        Vector3 positionDifference = rb1.position - rb2.position;
        float gForce = (gMultiplier * rb1.mass * rb2.mass) / (distance*distance);
        Vector3 gravityVector = positionDifference.normalized * gForce * timeDelta ;
        rb1.AddForce(gravityVector);
    }

    void applyInitialVelocity(Rigidbody rb, Vector3 initialVelocity) {
        rb.AddForce(initialVelocity);
    }

    public void faster(int fastOrSlow) {
        rb1.AddForce(rb2.velocity * -1 * fastOrSlow);
        rb2.AddForce(rb2.velocity * fastOrSlow);
    }

    public void addForce(float fl) {
        rb2.AddForce(new Vector3(fl, 0.0f, fl) );
    }

    public void changeGravity(float fl) {
        gMultiplier = fl;
    }


    // Start is called before the first frame update
    void Start() {
        rb1.angularVelocity = Vector3.down * 1.0f;
        rb2.angularVelocity = Vector3.down * 4.0f;
    }

    // Update is called once per frame
    void Update() {
        float timeDelta = Time.deltaTime;
        if (Time.frameCount < 10) { /*Debug.Log("long");*/ } else {
            if (!hasStartedForRealz) {
                hasStartedForRealz = true;
                applyInitialVelocity(rb1, initialVelocity1);
                applyInitialVelocity(rb2, initialVelocity2);
            } else {
                Gravity(rb1, rb2, timeDelta);
                Gravity(rb2, rb1, timeDelta);
            }
        }
    }

    
}
