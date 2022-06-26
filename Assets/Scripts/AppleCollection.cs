using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleCollection : MonoBehaviour
{
    // Variables
    public bool collected;
    public GameObject Basket;
    public GameObject BasketItem;
    
    

    // Start is called before the first frame update
    void Start()
    {
        collected = false;

    }

    // Update is called once per frame
    void Update()
    {

        
      
    } 

   public void OnTriggerEnter(Collider other)
   {
        // check to see if player has collided with apple
        if (other.gameObject.tag == "Player")
        {
            collected = true;
            Debug.Log("Apple collected");
        }
        if (collected == true)
        {
         
       
        }
        if (gameObject == null)
        {
            Debug.LogWarning("object field has'nt been defined");
        }

   }
    

}
