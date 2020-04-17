using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speepometer : MonoBehaviour
{
    // Start is called before the first frame update
    static float minAng = 90;
    static float maxAng = -90;
    static Speepometer speepometer;
    void Start()
    {

        speepometer = this;
    }
    public static  void ShowSpeed(float speed, float maxSpeed){
        float minSpeed = 0f;

        float ang = Mathf.Lerp(minAng,maxAng,Mathf.InverseLerp(0f,maxSpeed,speed));
        speepometer.transform.eulerAngles = new Vector3(0,0,ang);
    }
    // Update is called once per frame
   
}
