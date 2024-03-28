# Overview

## Purpose

This script manages the visibility of the canvas' UI elements.

## Attach

This script should be attached to the canvas object in the scene.

# Implementation Details

## Variables

- `Scope scope`: Reference to the `Scope` component used for toggling it's visibility.

## Methods

### `Start()`

- Description: Initializes the `scope` variable and starts the coroutine to toggle scope visibility.

### `IEnumerator ToggleScope()`

- Description: Coroutine that continuously checks if the player has a gun equipped and toggles the visibility of the scope accordingly.
- Called: Started in `Start()`.

### `private bool playerHasGun()`

- Description: Placeholder method indicating whether the player has a gun equipped.
- Returns: Always returns true in the current implementation (placeholder).