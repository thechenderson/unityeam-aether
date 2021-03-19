using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    //The controller itself
    public CharacterController controller;

    //The camera that looks at the controller  
    public Transform cam;

    /*Small empty object that sits beneath the character
    to determine if they're on the ground*/
    public Transform groundCheck;

    /*The distance the character is above the ground before being
    considered as on the ground*/
    public float groundDistance = 0.4f;

    /*Determines what objects are considered as ground level for the player*/
    public LayerMask groundMask;

    /*The movement speed of the character*/
    public float moveSpeed = 10f;

    /*The jump height of the character*/
    public float jumpHeight = 5f;

    /*How smoothly the character turns towards the direction the camera is facing*/
    float smoothTurnTime = 0.1f;

    /*The resulting speed that is required to turn the character towards the
    direction the camera is facing*/
    float turnSmoothVelocity;

    /*The specified level for gravity that the character will descend according to*/
    public float gravity = -9.81f;

    /*Boolean that changes depending whether the character is currently on the ground or not*/
    bool isGrounded;

    /* Point vector that determines what direction to move the agent to point towards the camera*/
    Vector3 velocity;

    // Update is called once per frame
    void Update()
    {
        /*Set isGrounded based off of where the invisible sphere underneath the player is*/ 
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        /* If the player is on the ground and their vertical velocity is 0,
        i.e. not jumping then have them stay on the ground. (slight downwards force
        to make up for slight hovering occasionaly)*/
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        /*Current location of the character vertically and horizontally in the world*/
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        /* 3 point vector that stores the players current location in the game world*/
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        /*The player has begun moving*/
        if(direction.magnitude >= 0.1f)
        {
            /*Calculate the angle that the player needs to face to be facing the same direction as the camera*/
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            /*Set the angle that the character will turn to using a combination of the current angle of the camera, the target angle and the turn time and velocity*/
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, smoothTurnTime);

            /*Rotate the player around the y-axis only to face the angle calculated above*/
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            /*A 3 point vector used to store the direction to move the character in based off of the angle of the camera and the forward movement*/
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            /*Move the player in the specified direction above at the moveSpeed set previously in line with real time*/
            controller.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
        }

        /*If the player is on the ground (not falling and not already jumping)*/
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            /*Set vertical velocity in line with gravity set and modifier to increase/decrease speed of jump*/
            velocity.y = Mathf.Sqrt(jumpHeight * -2.3f * gravity);
            Debug.Log("Jump Command Received");
        }
        /*Set the upward velocity to original calculated vertical velocity + the effect of gravity based on time*/
        velocity.y += gravity * Time.deltaTime;
        /*Move chracter vertically upwards (jump) */
        controller.Move(velocity*Time.deltaTime);
    
    }
}
