using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    [SerializeField] private Transform cam;

    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shootPoint;

    [SerializeField] private Rigidbody rb;

    [SerializeField] private float range = 100f;
    [SerializeField] private float fireRate = 10f;

    private float shootDelay;


    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {

        if(Input.GetButton("Fire1") && Time.time >= shootDelay)
        {
            shootDelay = Time.time + 1 / fireRate;

            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        Vector3 hitPoint;

        GameObject B = Instantiate(bullet, shootPoint.transform.position, cam.transform.rotation);
        rb = B.GetComponent<Rigidbody>();




        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            hitPoint = hit.point;

            rb.AddForce((hitPoint - shootPoint.transform.position).normalized * bulletSpeed, ForceMode.VelocityChange);
        }
        else rb.AddForce(cam.transform.forward * bulletSpeed, ForceMode.VelocityChange);







    }


}
