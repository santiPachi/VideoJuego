using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
  
	private void OnTriggerEnter(Collider other)
	{
        if(other.CompareTag("car")){    
            GameManager.instance.PickItem();
            Destroy(gameObject);
        }
	}
}
