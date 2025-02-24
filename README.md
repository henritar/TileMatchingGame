# **Tile Matching Game - Unity Project**  

Welcome to **Tile Matching Game**, a Unity-based puzzle game where players eliminate groups of connected tiles to complete objectives and score points. Built with **Unity 2020.3.30 LTS**, this project follows **solid software architecture principles** and **design patterns** to ensure **scalability, modularity, and maintainability**.  

---

## **1. Core Systems**  

This game follows an **MVC-based architecture**, separating logic into **Model, View, and Controller**, while utilizing **event-driven systems** and **dependency injection** for efficient communication between components.  

### **Game Board and Tile System**  

- The board consists of a **grid of tiles**, each represented by a `Tile` instance.  
- **Tile attributes** (color, sprite) are stored in `TileData`, implementing the **Flyweight pattern** to reduce memory usage.  
- `Board` manages tile placement and updates, while `BoardLayoutCalculator` acts as an **Adapter**, converting board coordinates into world positions.  
- `MatchFinder` detects groups of matching tiles using the **Observer pattern** to trigger game logic events.  
- `BoardModifier` handles **tile removal, gravity simulation, and refilling** new tiles through `TileFactory`.  

### **Game Objectives and Level System**  

- Objectives are defined using the **Strategy pattern**, allowing different goal types:  
  - `CollectColorTilesGoal` → Remove a specific number of tiles of a certain color.  
  - `CollectTilesPointsGoal` → Reach a target score by matching tiles.  
  - `MaxMovesGoal` → Complete the level within a move limit.  
- `LevelManager` loads levels dynamically using **ScriptableObjects**, while `LevelButtonFactory` generates level selection buttons.  

### **Game Flow and UI**  

- `GameManager` manages game states using the **State pattern**, transitioning between:  
  - `PlayingState` → Active gameplay mode.  
  - `PauseState` → Freezes all interactions.  
  - `ShowGoalsState` → Displays level objectives.  
  - `VictoryState` → Triggers when objectives are met.  
  - `GameOverState` → Ends the level when conditions are not met.  
- `GameHUD` dynamically updates **score, objectives, and game status** using **event-driven communication**.  

### **User Interaction and Input Handling**  

- `GameplayController` processes player actions (tap/click) and delegates them to `GameManager`, applying the **Command pattern** for structured input handling.  
- `TileViewPool` implements **Object Pooling**, improving performance by reusing UI elements instead of constantly instantiating new ones.  

---

## **2. Technical Design**  

### **Design Patterns Used**  

The project integrates multiple **design patterns** to ensure clean architecture and maintainability:  

- **Flyweight** → `TileData` reduces redundant sprite and color allocations.  
- **Factory Method** → `TileFactory` and `LevelButtonFactory` dynamically generates new tiles and buttons.  
- **Singleton** → `GameplayController` and `DIContainer` ensure a single instance of key controllers.  
- **Strategy** → `IGoal` allows for different game objectives without modifying core logic. `IMatchFinder` can also be used as an exemplo of Strategy for new match finding algori
- **State** → `GameManager` orchestrates game state transitions.  
- **Observer** → `MatchFinder` and `GameHUD` use event-driven updates.  
- **Adapter** → `BoardLayoutCalculator` converts logical board coordinates into world positions.  
- **Object Pooling** → `TileViewPool` optimizes UI performance by reusing elements.  
- **Command (Partial)** → `GameplayController` structures input handling.  

These patterns ensure that the project remains **modular, scalable, and easy to maintain**.  

### **Interfaces for Flexibility and Testability**  

To improve code maintainability, flexibility, and ease of testing, several key components implement interfaces:  

- **`IBoard`** → Defines the contract for the game board, making it possible to swap implementations or mock it in tests.  
- **`IMatchFinder`** → Abstracts the match-finding logic, allowing for different matching algorithms.  
- **`IScoreManager`** → Provides a structured way to handle scoring logic.  
- **`ITileFactory`** → Encapsulates the creation of tiles, making it possible to modify tile generation logic without affecting other components.  

These interfaces facilitate unit testing by enabling dependency injection and reducing tight coupling between components.  

---

## **3. Setup and Installation**  

### **Prerequisites**  

- Unity **2020.3.30 LTS** or later.  
- No additional packages or external dependencies are required.

### **Installation Steps**  

1. **Clone the repository**:  

   ```sh
   git clone https://github.com/your-repo/tile-matching-game.git
   ```

2. **Open the project in Unity.**  
3. **Run the game** by pressing Play in the Unity Editor.  

---

## **4. License**  

This project is licensed under the **MIT License**.
