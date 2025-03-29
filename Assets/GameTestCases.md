# Test Case Document for UWindsor Tours Unity Game

## 1. Player Existence in Scene
- **Test ID:** TC001
- **Objective:** Ensure the player object exists in the scene.
- **Steps:**
  1. Load the game scene.
  2. Check if the player object is instantiated.
- **Expected Result:** The player object is present in the scene.

## 2. Player Movement
- **Test ID:** TC002
- **Objective:** Verify that the player can move.
- **Steps:**
  1. Move the player forward.
  2. Check if the position has changed.
- **Expected Result:** Player position updates accordingly.

## 3. NPC Interaction
- **Test ID:** TC003
- **Objective:** Verify NPC responds to the player.
- **Steps:**
  1. Move player near NPC.
  2. Trigger interaction.
  3. Check if NPC responds.
- **Expected Result:** NPC reacts to interaction.

## 4. Scene Loading
- **Test ID:** TC004
- **Objective:** Ensure correct scene loads at start.
- **Steps:**
  1. Start the game.
  2. Check the active scene name.
- **Expected Result:** The correct scene loads.

## 5. Collision Detection
- **Test ID:** TC005
- **Objective:** Verify physics-based collisions work.
- **Steps:**
  1. Move player towards an NPC.
  2. Detect if collision occurs.
- **Expected Result:** Collision is registered.

## 6. Player Interaction with Objects
- **Test ID:** TC006
- **Objective:** Check if the player can interact with objects.
- **Steps:**
  1. Move player to an interactable object.
  2. Trigger interaction.
  3. Check if interaction flag is set.
- **Expected Result:** Object responds to player interaction.

## 7. NPC Pathfinding
- **Test ID:** TC007
- **Objective:** Ensure NPCs navigate correctly.
- **Steps:**
  1. Set an NPC’s destination.
  2. Check if NPC moves to the destination.
- **Expected Result:** NPC reaches the target position.

## 8. Player Health System
- **Test ID:** TC008
- **Objective:** Ensure player coin gets increased/no change for each correct/incorrect answer
- **Steps:**
  1. Attempt the Quiz.
  2. Answer correctly/Incorrectly and then check the knowledge coins.
- **Expected Result:** Coins should be increased for each correct answer/No change if wrong answer.

## 9. Game Over on Player Death
- **Test ID:** TC009
- **Objective:** Verify the game-over state triggers when the player’s exit.
- **Steps:**
  1. Click on Quit Game.
  2. Check if game-over state is triggered.
- **Expected Result:** Game-over screen appears.

## 10. UI Elements Presence
- **Test ID:** TC010
- **Objective:** Ensure UI elements exist.
- **Steps:**
  1. Load the scene.
  2. Check if health bar and UI canvas exist.
- **Expected Result:** UI elements are present.

### 11. Player Jump Mechanism
- **Test ID:** TC011
- **Objective:** Verify that the player can jump.
- **Steps:**
  1. Press the jump button (`Space` key or controller button).
  2. Observe if the player moves upward.
- **Expected Result:** The player successfully jumps.

### 12. Player Sprint Functionality
- **Test ID:** TC012
- **Objective:** Ensure the player can sprint.
- **Steps:**
  1. Hold down the sprint button (`Shift`).
  2. Move the player forward.
  3. Compare the movement speed with normal walking speed.
- **Expected Result:** The player moves faster while sprinting.

### 13. Checkpoint System Functionality
- **Test ID:** TC013
- **Objective:** Ensure checkpoints save player progress.
- **Steps:**
  1. Reach a checkpoint.
  2. Die or restart the level.
  3. Check if the player respawns at the last checkpoint.
- **Expected Result:** Player respawns at the saved checkpoint.

### 14. Environmental Interactions (Doors, Switches, etc.)
- **Test ID:** TC014
- **Objective:** Verify interaction with environment objects.
- **Steps:**
  1. Approach an interactive object (e.g., door, switch).
  2. Press the interaction button.
  3. Observe if the object responds (e.g., door opens).
- **Expected Result:** The object reacts correctly to interaction.

### 15. Game Pause and Resume
- **Test ID:** TC015
- **Objective:** Verify that the game pauses and resumes correctly.
- **Steps:**
  1. Press the pause button (`ESC` or equivalent).
  2. Check if the game freezes and the pause menu appears.
  3. Resume the game.
  4. Ensure gameplay resumes normally.
- **Expected Result:** The game pauses and resumes without issues.

### 16. Main Menu Navigation
- **Test ID:** TC016
- **Objective:** Verify that menu navigation works properly.
- **Steps:**
  1. Open the main menu.
  2. Navigate through different options (Start, Settings, Exit).
- **Expected Result:** Menu options function as expected.

### 17. Settings Menu Adjustments
- **Test ID:** TC017
- **Objective:** Ensure settings (audio, graphics, controls) can be changed.
- **Steps:**
  1. Open the settings menu.
  2. Adjust sound and graphics settings.
  3. Save and apply changes.
- **Expected Result:** Changes should persist and affect gameplay.

### 18. UI Responsiveness on Different Resolutions
- **Test ID:** TC018
- **Objective:** Check if UI scales correctly at different screen resolutions.
- **Steps:**
  1. Change screen resolution settings.
  2. Observe UI scaling and readability.
- **Expected Result:** UI elements remain properly aligned and readable.

## Audio & Visuals

### 19. Background Music and Sound Effects
- **Test ID:** TC019
- **Objective:** Ensure background music and sound effects play correctly.
- **Steps:**
  1. Start the game and check for background music.
  2. Perform actions that should trigger sound effects (e.g., jumping, interacting).
- **Expected Result:** Background music plays, and sound effects trigger correctly.

### 20. Graphical Rendering and Texture Loading
- **Test ID:** TC020
- **Objective:** Ensure textures and models load correctly.
- **Steps:**
  1. Observe textures of terrain, objects, and characters.
  2. Check for any missing or flickering textures.
- **Expected Result:** All game assets load properly without visual glitches.
