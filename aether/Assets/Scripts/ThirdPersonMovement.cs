using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{

    public Transform cam;
    public CharacterController controller;
    public float moveSpeed = 6f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    Vector3 playerVelocity;
    public Transform groundCheck;
    public float groundDistance = 1f;
    public LayerMask groundMask;
    bool isGrounded;
    public float jumpHeight = 2f;


    // Update is called once per frame
    void Update()
    {
        //Find the current gravity value from the gravity manager
        float currentGravity = FindObjectOfType<GravityManager>().gravity;

        //Project sphere at bottom of character to determine whether touching the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //Log current velocity, whether player is grounded and current gravity value obtained from gravity manager
        Debug.Log("Player Velocity: "+playerVelocity + "Grounded: " + isGrounded + "Current Gravity: " + currentGravity);

        //If player is on the ground then force player to remain there. (simulate falling until hitting ground)
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        //Get current horizontal and vertical location in space
        float horizontal = Input.GetAxisRaw("Horizontal"); 
        float vertical = Input.GetAxisRaw("Vertical");
        
        //Assign vertical and horizontal location to a 3 point vector
        Vector3 direction = new Vector3(horizontal, 0f, vertical);

        //Check whether jump has been pressed and the player is currently on the ground
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //Add upwards velocity that works with current gravity
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * currentGravity);
        }

        //Make player move as gravity is currently set
        playerVelocity.y += currentGravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        //Move player in direction camera is facing
        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
        }
    }
}
