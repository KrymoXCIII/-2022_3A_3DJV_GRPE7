using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;

[System.Serializable]


public class AxleInfo {
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
    

}

public class CarControllerP2 : MonoBehaviour
{



    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;
    public float motor;
    public float steering;




    private static void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
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

        


        if (Input.GetKey("z"))
        {
            motor = maxMotorTorque * 1;
        }
        else if (Input.GetKey("s"))
        {
            motor = maxMotorTorque * -1;
        }
        
        
        if (Input.GetKey("d"))
        {
            steering = maxSteeringAngle * 1;
        }
        else if (Input.GetKey("q"))
        {
            steering = maxSteeringAngle * -1;
        }
        



        foreach (var axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }

            if (axleInfo.motor)
            {
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