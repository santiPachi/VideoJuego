using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using System;


public class ArTapPlaceObject : MonoBehaviour 
{

    private GameObject ObjectToPlace;
    public GameObject UIGame;
    public GameManager enviroment;
    public GameObject UIScaleRot;
    public Slider SliderScale;
    public Slider SliderScaleAll;
    public Slider SliderRot;
    //  public LongPressButton btReload;
    private ARSessionOrigin arOrigin;
    private Pose placementPose;
    private Vector3 scale;
    private Vector3 rotation;
    public ArTapPlaceCar arTapPlaceCar;

     private bool Place =  false;
     private bool toScaleScene = false;
    // private bool isInstantiate = false;
    // private bool isEnviromentPlaced = false;
     private bool placementPoseIsValid = false;
     public  ARPlaneManager aRPlaneManager;
     private List<GameObject> Ramps = new List<GameObject>();
     private Ramp ramp;
    // Start is called before the first frame update
    void Start()
    {   
        
        arOrigin = FindObjectOfType<ARSessionOrigin>(); 
        
        // car.SetActive(false);
        
    }
   
    // Update is called once per frame
    void Update()
    {
        if(Place){
            UpdatePlacementPose();
            UpdatePlacementIndicator();
            PlaceObject();
        }
        ScaleAllRamps();
        // ReloadScene();
     }
    // public void ReloadScene(){

    //         Player.Reload();
    //         placementIndicator.SetActive(true);
    //         isPlaced = false;
        
    // }
  
    public void PlaceObject(GameObject obj){

        ObjectToPlace = Instantiate(obj);
        scale = ObjectToPlace.transform.localScale;
        rotation = ObjectToPlace.transform.localEulerAngles;
        UIGame.SetActive(false);
        ramp = ObjectToPlace.GetComponent<Ramp>();
        SliderRot.gameObject.SetActive(true);
        SliderScale.gameObject.SetActive(true);
        ObjectToPlace.SetActive(false);
        Place = true;
    }
    public void Cancel(){
        Place = false;
        
        ObjectToPlace.SetActive(false);

    }
    public void StartScaleScene(){
        toScaleScene = true;

    } 
    public void StopScaleScene(){
        toScaleScene = false;

    }
    private void ScaleAllRamps(){
        if(toScaleScene)
            foreach(GameObject rampObj in Ramps){
                rampObj.transform.localScale = new Vector3(1.0f+SliderScaleAll.value,1.0f+SliderScaleAll.value,1.0f+SliderScaleAll.value);
            }
    }
    private void PlaceObject(){
       

        if( placementPoseIsValid && Input.touchCount > 1 && Input.GetTouch(0).phase == TouchPhase.Began){
                if(!ramp.isColliding){  
                    Vector3 pos = ObjectToPlace.transform.position;
                    Quaternion rot = ObjectToPlace.transform.rotation;
                    GameObject obj = Instantiate(ObjectToPlace,pos,rot);
                    Ramp rampObj = obj.GetComponent<Ramp>();
                    rampObj.PlaceRamp();
                    rampObj.enabled = false;
                    Ramps.Add(obj);
                    // Place = false;
                    // UIGame.SetActive(true);
                    // UIScaleRot.SetActive(false);
                }
        }
    }
    
    private void UpdatePlacementPose(){

        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f,0.5f));
        var hits = new List<ARRaycastHit>();
        
        arOrigin.Raycast(screenCenter,hits,TrackableType.PlaneEstimated);
        placementPoseIsValid = hits.Count > 0;
        
        if( placementPoseIsValid){
            placementPose = hits[0].pose;
            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x,0,cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }

    }

     

     private void  UpdatePlacementIndicator(){
        if( placementPoseIsValid){

            float y =  arTapPlaceCar.enviroment.transform.position.y;
         
           float diff = Math.Abs(y-placementPose.position.y);
            if(diff < 0.1){
                placementPose.position.y = y;
                    
            }
            ObjectToPlace.SetActive(true);
            ObjectToPlace.transform.localScale = new Vector3(scale.x + SliderScale.value,scale.y + SliderScale.value,scale.z + SliderScale.value);
            ObjectToPlace.transform.localEulerAngles = new Vector3(rotation.x,rotation.y + SliderRot.value,rotation.z);
            ObjectToPlace.transform.position =  placementPose.position;          
        }else{
            ObjectToPlace.SetActive(false);
        }
    }

    
}

   
