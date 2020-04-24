using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSelectCar : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> Cars;
    private int numberCars;
    private bool isNext = false;
    private bool isBack = false;
    private bool Rotate = false;
    private float deg = 0;
    private float countDeg = 2f;
    private float countAux = 0f;
    private int CarNum = 0;
    private float DegOutRange;
    void Start()
    {
        numberCars = Cars.Count;
        hideCars(0);
    }

    // Update is called once per frame
    void Update()
    {   
        
        RotateCar();
    }
    private void hideCars(int i){
        int count = 0;
        foreach(GameObject car in Cars){
            RotateCarOwnAxis rot = car.GetComponent<RotateCarOwnAxis>();
            if(count != i){
                rot.StopRotateCar();
                car.SetActive(false);
            }else{
                car.SetActive(true);
                rot.RotateCar();
               
            }
            count ++;
           
        }
    }
    private void RotateCar(){
        if(Rotate){
            if(CarNum >= 0 && CarNum < numberCars){
                if(deg==0)
                    hideCars(CarNum);
                if(deg < 30f){
                    this.transform.Rotate(0f,countDeg,0f);
                    deg += countAux;
                }else{
                    Rotate = false;
                    deg = 0f;
                }
            }else{
                if(CarNum<0){
                    CarNum = numberCars-1;
                    DegOutRange = -360 + 30*(numberCars-1);
                }else if(CarNum >= numberCars){
                    CarNum = 0;
                    DegOutRange = 360 - 30*(numberCars-1);
                }
                hideCars(CarNum);
                this.transform.Rotate(0f,DegOutRange,0f);
                Rotate = false;
            }
            
        }
    }
    public void Next(){
        if(!Rotate){
            Rotate = true;
            countDeg = 2f;
            countAux = 2f;
            CarNum +=1;
        }
    
    }
    public void Back(){
        if(!Rotate){
            Rotate = true;
            countDeg = -2f;
            countAux = 2f;
            CarNum -=1;
        }
      
    }

    public void SelectCar(){
        PlayerConfig.CarName = Cars[CarNum].name;
    }
}
