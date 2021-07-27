using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingScript : MonoBehaviour
{
    private LineRenderer lr;
    private SpringJoint joint;
    private Vector3 grapplePoint;
    public LayerMask grappleableObj;
    public Transform gunTip, cam, player;
    public float maxDistance = 30f;
    
    private Rigidbody rb;

    //adjustable vars
    public float force = 5f;
    public float massScaleValue = 5f;
    public float damperValue = 6f;
    public float springValue = 20f;

    [HideInInspector] public bool grappling = false;
    [SerializeField] private float forceRate = 0.1f;
    
    
    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody>();
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        
        if (Input.GetButtonDown("Fire2"))
        {
            StartGrapple();
        }

        else if(Input.GetButtonUp("Fire2"))
        {

            StopGrapple();
        }

 
    }

    private void LateUpdate()
    {
        DrawRope();
    }

    void StartGrapple()
    {
        RaycastHit hit;
        if(Physics.Raycast(origin:cam.position, direction:cam.forward,out hit, maxDistance))
        {
            grappling = true;
            rb.useGravity = false;

            grapplePoint = hit.point;//point in space where raycast hit
          

            lr.positionCount = 2;

            //keep pulling player to grapplePoint
            
             StartCoroutine(PullPlayer());
            

        }
    }

    void StopGrapple()
    {
        grappling = false;
        rb.useGravity = true;

        lr.positionCount = 0;
        Destroy(joint);
    }

    void DrawRope()
    {
        //dont draw if not grappling
        if (!grappling) return;

        lr.SetPosition(index: 0, position: gunTip.position);
        lr.SetPosition(index: 1, grapplePoint);
    }

    public bool isGrappling()
    {
        return joint != null;
    }

    IEnumerator PullPlayer()
    {
        while(grappling)
        {
            rb.AddForce((grapplePoint - player.position).normalized * force * 10);


            yield return new WaitForSeconds(forceRate);
        }
           
        
      
    }






    
}
