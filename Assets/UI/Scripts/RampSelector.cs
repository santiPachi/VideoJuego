using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  
using UnityEngine.UI;
public class RampSelector : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject RampButton;
    public GameObject RampContariner;
    public GameObject  UIScaleRot;
    public ArTapPlaceObject arTapPlaceObject;
    private void Start()
    {
        Sprite[] thumbnails =  Resources.LoadAll<Sprite>("RampsImage");
        foreach(Sprite tb in thumbnails){
            GameObject container =  Instantiate(RampButton) as GameObject;
            container.GetComponent<Image>().sprite = tb;
            container.transform.SetParent(RampContariner.transform,false);
            string rampName = tb.name;
            container.GetComponent<Button>().onClick.AddListener(()=> LoadRamp(rampName));
        }
    }
    private void LoadRamp(string rampName){
        this.gameObject.transform.parent.gameObject.SetActive(false);
         UIScaleRot.SetActive(true);
        string path = "RampsObject/"+rampName;
        GameObject obj =  Resources.Load<GameObject>(path);
        arTapPlaceObject.PlaceObject(obj);
        
       
    }

    // Update is called once per frame
   
}
