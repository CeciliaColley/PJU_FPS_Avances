using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    IA_PlayerActions playerActions;
    private Vector2 movementInput = Vector2.zero;
    private Vector2 lookInput = Vector2.zero;
    private float movementDirectionY;
    private bool isRunning = false;
    private bool jumped = false;

    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;

    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;

    CharacterController characterController;

    private void Awake()
    {
        playerActions = new IA_PlayerActions();
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        playerActions.PlayerActions.Enable();

        playerActions.PlayerActions.Jump.performed += ctx => Jump();

        playerActions.PlayerActions.Move.performed += ctx => onMovementPerformed(ctx);
        playerActions.PlayerActions.Move.canceled += ctx => onMovementCanceled(ctx);

        playerActions.PlayerActions.Sprint.performed += ctx => onSprintPerformed();
        playerActions.PlayerActions.Sprint.canceled += ctx => onSprintCanceled();

        playerActions.PlayerActions.Look.performed += ctx => LookRotation(ctx);
    }

    private void OnDisable()
    {
        playerActions.PlayerActions.Disable();

        playerActions.PlayerActions.Jump.performed -= ctx => Jump();

        playerActions.PlayerActions.Move.performed -= ctx => onMovementPerformed(ctx);
        playerActions.PlayerActions.Move.canceled -= ctx => onMovementCanceled(ctx);

        playerActions.PlayerActions.Sprint.performed -= ctx => onSprintPerformed();
        playerActions.PlayerActions.Sprint.canceled -= ctx => onSprintCanceled();

        playerActions.PlayerActions.Look.performed -= ctx => LookRotation(ctx);
    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Press Left Shift to run
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * movementInput.y : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * movementInput.x : 0;
        movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (jumped && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
            jumped = false;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);
    }

    void Jump()
    {
        jumped = true;
        
    }

    void onMovementPerformed(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    void onMovementCanceled(InputAction.CallbackContext context)
    {
        movementInput = Vector2.zero;
    }

    void onSprintPerformed()
    {
        isRunning = true;
    }

    void onSprintCanceled()
    {
        isRunning = false;
    }

    void LookRotation(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();

        if (canMove)
        {
            rotationX += -lookInput.y * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, lookInput.x * lookSpeed, 0);
        }
    }
}