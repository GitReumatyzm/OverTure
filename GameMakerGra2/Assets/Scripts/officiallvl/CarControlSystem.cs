using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControlSystem : MonoBehaviour
{
    public Rigidbody SphereRB;

    private float SpeedInput;
    private float TurnInput;

    public float ForwardAccel = 8f;
    public float ReverseAccel = 4f;
    public float MaxSpeed = 50f;
    public float TurnStrength = 180f;
    public float GravityForce = 10f;
    public float DragOnGround = 3f;
    public float GroundRayLength = .5f;
    public float MaxWheelTurn = 25f;
    public float BoostDuration = 2f;
    private float BoostTimer = 0f;

    private bool IsGrounded;

    public LayerMask WhatIsGround;

    public Transform GroundRayPoint;
    public Transform LeftFrontWheel;
    public Transform RightFrontWheel;
    

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
        }
        
        //LeftFrontWheel.localRotation = Quaternion.Euler(LeftFrontWheel.localRotation.eulerAngles.x, (TurnInput * MaxWheelTurn) - 180, LeftFrontWheel.localRotation.eulerAngles.z);
        //RightFrontWheel.localRotation = Quaternion.Euler(RightFrontWheel.localRotation.eulerAngles.x, TurnInput * MaxWheelTurn, RightFrontWheel.localRotation.eulerAngles.z);

        transform.position = SphereRB.transform.position;

        Nitro();
    }

    void FixedUpdate()
    {
        IsGrounded = false;
        RaycastHit hit; 
        
        if(Physics.Raycast(GroundRayPoint.position, -transform.up, out hit, GroundRayLength, WhatIsGround))
        {
            IsGrounded = true;

            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }

        if(IsGrounded)
        {
            SphereRB.drag = DragOnGround;

               if(Mathf.Abs(SpeedInput) > 0)
                {
                   SphereRB.AddForce(transform.forward * SpeedInput);
                }
        }
        else
        {
            SphereRB.drag = 0.1f;
            SphereRB.AddForce(Vector3.up * -GravityForce * 100f);
        }  
    
    }

    void Nitro()
    {
        if (Input.GetKey(KeyCode.LeftShift) && BoostTimer <= 0f)
         {
            BoostTimer = BoostDuration;
         }

         if (BoostTimer > 0f)
         {
            SpeedInput = Input.GetAxis("Vertical") * ForwardAccel * 1000f * 2f; 

            BoostTimer -= Time.deltaTime;

               if (BoostTimer <= 0f)
               {
                   SpeedInput = 0f;
               }
        }
         else
        {
            SpeedInput = Input.GetAxis("Vertical") * ForwardAccel * 1000f;
        }
    }
      
}
