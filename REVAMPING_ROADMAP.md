# Color Cargo: Detailed Revamping Roadmap

This roadmap outlines the technical strategy to modernize the "Color-Cargo" project, focusing on architectural scalability, refined visuals, and performance, with Live Ops integration as a final step.

---

## Phase 1: Core Architecture & Data-Driven Scalability
**Objective**: Eliminate code duplication and hardcoded configurations to allow for rapid level creation.

### 1.1 ScriptableObject Level System [HIGH PRIORITY]
*   **Create `LevelData` ScriptableObject**: Define properties for each level:
    *   `List<TrainConfig>` (Colors, starting positions, speed multipliers).
    *   `List<CargoConfig>` (Spawn rates, allowed colors, speed ranges).
    *   `WinThreshold` (Cargo count needed to win).
    *   `SceneMetadata` (Environment theme, background music).
*   **Result**: New levels can be created in the Editor without writing a single line of code.

### 1.2 Generic Gameplay Refactoring
*   **Unified Train logic (`Train.cs`)**: Refactor to be entirely data-driven. A single script should handle any color based on the assigned `LevelData`.
*   **Smart Cargo Spawner**: Create a centralized spawner that reads `LevelData` to decide which cargo to spawn and at what frequency, replacing hardcoded spawners.
*   **Event-Driven Win/Loss**: Implement a `GameEvent` system (using C# Actions). Scripts like `Train` and `CargoContainer` will "Broadcast" events, and a single `WinManager` will "Listen" to them.

### 1.3 Cleanup & Standardization
*   **Delete Duplicates**: Remove `WinManager1.cs`, `WinManager4.cs`, etc.
*   **Standardize Tags/Layers**: Move away from string-based comparisons (`CompareTag("Track")`) towards a LayerMask or Interface-based detection (`IInteractable`).

---

## Phase 2: Visual Polish & UI Refinement
**Objective**: Enhance the "Look & Feel" using existing rendering capabilities.

### 2.1 Material & Lighting Optimization
*   **Material Instancing**: Use `MaterialPropertyBlock` for changing train/cargo colors to reduce draw calls.
*   **Baked Lighting Cleanup**: Perform a clean lightmap bake for all static environment objects to improve mobile performance and visual depth.
*   **Post-Processing**: Add a subtle `Bloom` and `Color Grading` (if using Post-Processing Stack v2) to make colors "pop".

### 2.2 UI Modernization (TextMeshPro)
*   **TMPro Migration**: Replace all legacy `UI.Text` with `TextMeshProUGUI` for superior font rendering and effects (outlines, glows).
*   **Responsive Layouts**: Rebuild UI anchors and pivots to ensure the game looks perfect on everything from iPads to ultra-wide phones.

---

## Phase 3: Optimization & Performance
**Objective**: Ensure 60 FPS on mid-range mobile devices.

### 3.1 Object Pooling [CRITICAL]
*   **Cargo Pooling**: Instantiate a fixed number of cargo objects and reuse them instead of constant `Instantiate/Destroy` (reduces Garbage Collection spikes).
*   **VFX Pooling**: Apply the same logic to explosion and activation particles.

### 4.2 Code Efficiency
*   **Update Optimization**: Move Raycasting logic in `Train.cs` out of `Update()` and into a timed `Coroutine` (e.g., every 0.1s instead of every frame).
*   **Audio Management**: Create a `SoundManager` singleton to handle all SFX/BGM, preventing multiple background musics from playing simultaneously.

---

## Phase 4: Live Ops & Production Readiness
**Objective**: Enable remote management and deep player insights (To be discussed later).

### 4.1 Remote Configuration (Firebase)
*   **Integrate Remote Config**: Move gameplay balance variables (e.g., `DefaultMoveSpeed`, `ActivationThreshold`) to the cloud.
*   **A/B Testing**: Enable the ability to test different difficulty settings for segments of players.

### 4.2 Advanced Analytics & Retention
*   **Funnel Tracking**: Implement tracking for the player journey (Tutorial Start -> Tutorial End -> Level 5 Complete).
*   **Economy/Difficulty Logs**: Log failed levels to identify "choke points".
*   **Crashlytics**: Ensure every exception is logged in real-time for proactive debugging.

### 4.3 Cloud Progress & Save
*   **Integrate Unity Gaming Services (UGS) Save**: Allow players to sync progress across devices.
*   **Offline Mode**: Ensure the game remains playable without connection, syncing once back online.

---

### Implementation Sequence (Recommended)
1.  **Architecture (Phase 1)**: The essential foundation.
2.  **UI/Visuals (Phase 2)**: Improving the look while systems stabilize.
3.  **Optimization (Phase 3)**: Ensuring performance before launch.
4.  **Live Ops (Phase 4)**: Final production layer and maintenance.
