using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    private int id;
	private void OnTriggerEnter(Collider other)
	{
        if(other.CompareTag("car")){    
            if(GameManager.instance.PickItem(id))
                gameObject.SetActive(false);
        }
	}
    public void SetId(int id){
        this.id = id;
    }
}
