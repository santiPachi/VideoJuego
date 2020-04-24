using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramp : MonoBehaviour
{
    public bool isColliding = false;
    public List<GameObject> listRamps;
    private  Material[] matNormal;
    public  Material matWarning;
    private bool isPlaced = false;
    void Start() {
        
        DiableMeshColl();
        matNormal = listRamps[0].GetComponent<Renderer>().materials;
    }
    void OnTriggerEnter(Collider other)
    {
        if(!isPlaced)
            if(other.CompareTag("car")){
                isColliding = true;
                MaterialWarning();
            }
    }
    void OnTriggerExit(Collider other)
    {
        if(!isPlaced)
            if(other.CompareTag("car")){
                isColliding = false;
                MaterialNormal();
            } 
    }
    public void PlaceRamp(){
        isPlaced = true;
        EnableMeshColl();

    }
    public void MaterialNormal(){
        foreach(GameObject ramp in listRamps){
            ramp.GetComponent<Renderer>().materials  =  matNormal;

        }
    }
    
    private void MaterialWarning(){
        foreach(GameObject ramp in listRamps){
            ramp.GetComponent<Renderer>().material  =  matWarning;
        }
    }
    private void EnableMeshColl(){
        foreach(GameObject ramp in listRamps){
            MeshCollider mesh = ramp.GetComponent<MeshCollider>();
            mesh.enabled = true;
        }
    }
    private void DiableMeshColl(){
        foreach(GameObject ramp in listRamps){
            MeshCollider mesh = ramp.GetComponent<MeshCollider>();
            mesh.enabled = false;
        }
    }
}
