using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    //What character will be controlled by this script
    public CharacterController controller;

    //What camera will be used to aim the character
    public Transform cam;

    //What object is used to determine if the character is touching the ground
    public Transform groundCheck;

    //The distance the character is above the ground before being considered as not grounded
    public float groundDistance = 0.3f;

    //What layers are considered as ground for the player to be standing on
    public LayerMask groundMask;

    //What speed does the player move at
    public float moveSpeed = 5f;

    //How high can the player jump
    public float jumpHeight = 2f;

    //How long will a turn be extended to smoothen the movement
    public float turnSmoothTime = 0.1f;

    //The speed required for the character to turn towards the diretion of movement
    float turnSmoothVelocity;

    //Is the player on the ground
    bool isGrounded;

    //What is the velocity of the player in all 3 axes
    Vector3 playerVelocity;

    // Update is called once per frame
    void Update()
    {

        //Create a sphere under character to check whether they are touching ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //Find the current gravity value from the gravity manager
        Vector3 currentGravity  = GameObject.FindObjectOfType<GravityManager>().getGravity();


        //Get current horizontal and vertical input (WASD or Arrows)
        float horizontal = Input.GetAxisRaw("Horizontal"); 
        float vertical = Input.GetAxisRaw("Vertical");
        

        //Assign vertical and horizontal keys to a 3 point vector
        Vector3 direction = new Vector3(horizontal, 0f, vertical);
        

        //Move player in direction camera is facing
            if(direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
            }
        
        
        if (isGrounded == true && playerVelocity.y < 0)
        {            
            //Force player towards ground
            playerVelocity.y = -2f;
   
        }

            //Make player move as gravity is currently set
            playerVelocity.y += currentGravity.y * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);     



        //Check whether jump has been pressed and the player is currently on the ground
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                moveSpeed = 3f;
                //Add upwards velocity that works with current gravity
                playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * currentGravity.y);
                Debug.Log("playerVelocity.y = " + playerVelocity.y);
                controller.Move(playerVelocity * Time.deltaTime);    
            }
        }
    }
