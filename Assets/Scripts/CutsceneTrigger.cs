using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class CutsceneTrigger : MonoBehaviour
{
    //Variables
    public GameObject MainCamera;
    public GameObject CutsceneCamera;
    public float CutsceneTime = 10;
    public bool triggered;
    public GameObject Basket;
    public bool Collider;
    
    



    // Start is called before the first frame update
    void Start()
    {
        triggered = false;
        Collider = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Collider == true)
        {
            gameObject.GetComponent<BoxCollider>().isTrigger = true;

        }
       
        else 
            triggered = false;
        gameObject.GetComponent<BoxCollider>().isTrigger = false;
       
        
        
        if (gameObject.tag == ("Player") && gameObject.GetComponent<CollectingApples>().full)
        {
            triggered = true;
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
        }




    }
    public void OnTriggerEnter(Collider other)
    {       
        //trigger updating the time allowed within the cutscene

        if (triggered == true)
        {


            CutsceneTime -= Time.deltaTime;
            if (CutsceneTime < 0)
            {
                Destroy(Basket);
                CutsceneCamera.SetActive(false);
                MainCamera.SetActive(true);
                CutsceneTime = 0;

            }

        }
        else Debug.Log("Need More Apples");
    }


}
