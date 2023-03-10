using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonController : MonoBehaviour
{
    private CharacterController controller;
    public Transform cam;
    public Transform LookAtTransform;

    private Vector3 playerVelocity;
    public Transform groundSensor;
    public LayerMask ground;
    public float sensorRadius = 0.1f;
    public float gravity = -9.81f;
    public bool isGrounded;
    public float speed = 5;
    public float jumpForce = 5;
    public float jumpHeight = 1;

    private float TurnSmoothVelocity;
    public float TurnSmoothTime = 0.1f;

    public Cinemachine.AxisState xAxis;
    public Cinemachine.AxisState yAxis;

    public GameObject[] cameras;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame

    void Update()
    {
        // Movement();
        //MovementTPS();
        MovementTPS2();
        Jump();

    }
    

void Movement()

{
    Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        if (move != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref TurnSmoothVelocity, TurnSmoothTime);

            transform.rotation = Quaternion.Euler(0, targetAngle, 0);

            Vector3 moveDirection  = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }
    
}

//Movimiento TPS con FreeLook Camera
void MovementTPS()

{
    Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        if (move != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, cam.eulerAngles.y, ref TurnSmoothVelocity, TurnSmoothTime);

            transform.rotation = Quaternion.Euler(0, targetAngle, 0);

            Vector3 moveDirection  = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }
    
}

//Movimiento TPS con VirtualCamera
void MovementTPS2()

{
    Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
    
    xAxis.Update(Time.deltaTime);
    yAxis.Update(Time.deltaTime);

    transform.rotation = Quaternion.Euler(0, xAxis.Value, 0);
    LookAtTransform.eulerAngles = new Vector3 (yAxis.Value, xAxis.Value, LookAtTransform.eulerAngles.z);
    //LookAtTransform.rotation = Quaternion.Euler(yAxis.Value, xAxis.Value, LookAtTransform.eulerAngles.z);

    if(Input.GetButton("Fire2"))
    {
        cameras[0].SetActive(false);
        cameras[1].SetActive(true);
    }
    else 
    {
        cameras[0].SetActive(true);
        cameras[1].SetActive(false);
    }


        if (move != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, cam.eulerAngles.y, ref TurnSmoothVelocity, TurnSmoothTime);

            

            Vector3 moveDirection  = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }
    
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