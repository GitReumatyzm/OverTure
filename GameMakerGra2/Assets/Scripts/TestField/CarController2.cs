using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController2 : MonoBehaviour
{
    public Rigidbody SphereRB;

    private float SpeedInput;
    private float TurnInput;

    public float ForwardAccel = 8f;
    public float ReverseAccel = 4f;
    public float MaxSpeed = 50f;
    public float TurnStrength = 180f;
    public float GravityForce = 10f;

    private bool IsGrounded;

    public LayerMask WhatIsGround;
    public float GroundRayLength = .5f;
    public Transform GroundRayPoint;    

    void Start()
    {
        SphereRB.transform.parent = null;
    }

    void Update()
    {
        SpeedInput = 0f;
        if(Input.GetAxis("Vertical") > 0)
        {
            SpeedInput = Input.GetAxis("Vertical") * ForwardAccel * 1000f;
        }
        else if(Input.GetAxis("Vertical") < 0)
        {
            SpeedInput = Input.GetAxis("Vertical") * ForwardAccel * 1000f;
        }

        TurnInput = Input.GetAxis("Horizontal");

        if(IsGrounded)
        {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, TurnInput * TurnStrength * Time.deltaTime * Input.GetAxis("Vertical"), 0f));

        transform.position = SphereRB.transform.position;
        }
    }

    void FixedUpdate()
    {
        IsGrounded = false;
        RaycastHit hit; 
        
        if(Physics.Raycast(GroundRayPoint.position, -transform.up, out hit, GroundRayLength, WhatIsGround))
        {
            IsGrounded = true;
        }

        if(IsGrounded)
        {
               if(Mathf.Abs(SpeedInput) > 0)
                {
                   SphereRB.AddForce(transform.forward * SpeedInput);
                }
        }
        else
        {
            SphereRB.AddForce(Vector3.up * -GravityForce * 100f);
        }
               
    }
}
