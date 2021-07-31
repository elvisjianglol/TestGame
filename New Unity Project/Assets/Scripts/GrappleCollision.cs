using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleCollision : MonoBehaviour
{
    Rigidbody rb;



    private void Start()
    {
        if(GetComponent<Rigidbody>() != null)
        {
            rb = GetComponent<Rigidbody>();
            rb.isKinematic = false; ;// enable physics
        }
    }
    
    // runs automatically
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.gameObject.layer); //for testing purposes
        if(collision.collider.gameObject.layer == 3 && rb != null)
        {
            rb.isKinematic = true; // disable physics for this object
            transform.parent = collision.collider.transform; // set this transform to the object that of the collider
        }
    }
}
