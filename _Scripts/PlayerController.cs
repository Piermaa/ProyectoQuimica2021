using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private CharacterController _characterController;
    private Rigidbody _rB;
    Vector3 movement;
    Vector3 normalizedMovement;
    Quaternion rotation = Quaternion.identity;
    
    private float horizontal;
    private float vertical;
    float moveForce;
    float turnSpeed=100;
    float playerSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _rB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
       
    }
    private void FixedUpdate()
    {
        CharacterMovement();
    }
   
    void CharacterMovement()
    {
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0);//if float horizontal isn't near to 0 then you're pressing the horizontal Input
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0);//" " vertical input
        movement.Set(horizontal, 0, vertical); //Sets movement's vectors


        normalizedMovement = movement;//Gets movement value to normalize it after without affecting the first
        normalizedMovement.Normalize();//Sets V3 value between 0 & 1


        Vector3 desiredForward = Vector3.RotateTowards
           (transform.forward, normalizedMovement, turnSpeed * Time.deltaTime, 0f); //Creates a Vector3 that saves player's desired direction to look at
        rotation = Quaternion.LookRotation(desiredForward);//Sets the direction that the player wants to look at

        if (hasHorizontalInput || hasVerticalInput) //Avoid moving to prevPos
        {
            _characterController.Move(movement * playerSpeed * Time.deltaTime);//Moves player's transform
            transform.SetPositionAndRotation(transform.position, rotation);//Sets position and rotation, requires V3 and Quat
        }
    }
}
