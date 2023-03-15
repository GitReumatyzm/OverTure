using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
   private const string Horizontal = "Horizontal";
   private const string Vertical = "Vertical";

   private float HorizontalInput;
   private float VerticalInput;
   private float CurrentSteerAngle;
   private float CurrentBreakForce;
   private bool IsBreaking;

   [SerializeField] private float MotorForce;
   [SerializeField] private float BreakForce;
   [SerializeField] private float MaxSteerAngle;

   [SerializeField] private WheelCollider FrontLeftWheelCollider;
   [SerializeField] private WheelCollider FrontRightWheelCollider;
   [SerializeField] private WheelCollider RearLeftWheelCollider;
   [SerializeField] private WheelCollider RearRightWheelCollider;

   [SerializeField] private Transform FrontLeftWheelTransform;
   [SerializeField] private Transform FrontRightWheelTransform;
   [SerializeField] private Transform RearLeftWheelTransform;
   [SerializeField] private Transform RearRightWheelTransform;


   private void FixedUpdate()
   {
      GetInput();
      HandleMotor();
      HandleSteering();
      UpdateWheels();
   }

   private void HandleMotor()
   {
     FrontLeftWheelCollider.motorTorque = VerticalInput * MotorForce;
     FrontRightWheelCollider.motorTorque = VerticalInput * MotorForce;
     CurrentBreakForce = IsBreaking ? BreakForce : 0f;
     if(IsBreaking)
     {
        ApplyBreaking();
     }
   }

    private void ApplyBreaking()
    {
       FrontLeftWheelCollider.brakeTorque = CurrentBreakForce;
       FrontRightWheelCollider.brakeTorque = CurrentBreakForce;
       RearLeftWheelCollider.brakeTorque = CurrentBreakForce;
       RearRightWheelCollider.brakeTorque = CurrentBreakForce;
    }
   

    private void GetInput()
    {
      HorizontalInput = Input.GetAxis(Horizontal);
      VerticalInput = Input.GetAxis(Vertical);
      IsBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleSteering()
    {
        CurrentSteerAngle = MaxSteerAngle * HorizontalInput;
        FrontLeftWheelCollider.steerAngle = CurrentSteerAngle;
        FrontRightWheelCollider.steerAngle = CurrentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(FrontLeftWheelCollider, FrontLeftWheelTransform);
        UpdateSingleWheel(FrontRightWheelCollider, FrontRightWheelTransform);
        UpdateSingleWheel(RearLeftWheelCollider, RearLeftWheelTransform);
        UpdateSingleWheel(RearRightWheelCollider, RearRightWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider WheelCollider, Transform WheelTransform)
    {
        Vector3 Pos;
        Quaternion Rot; 
        WheelCollider.GetWorldPose(out Pos, out Rot);
        WheelTransform.position = Pos;
        WheelTransform.rotation = Rot;
    }

}
