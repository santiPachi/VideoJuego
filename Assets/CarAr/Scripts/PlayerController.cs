using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject carPref;
    public GameObject car;
    private bool isInstantiate = false;
    void Start(){
   
        
     
    }
    public void LoadPrefav(){
        string path = "Cars/"+PlayerConfig.CarName;
        carPref =  Resources.Load<GameObject>(path);
    }
    public void Place(Vector3 pos,Quaternion rot){
         if(!isInstantiate){
            pos.y += 0.09f;
            car =  Instantiate(carPref,pos,rot);
            isInstantiate = true;
        }else{
            car.transform.position =  pos;
            car.transform.rotation =  rot;
            car.SetActive(true);
         }
    }
    public void Reload(){
        car.SetActive(false);
    }
}
