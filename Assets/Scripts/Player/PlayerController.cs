using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    public CharacterController controller;
    private Vector3 direction;
    public float speed = 8;
    public float jumpForce = 10;
    public float gravity = -30;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public bool ableToDoubleJump = true;
    public Animator animator;
    public Transform model;


    // Update is called once per frame
    void Update()
    {
        float hInput = Input.GetAxis("Horizontal");
        direction.x = hInput * speed;
        animator.SetFloat("speed", Mathf.Abs(hInput));
        bool isGrounded = Physics.CheckSphere(groundCheck.position, 0.15f, groundLayer);
        animator.SetBool("isGrounded", isGrounded);

        if (isGrounded)
        {
            direction.y = -1;
            ableToDoubleJump = true;

            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                animator.SetTrigger("fireBallAttack");
            }
        }
        else
        {
            direction.y += gravity * Time.deltaTime;
            if (ableToDoubleJump & Input.GetButtonDown("Jump"))
            {
                DoubleJump();
            }
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fireball Attack"))
            return;
        //flip player
        if (hInput != 0)
        {
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(hInput, 0, 0));
            model.rotation = newRotation;
        }
        //move player
        controller.Move(direction * Time.deltaTime);
    }

    private void DoubleJump()
    {
        animator.SetTrigger("doubleJump");
        direction.y = jumpForce;
        ableToDoubleJump = false;
    }

    private void Jump()
    {
        direction.y = jumpForce;
    }
}
