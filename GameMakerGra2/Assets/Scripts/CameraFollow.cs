using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 Offset;
    [SerializeField] private Transform Target;
    [SerializeField] private float TranslateSpeed;
    [SerializeField] private float RotationSpeed;


    private void FixedUpdate()
    {
        HandleTranslation();
        HandleRotation();
    }
  
    private void HandleTranslation()
    {
       var TargetPosition = Target.TransformPoint(Offset);
       transform.position = Vector3.Lerp(transform.position, TargetPosition, TranslateSpeed * Time.deltaTime);
    }

    private void HandleRotation()
    {
        var Direction = Target.position - transform.position;
        var Rotation = Quaternion.LookRotation(Direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, Rotation, RotationSpeed * Time.deltaTime);
    }
}
