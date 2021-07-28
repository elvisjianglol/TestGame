using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LookingScript : MonoBehaviour
{
    [Range(1.4f, 10f)]
    [SerializeField] private float xSensitivity = 3f;

    [Range(0.8f, 7f)]
    [SerializeField] private float ySensitivity = 2.5f;

    [SerializeField] private Transform player;
    [SerializeField] private Transform cam;

    private Camera fpsCAM;
    [SerializeField] private PlayerMovement playerMovement;

    [SerializeField] private float normalFOV = 60f;
    [SerializeField] private float dashFOV = 45f;
    [SerializeField] private float fovSpeed = 5f;

    private float currentFOV;

    private float playerRotation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        fpsCAM = GetComponent<Camera>();
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

        FOVControl();
    }

    void FOVControl()
    {

        fpsCAM.fieldOfView = Mathf.Lerp(fpsCAM.fieldOfView, currentFOV, Time.deltaTime * fovSpeed);

        if (playerMovement.isDashing)
        {
            currentFOV = dashFOV;
        }
        else currentFOV = normalFOV;
    }
}
