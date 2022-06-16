using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollectingApples : MonoBehaviour
{
    public bool full;
    public bool NeedMore;
    public int counter = 0;
    public List<GameObject> apples = new List<GameObject>();
    
    
    
    private void OnTriggerEnter(Collider other)
       
    {
        if (other.gameObject.tag == "Apples")
        {
            
            Destroy(other.gameObject);  
            apples[counter].SetActive(true);
            counter += 1;
            Debug.Log(counter);

        }            
    }
    public void Update()
    {
       
        if (counter == 4)
        {
            full = true;
            NeedMore = false;
        }
        if (counter <= 3)
        {
            NeedMore = true;                         
        }
    }
    
}
