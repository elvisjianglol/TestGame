using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {  
        
        GunScript GS = GetComponent<GunScript>();
        Target target = other.transform.GetComponent<Target>();


        if (target != null)
        {
            target.Hit(GS.damage);
        }

        Destroy(gameObject);


    }

}
