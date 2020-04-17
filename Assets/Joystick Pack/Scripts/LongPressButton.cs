using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class LongPressButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Start is called before the first frame update
    public float value = 0f;
    public UnityEvent onLongClick;
    private bool down = false;
    private bool up = false;
    public void OnPointerDown(PointerEventData eventData){
        down = true;
        
        // value = 0.9f;
    }
    public void OnPointerUp(PointerEventData eventData){
        down = false;
        // up = true;
         
        // value = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        if(down){
            if ( value < 1){
                value = value + (float)0.01;
            }
        }else{
            if ( value > 0){
            value = value - (float)0.025;
            }
        }
    }
}
