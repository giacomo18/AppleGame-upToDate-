using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;


public class CutsceneTrigger : MonoBehaviour
{
    //Variables
    public GameObject MainCamera;
    public GameObject CutsceneCamera;
    public float CutsceneTime = 10;
    public bool triggered;
    public GameObject Basket;
    public CollectingApples apples;
    public GameObject checkpoint;
    public GameObject input;
    public bool checkv2;
    public float timer = 30f;
    
    



    // Start is called before the first frame update
    void Start()
    {
        triggered = false;
        gameObject.GetComponent<BoxCollider>().isTrigger = false;
        apples = GameObject.Find("_FPCharacter").GetComponent<CollectingApples>();
        checkv2 = true;

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            SceneManager.LoadScene("SampleScene");
        }
       if (apples.full == true)
       {
            triggered = true;
            if (checkv2 == true)
            {
                gameObject.GetComponent<BoxCollider>().isTrigger = true;
                checkv2 = false;

            }
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
                input.transform.position = checkpoint.transform.position;
                gameObject.GetComponent<BoxCollider>().isTrigger = false;

                
                input.transform.position = checkpoint.transform.position;  
                
               
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
