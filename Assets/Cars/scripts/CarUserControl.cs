using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
      
       
        private CarController m_Car; // the car controller we want to use


        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
        }


        private void FixedUpdate()
        {
            // pass the input to the car!
            float s =  InputManager.steer;
            float t = InputManager.throttle;
            float tb = InputManager.throttleBack;
            float hb = InputManager.brake;
            m_Car.isTurbo =  InputManager.turbo;
            m_Car.Move(s, t, tb, hb);

        }

    }
}