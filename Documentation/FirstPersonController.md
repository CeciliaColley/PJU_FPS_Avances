# Overview

`Start()`
Retrieves the CharacterController component for character movement and initializes rotation variables for consistent starting orientation.

`Awake()`
Initializes the playerActions variable with a new instance of `IA_PlayerActions`.
Reason: Initialization in `Awake()` ensures setup before other script execution, which is a Unity best practice for consistency and avoiding execution order issues.

`OnEnable()` and Event Handlers:
Enables player actions and sets up event handlers for input actions: jump, movement, sprint, and look rotation.

`Update()`
Executes once per frame, applying gravity to the character and moving it based on user input (movementVector) and vertical velocity.
Vertical velocity and the movement vector are updated within `ApplyGravity()`, and `onMovementPerformed()` and `LookRotation()` functions respectively.


###


## How it works

The `Awake()` function initializes the playerActions variable with a new instance of `IA_PlayerActions`. Then, the `Start()` function retrieves the `CharacterController` component attached to the game object and initializes rotation variables for the character and camera.

In the `OnEnable()` function, event handlers are set up for various input actions (jump, movement, sprint, and look). When the player performs any of these actions, the corresponding functions (`Jump(), onMovementPerformed/Canceled(), onSprintPerformed/Canceled(), and LookRotation()`) are called. These event handlers are removed in the `OnDisable()` function to prevent memory leaks when the script is disabled.

When the player performs movement input (`onMovementPerformed()`), the movement direction is determined based on the input values (a vector2) and adjusted relative to the camera's orientation. The target speed determines the players speed and is set based on whether the player is sprinting or not.

The `SetMovementDirection()` function calculates the movement vector based on the updated direction.

In the `Update()` function, the character's position is updated using `controller.Move(`) with the movement vector and vertical velocity, allowing the character to move within the game environment.

Since the player can only jump if they�re grounded, when the player performs the jump action (`Jump()`), the script checks if the character is grounded using a sphere cast. If grounded, the vertical velocity is calculated to achieve the desired jump height, allowing the character to jump, and the vertical velocity is applied in the `Update()` function, affecting the character's movement. The `ApplyGravity()` function applies gravity to the character by adjusting its vertical velocity over time, and eventually settling at a constant vertical velocity of -50. It would be a good idea to reset vertical velocity to 0 when the jump has already been performed in future updates.

When the player moves the camera (`LookRotation()`), rotation inputs are used to update the character and camera rotations. The movement direction is also updated relative to the new camera orientation, ensuring that movement controls align with the player's visual perspective.
