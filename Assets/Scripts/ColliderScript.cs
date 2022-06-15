using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderScript : MonoBehaviour
{
    public bool Collider;
    public GameObject CutsceneTrigger;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Collider == true)
        {
            GetComponent <Collider>().enabled = false;  
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        //checking if player has correct amount of apples to pass through trigger
        if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<CollectingApples>().full)  
        {
            Collider = false;    
            CutsceneTrigger.GetComponent<BoxCollider>().isTrigger = true;
       
        } 
       
        CutsceneTrigger.GetComponent<Collider>().isTrigger = false;




    }
}
