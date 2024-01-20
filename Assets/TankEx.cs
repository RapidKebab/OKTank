using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEx : MonoBehaviour {

	public WheelCollider [] leftWheels;
	public WheelCollider [] rightWheels;

	WheelCollider cLeft;
	WheelCollider cRight;

	public float power; //total power sent to each side of the vehicle
	public float autoStopBrakeForce; //stops unpowered tracks from just rolling with the other side to better turn.
    public bool wasd = false; //enable for WASD, disable for Tank Drive
    float leftPower = 0;
    float rightPower = 0;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); //This is used for brakeforce calculations as when the tank is stuck in place, moving only one track didn't allow the tank to move at all, undesirable behavior.
    }

    void FixedUpdate () {
        //Calculating input and converting it to values for the tracks to take

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

        //Applying appropriate amounts of power and/or brake force to the tracks. Assumes the vehicle has the same number of wheels each side.
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
