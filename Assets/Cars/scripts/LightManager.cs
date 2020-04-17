using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Light> Backlights;
    public List<GameObject> TailLigths;
    public  void ToggleTailLights(bool brake){
        foreach( GameObject tail in TailLigths){
             tail.GetComponent<Renderer>().material.SetColor("_EmissionColor",brake? new Color(0.5f,0.111f,0.111f) : Color.black);
        }
       
    }
    public void ToggleBackLights(float throttle){
        foreach(Light light in Backlights){
            if(throttle < 0  ){
                light.intensity = 13;
            }else{
                light.intensity = 0;
            }
        }
    }
    
    // Update is called once per frame
}
