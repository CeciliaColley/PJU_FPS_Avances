# Overview

## Purpose

This script handles player combat mechanics, including shooting with a gun, if equipped, and dealing damage to enemies.

## Attach

This script should be attached to the player character in the scene.

# Implementation Details

## Variables

- `IA_PlayerActions playerActions`: Reference to the input actions for the player.
- `private RaycastHit objectHit`: Information about the object hit by a raycast.
- `private Camera look`: Reference to the camera used for aiming.
- `private Gun gun`: Reference to the gun. The gun should be a child of the player.

## Methods

### `Awake()`

- Description: Initializes the `playerActions` variable.
- Called: Upon script instantiation.

### `Start()`

- Description: Initializes the `look` variable.
- Called: Before the first frame update.

### `OnEnable()`

- Description: Enables the input actions related to gun actions and subscribes to the shoot event.
- Called: When the script is enabled.

### `OnDisable()`

- Description: Disables the input actions related to gun actions and unsubscribes from the shoot event.
- Called: When the script is disabled.

### `Shoot()`

- Description: Performs shooting action if the gun is equipped, deducts bullets, and deals damage to enemies if hit.
- Called: When the shoot input action is triggered.
- Relies on: a gun being equipped and detected as a child of the player that this script is attached to.
