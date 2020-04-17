
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using System;


public class ArTapPlaceObjectRace : MonoBehaviour 
{
     public GameObject enviroment;
     public PlayerController Player;
     private GameObject placementIndicator;
     public GameObject UIGame;
     public GameObject UiScan;
     public Slider slideScene;
     private bool toScaleScene;
     private Vector3 localScale;
    //  public LongPressButton btReload;
    private ARSessionOrigin arOrigin;
    private Pose placementPose;
    private bool isPlaced = false;
    private bool isInstantiate = false;
    private bool isEnviromentPlaced = false;
    private bool placementPoseIsValid = false;
    // Start is called before the first frame update
    void Start()
    {   
        
        arOrigin = FindObjectOfType<ARSessionOrigin>(); 
        enviroment.SetActive(false);
        localScale = enviroment.transform.localScale;
        Player.LoadPrefav();
        placementIndicator = Instantiate(Player.carPref);
        UIGame.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPlaced){
            UpdatePlacementPose();
            UpdatePlacementIndicator();
            PlaceObject();
        }
         if(toScaleScene){
            ScaleScene();
        }
        // ReloadScene();
    }
    public void ReloadScene(){

            Player.Reload();
            placementIndicator.SetActive(true);
            isPlaced = false;
        
    }
    public void StartScaleScene(){
        toScaleScene = true;

    } 
    public void StopScaleScene(){
        toScaleScene = false;

    }
    private void ScaleScene(){
        if(enviroment!=null){
            enviroment.transform.localScale = new Vector3(localScale.x + slideScene.value,localScale.y + slideScene.value,localScale.z + slideScene.value);
        }
    }
    private void PlaceObject(){
        if( placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began){
                 
                if(!isEnviromentPlaced){
                    enviroment.SetActive(true);
                    
                    enviroment.transform.position =  placementIndicator.transform.position;
                    enviroment.transform.rotation =  placementIndicator.transform.rotation;
                }
                Vector3 pos = placementIndicator.transform.position;
                Quaternion rot = placementIndicator.transform.rotation;
                Player.Place(pos,rot);
                if(!isEnviromentPlaced){
                    Player.car.transform.parent = enviroment.transform;
                    placementIndicator.transform.parent = enviroment.transform;
                }
                    
                placementIndicator.SetActive(false);
                isPlaced = true;
                isEnviromentPlaced = true;
                UIGame.SetActive(true);
                // UiScan.SetActive(false);
        }
    }
    
    private void UpdatePlacementPose(){

        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f,0.5f));
        var hits = new List<ARRaycastHit>();
        
        arOrigin.Raycast(screenCenter,hits,TrackableType.Planes);
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
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }else{
            placementIndicator.SetActive(false);
        }
    }

    
}

   
