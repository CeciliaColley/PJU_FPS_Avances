# Overview

## Purpose

This script detects if an enemy is in the center of the screen by casting the same ray as `Shoot()` in `PlayerCombat`. If this ray intersects with an object on a designated layer (defined by the `LayerMask enemies`), it returns true, indicating the presence of an enemy. Otherwise, it returns false. `Scope` uses this method to change color depending on if the gun is aiming at an enemy or not.

## Variables

`public LayerMask enemies` which must be set up in the engine to the layer where the ojects that should be detected are.
`private Camera look` is the camera the player uses to shoot.

## Attach

This script should be attached to the gun. The gun should be a child of the player. 
