# Overview

## Purpose

This script is responsible for changing the color of a scope based on whether the gun is pointed at an enemy or not. It relies on an `EnemyDetection` component attached to the gun to determine enemy presence.

## Variables

- `public Color enemyDetectedColor`: Color of the scope when the gun is pointed at an enemy.
- `public Color noEnemyDetectedColor`: Color of the scope when the gun isn't pointed at an enemy.

## Attach

This script should be attached to the scope UI element.