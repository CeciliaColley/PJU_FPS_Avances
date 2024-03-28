# Overview

## Purpose

A basic class from which to create guns.

## Variables

All guns come with:

`LayerMask enemies` to distinguish what objects are shootable.
`float hitDamage` for the amount of damage each bullet deals.
`int bullets` for how many times the gun can shoot without reloading.

These values are used by `PlayerCombat` in the during the `Shoot()` event.

## Attach

This script should be attached to the gun. The gun should be a child of the player.

