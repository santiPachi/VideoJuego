using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{   
  
    void Update()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * 120 , Space.World);
    }
    private void OnTriggerEnter(Collider other) {
        GameManager.instance.PickCoin();

        Destroy(gameObject);
    }
}
