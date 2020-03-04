using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leonard;

namespace Leonard
{
    public class CarController : MonoBehaviour
    {
        public float maxSteerAngle = 42;
        public float motorForce = 800;
        float steeringAngle;

        public WheelCollider frontRight, frontLeft, backRight, backLeft;
        public Transform frontRightT, frontLeftT, backRightT, backLeftT;

        //Do not directly include player inputs, as this class is used both by the player and the algorithm
        public float horizontalInput;
        public float verticalInput;

        Vector3 wheelPos;
        Quaternion wheelQuat;

        public Rigidbody rb;
        public Vector3 centerOfMassPos = new Vector3(0.00923f, 0.02833f, -0.06784f);

        private void Start()
        {
            rb.centerOfMass = centerOfMassPos;
        }

        void FixedUpdate()
        {
            Steer();
            Accelerate();
            WheelVisuals();
        }

        void Steer()
        {
            steeringAngle = horizontalInput * maxSteerAngle;

            //apply steeringAngle to the wheel collider components
            frontRight.steerAngle = steeringAngle;
            frontLeft.steerAngle = steeringAngle;
        }

        void Accelerate()
        {
            //apply input value as acceleration
            backRight.motorTorque = verticalInput * motorForce;
            backLeft.motorTorque = verticalInput * motorForce;
        }

        void WheelVisuals()
        {
            UpdateWheelPos(frontRight, frontRightT);
            UpdateWheelPos(frontLeft, frontLeftT);
            UpdateWheelPos(backRight, backRightT);
            UpdateWheelPos(backLeft, backLeftT);
        }

        private void UpdateWheelPos(WheelCollider wheelCollider, Transform tr)
        {
            wheelPos = tr.position;
            wheelQuat = tr.rotation;

            //using the "out" function retroactively updates the values of pos and quat
            wheelCollider.GetWorldPose(out wheelPos, out wheelQuat);

            //assign
            tr.position = wheelPos;
            tr.rotation = wheelQuat;
        }
    }
}