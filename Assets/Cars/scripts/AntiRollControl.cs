using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
public class AntiRollControl : MonoBehaviour
{
    public WheelCollider WheelL;
    public WheelCollider WheelR;
    private Rigidbody carRigidBody;
    public float AntiRoll = 5000.0f;

    // Start is called before the first frame update
    void Start()
    {
        carRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        WheelHit hit = new WheelHit();
        float travelL = 0.5f;
        float travelR = 0.5f;

        bool groundedL = WheelL.GetGroundHit(out hit);
        if(groundedL){
            travelL = (-WheelL.transform.InverseTransformPoint(hit.point).y
                      - WheelL.radius) / WheelL.suspensionDistance;
        }

        bool groundedR = WheelL.GetGroundHit(out hit);
        if(groundedL){
            travelR = (-WheelR.transform.InverseTransformPoint(hit.point).y
                      - WheelR.radius) / WheelR.suspensionDistance;
        }

        var antiRollForce = (travelL - travelR) * AntiRoll;

        if(groundedL){
            carRigidBody.AddForceAtPosition(WheelL.transform.up * - antiRollForce,
                WheelL.transform.position);
        }
        if(groundedR){
            carRigidBody.AddForceAtPosition(WheelR.transform.up * antiRollForce,
                WheelR.transform.position);
        }
    }
}
