using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCarOwnAxis : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    private Quaternion quaternionOriginal;
    private bool startRotatation = false;
    void Start()
    {
        quaternionOriginal = this.transform.rotation;
    }
    void Update()
    {
        if(startRotatation)
            this.transform.Rotate(0f,0.5f,0f);
    }

    public void RotateCar(){
        this.transform.rotation = quaternionOriginal;
        startRotatation = true;
    }
    public void StopRotateCar(){
        startRotatation = false;
    }
}
