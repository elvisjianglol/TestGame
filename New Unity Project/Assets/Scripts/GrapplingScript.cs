using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingScript : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask grappleableObj;
    public Transform gunTip, cam, player;
    public float maxDistance = 30f;
    
    private Rigidbody rb;

    //adjustable vars
    [SerializeField] private float force = 5f;


    [HideInInspector] public bool isGrappling = false;
    [SerializeField] private float forceRate = 0.1f;

    [SerializeField] private float grappleRotSpeed = 5f;
    private Quaternion rotation;

    
    
    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody>();
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (!isGrappling)
        {
            rotation = transform.parent.rotation;
        }
        else
        {
            rb.useGravity = false;
            rotation = Quaternion.LookRotation(grapplePoint - transform.position);
        }
         
        

        if (Input.GetButtonDown("Fire2"))
        {
            StartGrapple();
        }

        else if(Input.GetButtonUp("Fire2"))
        {

            StopGrapple();
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * grappleRotSpeed);
    }

    private void LateUpdate()
    {
        DrawRope();
    }

    void StartGrapple()
    {
        RaycastHit hit;
        if(Physics.Raycast(origin:cam.position, direction:cam.forward, out hit, maxDistance))
        {

            isGrappling = true;

            grapplePoint = hit.point;//point in space where raycast hit
          

            lr.positionCount = 2;
        }
    }

    

    private void FixedUpdate()
    {
        StartCoroutine(PullPlayer());
    }


    void StopGrapple()
    {
        isGrappling = false;
        rb.useGravity = true;

        lr.positionCount = 0;
    }

    void DrawRope()
    {
        //dont draw if not grappling
        if (!isGrappling) return;

        lr.SetPosition(index: 0, position: gunTip.position);
        lr.SetPosition(index: 1, grapplePoint);
    }


    IEnumerator PullPlayer()
    {
        while(isGrappling)
        {
            rb.AddForce((grapplePoint - player.position).normalized * force);


            yield return new WaitForSeconds(forceRate);
        }
           
        
      
    }






}
