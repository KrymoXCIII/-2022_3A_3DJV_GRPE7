using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;





    


     
public class CarControllerP1 : MonoBehaviour {
    
    
    
    public List<AxleInfo> axleInfos; 
    public float maxMotorTorque;
    public float maxSteeringAngle;
    public float motor;
    public float steering;
    


    

    private static void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0) {
            return;
        }
     
        var visualWheel = collider.transform.GetChild(0);

        collider.GetWorldPose(out var position, out var rotation);
        rotation = rotation * Quaternion.Euler(new Vector3(0, 0, 90));

        var transform1 = visualWheel.transform;
        transform1.position = position;
        transform1.rotation = rotation;
    }
     
    public void FixedUpdate()
    {
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            motor = maxMotorTorque * 1;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            motor = maxMotorTorque * -1;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            steering = maxSteeringAngle * 1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            steering = maxSteeringAngle * -1;
        }
        

       
     
        foreach (var axleInfo in axleInfos) {
            if (axleInfo.steering) {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor) {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);
        }
        
        steering = 0;
        motor = 0;
    }
}