using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public GameObject teleportIn;
    public GameObject teleportOut;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCollisionEnter(Vector3 teleporting)
    {
       if (player = teleportIn)
        {
           // gameObject.transform.position = teleportOut;
        }
        
    }

}
