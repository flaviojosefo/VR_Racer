﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class CarController : MonoBehaviour {

    [SerializeField] private Transform steeringWheel;
    [SerializeField] private Transform neck;

    [SerializeField] private List<WheelCollider> wheels;
    [SerializeField] private List<GameObject> wheelsObject;

    [SerializeField] private KeyCode resetKey;

    public float topSpeed = 250f; //the top speed
    public float maxTorque = 200f; //the maximum torque to apply to wheels
    public float maxSteerAngle = 45f;
    public float currentSpeed;
    public float maxBrakeTorque = 2200;

    private float Direction;
    private float Forward; //forward axis
    private float BackWards; //backward axis
    private float Turn; //turn axis
    private float Brake; //brake axis

    private VRTK_ControllerEvents events;

    private void Start() {

        steeringWheel = steeringWheel.GetChild(0);
        
        foreach(WheelCollider w in wheels) {

            w.ConfigureVehicleSubsteps(5, 12, 15);
        }

        events = GetComponent<VRTK_ControllerEvents>();
    }

    void FixedUpdate() {

        if (events.triggerTouched) {  // NEEDS TESTING

            Forward = events.GetTriggerAxis();
            BackWards = events.GetTriggerAxis();  // how do we get the correct hand (left)?

            Direction = Forward - BackWards;

        } else {

            Forward = Input.GetAxis("Vertical");

            Direction = Forward;
        }

        Turn = ConvertSteeringAngle(GetSteeringAngle());
        Brake = Input.GetAxis("Jump");

        wheels[0].steerAngle = maxSteerAngle * Turn;
        wheels[1].steerAngle = maxSteerAngle * Turn;

        currentSpeed = 2 * 22 / 7 * wheels[2].radius * wheels[2].rpm * 60 / 1000; // Formula for calculating speed in km/h

        if (currentSpeed < topSpeed) {

            wheels[2].motorTorque = maxTorque * Direction; // Move the wheels on back left...
            wheels[3].motorTorque = maxTorque * Direction; // ...and back right

        } else {

            wheels[2].motorTorque = 0.0f;
            wheels[3].motorTorque = 0.0f;
        }

        foreach (WheelCollider w in wheels) {

            w.brakeTorque = maxBrakeTorque * Brake;
        }
    }

    void Update() {

        ResetCar();

        for (int i = 0; i < wheels.Count; i++) {

            wheels[i].GetWorldPose(out Vector3 pos, out Quaternion rot);
            wheelsObject[i].transform.position = pos;
            wheelsObject[i].transform.rotation = rot;
        }
    }

    private void ResetCar() {

        if (Input.GetKeyDown(resetKey)) {

            transform.rotation = Quaternion.identity;
            neck.localRotation = Quaternion.identity;
        }
    }

    private float GetSteeringAngle() {

        float angle = steeringWheel.localRotation.eulerAngles.y;

        if (steeringWheel.localRotation.eulerAngles.y > 180) {

            angle -= 360;

            return angle;
        }

        return angle;
    }

    private float ConvertSteeringAngle(float angle) {

        return angle / 50.0f;
    }
}
