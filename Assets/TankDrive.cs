using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankDrive : MonoBehaviour {

	public WheelCollider [] leftWheels;
	public WheelCollider [] rightWheels;

	WheelCollider cLeft;
	WheelCollider cRight;

	public float power;
	public float autoStopBrakeForce;
    public bool wasd = true;
    float leftPower = 0;
    float rightPower = 0;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate () {
        if (!wasd)
        {
            leftPower = Input.GetAxis("Left");
            rightPower = Input.GetAxis("Right");
        }
        else {
            leftPower = Input.GetAxis("Vertical") + Input.GetAxis("Horizontal");
            rightPower = Input.GetAxis("Vertical") - Input.GetAxis("Horizontal");

            if (Mathf.Abs(leftPower) > 1)
                leftPower = Mathf.Abs(leftPower) * (1/leftPower);
            if (Mathf.Abs(rightPower) > 1)
                rightPower = Mathf.Abs(rightPower) * (1 / rightPower);
        }
		for (int i = 0; i < leftWheels.Length; i++){
    		cLeft=leftWheels[i];
    		cLeft.motorTorque = (power* leftPower / leftWheels.Length);
    		if(cLeft.motorTorque==0 && rb.velocity.magnitude < 0.2)
    			cLeft.brakeTorque=autoStopBrakeForce;
    		else
    			cLeft.brakeTorque=0;
    
    		cRight=rightWheels[i];
    		cRight.motorTorque = (power* rightPower/ rightWheels.Length);
    		if(cRight.motorTorque==0 && rb.velocity.magnitude>0.2)
    			cRight.brakeTorque=autoStopBrakeForce;
    		else
    			cRight.brakeTorque=0;
		}
	}
}
