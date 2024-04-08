using System.Threading;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;
using Input = UnityEngine.Input;

// Find documentation on this script in the documentation folder, under the file name "FirstPersonController.txt"

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    IA_PlayerActions playerActions;
    private CharacterController controller;


    [Header("Grounded Variables")]
    [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
    public bool grounded = true;
    [Tooltip("Offset to mark feet position")]
    public float groundedOffset = 0.85f;
    [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
    public float groundedRadius = 0.5f;
    [Tooltip("What layers the character uses as ground")]
    public LayerMask groundLayers;

    [Space(10)]
    [Header("Jump Variables")]
    [Tooltip("The height the player can jump")]
    public float jumpHeight = 1.2f;
    [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
    public float gravity = -15.0f;
    public float terminalVelocity = 53.0f;
    [Tooltip("The fastest the character can fall")]
    public float maxFallSpeed = -50.0f;

    [Space(10)]
    [Header("Movement Variables")]
    [Tooltip("Move speed of the character in m/s")]
    public float moveSpeed = 6.0f;
    [Tooltip("Sprint speed of the character in m/s")]
    public float sprintSpeed = 10.0f;
    [Tooltip("Rotation speed of the character")]
    public float rotationSpeed = 2.0f;
    public Transform look;

    [Space(10)]
    [Header("Camera Limits")]
    public float minCameraAngle = -90F;
    public float maxCameraAngle = 90F;


    private float verticalVelocity;
    private Vector3 movementVector;
    private Vector3 updatedDirection;
    private Vector3 direction;
    private float targetSpeed;

    private Quaternion characterTargetRot;
    private Quaternion cameraTargetRot;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
        characterTargetRot = transform.localRotation;
        cameraTargetRot = look.localRotation;
    }

    private void Awake()
    {
        playerActions = new IA_PlayerActions();
    }

    private void OnEnable()
    {
        playerActions.PlayerActions.Enable();
        
        playerActions.PlayerActions.Jump.performed += ctx => Jump();
        
        playerActions.PlayerActions.Move.performed += ctx => onMovementPerformed(ctx);
        playerActions.PlayerActions.Move.canceled += ctx => onMovementCanceled();

        playerActions.PlayerActions.Sprint.performed += ctx => onSprintPerformed();
        playerActions.PlayerActions.Sprint.canceled += ctx => onSprintCanceled();

        playerActions.PlayerActions.Look.performed += ctx => LookRotation(ctx);

    }

    private void OnDisable()
    {
        playerActions.PlayerActions.Disable();
        
        playerActions.PlayerActions.Jump.performed -= ctx => Jump();
        
        playerActions.PlayerActions.Move.performed -= ctx => onMovementPerformed(ctx);
        playerActions.PlayerActions.Move.canceled -= ctx => onMovementCanceled();

        playerActions.PlayerActions.Sprint.performed -= ctx => onSprintPerformed();
        playerActions.PlayerActions.Sprint.canceled -= ctx => onSprintCanceled();

        playerActions.PlayerActions.Look.performed -= ctx => LookRotation(ctx);
    }

    private void Update()
    {
        ApplyGravity();
        controller.Move(movementVector * (targetSpeed * Time.deltaTime) + new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        // Stop increasing veritcal velocity when maximum fall speed is reached
        if (verticalVelocity > maxFallSpeed)
        {
            // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
            if (verticalVelocity < terminalVelocity)
            {
                verticalVelocity += gravity * Time.deltaTime;
            }
        }
    }

    private void Jump() 
    {
        // Check if grounded
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
        grounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);

        if (grounded)
        {
            // The square root of H * -2 * G = how much velocity needed to reach desired height
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    private void onMovementPerformed(InputAction.CallbackContext ctx)
    {
        // Register the basic direction (forward, backward, left, right)
        Vector2 horizontalVector = ctx.ReadValue<Vector2>();
        direction = new Vector3(horizontalVector.x, 0, horizontalVector.y);

        // Set speed
        targetSpeed = moveSpeed;

        SetMovementDirection();
    }

    private void onMovementCanceled()
    {
        // Stop moving
        movementVector = Vector3.zero;

        //Reset direction
        direction = Vector3.zero;
    }

    private void SetMovementDirection()
    {
        // Establish "forward" as the direction the camera is facing
        updatedDirection = direction.x * transform.right + direction.z * transform.forward;
        movementVector = updatedDirection.normalized;
    }

    private void onSprintPerformed()
    {
        targetSpeed = sprintSpeed;
    }

    private void onSprintCanceled()
    {
        targetSpeed = moveSpeed;
    }

    public void LookRotation(InputAction.CallbackContext ctx)
    {
        Vector2 lookVector = ctx.ReadValue<Vector2>();
        float yRot = lookVector.x * rotationSpeed;
        float xRot = lookVector.y * rotationSpeed;

        characterTargetRot *= Quaternion.Euler(0f, yRot, 0f);
        cameraTargetRot *= Quaternion.Euler(-xRot, 0f, 0f);

        cameraTargetRot = ClampRotationAroundXAxis(cameraTargetRot);
        transform.localRotation = characterTargetRot;
        look.localRotation = cameraTargetRot;

        SetMovementDirection();
    }

    Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);
        angleX = Mathf.Clamp(angleX, minCameraAngle, maxCameraAngle);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }
}    