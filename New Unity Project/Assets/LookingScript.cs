using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LookingScript : MonoBehaviour
{

    [SerializeField] private float xSensitivity = 3f;
    [SerializeField] private float ySensitivity = 2.5f;

    [SerializeField] private Transform player;
    [SerializeField] private Transform cam;

    private float playerRotation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Mouse X") * xSensitivity;
        float y = Input.GetAxis("Mouse Y") * ySensitivity;

         playerRotation -= y;
         playerRotation = Mathf.Clamp(playerRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(playerRotation, 0f, 0f);

        player.Rotate(Vector3.up, x);

    }
}
