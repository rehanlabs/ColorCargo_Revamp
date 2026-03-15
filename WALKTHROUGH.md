# Color Cargo: Architecture Walkthrough & Setup Guide

This document provides a walkthrough of the new data-driven architecture implemented in Phase 1 of the project revamp. It includes instructions on how to set up, configure, and test the new system.

---

## 1. Core Architecture Overview

The project has moved from a hardcoded, scene-specific approach to a **Data-Driven Architecture**.

### Key Benefits:
- **Scalability**: Add new levels and trains without writing new code.
- **Maintainability**: Centralized logic in managers and generic scripts.
- **Performance**: Optimized systems (e.g., coroutine-based raycasting in `Train.cs`).
- **Organization**: Code is now logically namespaced under `ColorCargo`.

### Folder Structure:
- `Assets/_Project/Scripts/Core`: Base gameplay logic (`Train`, `Track`, `CargoSpawner`).
- `Assets/_Project/Scripts/Data`: ScriptableObjects and Enums.
- `Assets/_Project/Scripts/Managers`: Centralized singleton managers (`GameManager`).

---

## 2. Using the New Data System

### 2.1 CargoColor (Enum)
Located in `ColorCargo.Data`. Defines the supported colors for the game (`Red`, `Blue`, `Yellow`, `Green`).

### 2.2 TrainData (ScriptableObject)
Defines the properties of a specific train color.
- **How to Create**: Right-click in Project window -> `Create` -> `ColorCargo` -> `Train Data`.
- **Fields**:
  - `Color`: The enum color.
  - `Hex Color`: The HTML color code (e.g., `#FF3217`).
  - `Emission Color`: HDR color for materials.
  - `Activation/Deactivation Particles/Sounds`: Prefabs and Clips specific to this color.

### 2.3 LevelData (ScriptableObject)
Defines a specific level's configuration.
- **How to Create**: Right-click in Project window -> `Create` -> `ColorCargo` -> `Level Data`.
- **Fields**:
  - `Trains`: A list of `TrainConfig` objects defining which trains are in the level and their speeds.
  - `Required Trains Moving`: How many trains need to start moving to trigger a win.
  - `Win Threshold`: Cargo count needed for individual train activation.
  - `Cargo Spawns`: Configuration for the `CargoSpawner`.

---

## 3. Component Setup in Scenes

To transition an existing scene to the new system, follow these steps:

### 3.1 GameManager
1. Create an empty GameObject named `GameManager`.
2. Attach the `GameManager.cs` script.
3. Assign a `LevelData` asset to the `Current Level Data` slot.
4. Assign UI references (`Level Win Panel`, `Timer Text`, `Best Time Text`).

### 3.2 Tracks
1. Select all track GameObjects that previously used `TrackM`, `TrackL`, etc.
2. Remove the legacy scripts.
3. Attach the new generic `Track.cs` script (from `ColorCargo.Core`).
4. Assign the `Object To Activate/Deactivate` (usually the visual indicator or barrier).
5. Ensure the track GameObject is on the `Track` layer.

### 3.3 Trains
1. Select your train GameObjects.
2. Ensure they use the updated `Train.cs`.
3. Assign the corresponding `TrainData` asset.
4. Set the `Track Layer` in the `Optimization` section to match your track GameObjects.
5. (Optional) Adjust `Raycast Interval` for performance (default is 0.1s).

### 3.4 Cargo Spawner
1. Create a GameObject at the spawn position.
2. Attach `CargoSpawner.cs`.
3. Configure the `Spawn Configs` list with the cargo prefabs and timing.

---

## 4. Testing & Verification

1. **Verify Initialization**: When the game starts, `CargoContainer` objects should automatically pull their color and emission settings from the `TrainData` assigned to them.
2. **Verify Movement**: Trains should start moving once they collect the required number of cargo items (defined in `Activate Threshold`).
3. **Verify Win Condition**: The `GameManager` should trigger the Win Panel once the required number of trains have started moving.
4. **Console Logs**: Check the console for any "Missing Reference" or "Null Reference" errors, which might indicate a component wasn't correctly reassigned in the Inspector.

---

## 5. Summary of Phase 1 Changes
- **Deleted Legacy Scripts**: `WinManager.cs`, `WinManager1.cs`, `WinManager4.cs`, `WinManager5.cs`, `TrackM.cs`, `TrackL.cs`, `TrackLL.cs`, `TrackRR.cs`.
- **Refactored for Namespaces**: `Train.cs`, `CargoContainer.cs`, `Bomb.cs`, `Countup.cs`.
- **New Core Scripts**: `GameManager.cs`, `Track.cs`, `CargoSpawner.cs`.
- **Data Containers**: `TrainData.cs`, `LevelData.cs`, `CargoColor.cs`.

---

*Note: This walkthrough will be updated after each phase of the revamp.*
