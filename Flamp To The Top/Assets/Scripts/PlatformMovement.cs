using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour {

	public SliderJoint2D platformSlider;
	public JointMotor2D platformMotor;

	float motorSpeedPositive; 
	float motorSpeedNegative;

	void Start () {
		platformMotor = platformSlider.motor;	
		motorSpeedPositive = Random.Range(1.0f, 2.0f);
		motorSpeedNegative = Random.Range(-1.0f, -2.0f);
	}

	// Se chegar no limite inferior maximo limite superior máximo troca o sentido do movimento
	void Update () {
		if (platformSlider.limitState == JointLimitState2D.LowerLimit) {
			platformMotor.motorSpeed = motorSpeedPositive;
			platformSlider.motor = platformMotor;
		}

		if (platformSlider.limitState == JointLimitState2D.UpperLimit) {
			platformMotor.motorSpeed = motorSpeedNegative;
			platformSlider.motor = platformMotor;
		}
	}
}
