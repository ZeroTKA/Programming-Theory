using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -20f;
    public float jumpHeight = 2f;

    public Transform groundCheck;
    public Transform ceilingCheck;
    public float groundDistance = .4f;
    public float ceilingDistance = .2f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    bool isHittingCeiling;

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        //moves characetr
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward *z;
        controller.Move(move * speed * Time.deltaTime);   
        
        //jumps
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //checks if the player hits the ceiling
        if(!isGrounded && velocity.y > 0)
        {
            isHittingCeiling = Physics.CheckSphere(ceilingCheck.position, ceilingDistance, groundMask);
            if (isHittingCeiling) { velocity.y = 0; }
        }

        //gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
