using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    CharacterController controller;
    public Transform fpsCamera;

    public float sensitivity = 200f;
    public float speed = 15f;

    float xRotation = 0f;

    bool isGrounded;
    public Transform groundSensor;
    public float sensorRadius;
    public LayerMask ground;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    Vector3 playerVelocity;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float MouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        //Con += se hacía invertida la camara de arriba a abajo, con -= esta bien
        xRotation -= MouseY;
        xRotation = Mathf.Clamp(xRotation,-90, 90);

        fpsCamera.localRotation = Quaternion.Euler(xRotation, 0, 0);

        transform.Rotate(Vector3.up * MouseX);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move.normalized * speed * Time.deltaTime);

        Jump();
    }

    void Jump()
    {
        isGrounded = Physics.CheckSphere(groundSensor.position, sensorRadius, ground);

        if (isGrounded && playerVelocity.y <0)

        playerVelocity.y = 0;


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //playerVelocity.y += jumpForce;
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

    }
}
