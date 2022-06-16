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
    public CollectingApples apples;
    
    



    // Start is called before the first frame update
    void Start()
    {
        triggered = false;
        gameObject.GetComponent<BoxCollider>().isTrigger = false;
        apples = GameObject.Find("_FPCharacter").GetComponent<CollectingApples>();

    }

    // Update is called once per frame
    void Update()
    {
       if (apples.full == true)
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
            if (CutsceneTime > 0)
            {
                Destroy(Basket);
                CutsceneCamera.SetActive(true);
                MainCamera.SetActive(false);
                StartCoroutine(Cutscene());
                //CutsceneTime = 0;

            }

        }
        else Debug.Log("Need More Apples");
    }

    private IEnumerator Cutscene()
    {
        yield return new WaitForSeconds(CutsceneTime);
        CutsceneCamera.SetActive(false);
        MainCamera.SetActive(true);
        
       
    }
     

}
