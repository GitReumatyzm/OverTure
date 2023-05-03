using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController2 : MonoBehaviour
{
    private float moveInput;
    public float forwardSpeed;
    public Rigidbody SphereRB;

    void Update()
    {
       moveInput = Input.GetAxisRaw("Vertical");
       moveInput *= forwardSpeed;

        transform.position =  SphereRB.transform.position;
    }

    void FixedUpdate()
    {
        SphereRB.AddForce(transform.forward * moveInput, ForceMode.Acceleration);
    }
}
