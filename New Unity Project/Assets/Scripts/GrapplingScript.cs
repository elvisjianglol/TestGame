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
    
    public Rigidbody rb;

    //adjustable vars
    public float force = 5f;
    public float massScaleValue = 5f;
    public float damperValue = 6f;
    public float springValue = 20f;
    
    
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
            grapplePoint = hit.point;//point in space where raycast hit
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.2f;

            //adjustable
            joint.spring = springValue;
            joint.damper = damperValue;
            joint.massScale = massScaleValue;

            lr.positionCount = 2;

            //keep pulling player to grapplePoint
            StartCoroutine(PullPlayer());
           


        }
    }

    void StopGrapple()
    {
        
       
        lr.positionCount = 0;
        Destroy(joint);
    }

    void DrawRope()
    {
        //dont draw if not grappling
        if (!joint) return;

        lr.SetPosition(index: 0, position: gunTip.position);
        lr.SetPosition(index: 1, grapplePoint);
    }

    public bool isGrappling()
    {
        return joint != null;
    }

    IEnumerator PullPlayer()
    {
        rb.AddForce((grapplePoint - player.position).normalized * force);
        yield return new WaitForSeconds(5f);
    }






    
}
