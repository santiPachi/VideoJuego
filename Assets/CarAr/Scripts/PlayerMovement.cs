using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 6f;
    private Rigidbody rb;
    private BoxCollider box;
    private Vector3 movement;
    private float vertical;
    private float horizontal;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
      
	}
   
	private void FixedUpdate()
	{
        vertical = UnityEngine.Input.GetAxisRaw("Vertical");
        horizontal = UnityEngine.Input.GetAxisRaw("Horizontal");

        Move(vertical,horizontal);
        Turn(vertical,horizontal);

	}

    void Move(float v, float h)
    {
        movement.Set(h,0,v);
        movement = movement.normalized * speed * Time.deltaTime; 
        rb.MovePosition(transform.position + movement);

         
    }
    void Turn(float v, float H){
        Vector3 direccion = new Vector3(horizontal, 0f, v);

        //Si el personaje tuvo un camnio  de rotacio 
        if(direccion  != Vector3.zero){
            //Direccion a donde dene girar el personaje con Quaternion
            Quaternion newRotation = Quaternion.LookRotation(direccion);
            rb.rotation = newRotation;

        }
    }

    


}
