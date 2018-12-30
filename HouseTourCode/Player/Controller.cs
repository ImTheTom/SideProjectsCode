using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
    [SerializeField] private float walk = 5.0f;
    [SerializeField] private float walkAir = 3.0f;
    [SerializeField] private float run = 10.0f;
    private bool isRunning;
    private bool grounded = true;
    private CharacterController controller;
    [SerializeField] private float jumpSpeed = 20.0f;
    private Vector3 moveDirection = Vector3.zero;
    [SerializeField] private float gravity = 60.0f;
    private float translation = 0f;
    private float straffe = 0f;
    private float vSpeed = 0;
    // Use this for initialization
    void Start() {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.LeftShift)) {
            isRunning = true;
        } else {
            isRunning = false;
        }
        if (controller.isGrounded) {
            if (isRunning) {
                translation = Input.GetAxis("Vertical") * run;
                straffe = Input.GetAxis("Horizontal") * run;
                moveDirection = new Vector3(straffe, 0, translation);
                moveDirection = transform.TransformDirection(moveDirection);
                vSpeed = -100;
                if (Input.GetKeyDown((KeyCode.Space))) {
                    vSpeed = jumpSpeed;
                }
            } else {
                vSpeed = -100;
                translation = Input.GetAxis("Vertical") * walk;
                straffe = Input.GetAxis("Horizontal") * walk;
                moveDirection = new Vector3(straffe, 0, translation);
                moveDirection = transform.TransformDirection(moveDirection);
                if (Input.GetKeyDown((KeyCode.Space))) {
                    vSpeed = jumpSpeed;
                }
            }
        } else {
            if (isRunning) {
                translation = Input.GetAxis("Vertical") * run;
                straffe = Input.GetAxis("Horizontal") * run;
                moveDirection = new Vector3(straffe, 0, translation);
                moveDirection = transform.TransformDirection(moveDirection);
            } else {
                translation = Input.GetAxis("Vertical") * walkAir;
                straffe = Input.GetAxis("Horizontal") * walkAir;
                moveDirection = new Vector3(straffe, 0, translation);
                moveDirection = transform.TransformDirection(moveDirection);
            }
        }
        vSpeed -= gravity * Time.deltaTime;
        moveDirection.y = vSpeed;
        controller.Move(moveDirection * Time.deltaTime);
    }
}