# Overview

## Purpose

This script detects if an enemy is in the center of the screen by casting the same ray as `Shoot()` in `PlayerCombat`. If this ray intersects with an object on a designated layer (defined by the `LayerMask enemies`), it turns enemyDetected true, indicating the presence of an enemy. Otherwise, enemyDetected is false. 

`Scope` uses this script to change color depending on if the gun is aiming at an enemy or not.
`LifeBar`uses this script to determine whether or not to show an enemies life bar.

## Variables

- `public LayerMask enemies` which must be set up in the engine to the layer where the ojects that should be detected are.
- `private Camera look` is the camera the player uses to shoot.
- `private bool enemyDetected` is a bool that other scripts can access to see if an enemy is being aimed at or not.

## Attach

This script should be attached to the gun. The gun should be a child of the player. 
