using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLocalisate : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerController player;
    private CarLocalisate carLocalisate;

    private int dir;

    void Start()
    {
        carLocalisate = this;
    }

    // Update is called once per frame
    void Update()
    {
        // if(!Car.gameObject.GetComponent<Renderer>().isVisible)
        // {
            //  this.gameObject.SetActive(true);
            var cameraForward = Camera.main.transform.forward;

            var carDir = new Vector3(player.car.transform.position.x - Camera.main.transform.position.x,
                                    0f,player.car.transform.position.z - Camera.main.transform.position.z);
            // var cameraBearing = new Vector3(cameraForward.x,0,cameraForward.z).normalized;
             
 
            // // CopyTransform(pivot.transform,Camera.main.transform);
            // pivot.transform.position = Camera.main.transform.position;
            // pivot.transform.rotation = Quaternion.LookRotation(cameraBearing);
            var dot = cameraForward.x*carDir.x + cameraForward.z*carDir.z;
            var det = cameraForward.x*carDir.z - cameraForward.z*carDir.x;
            var angle = Mathf.Atan2(det,dot)*180/Mathf.PI;

            Dir(angle);

        // }else{
        //     //  this.gameObject.SetActive(false);
        // }
    }
    private void Dir(float angle){
        carLocalisate.transform.eulerAngles  = new Vector3(0,0,angle);
    }
   
}
