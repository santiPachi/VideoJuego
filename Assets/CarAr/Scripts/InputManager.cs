using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    static public float throttle;
    static public float throttleBack;
    static public float brake;
    static public bool turbo = false;
    // public Joystick joystick;
    static public float steer;
     public LongPressButton btAccel;
     public LongPressButton btBack;
     public LongPressButton btBrake;
     public LongPressButton btTurbo;
     public Joystick joystick;
    // Update is called once per frame
    void Update()
    {
        throttle = btAccel.value;
        throttleBack = -btBack.value;
        steer = joystick.Horizontal;
        brake = btBrake.value;

        if(btTurbo.value > 0){
            turbo = true;
        }else{
            turbo = false;
        }
        
    }
}
