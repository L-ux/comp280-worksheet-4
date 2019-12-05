using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControls : MonoBehaviour
{
    public Transform body;
    public float mouseSens = 100f;
    public float updown = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        updown -= mouseY;
        updown = Mathf.Clamp(updown, -90f, 90f);

        transform.localRotation = Quaternion.Euler(updown,0f,0f);
        body.Rotate(Vector3.up * mouseX);
    }
}
