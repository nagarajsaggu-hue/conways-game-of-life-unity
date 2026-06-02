# Object-Oriented Modelling and Programming in Engineering

**Author:** Saggu Nagaraju

## Project Overview

This project is a Unity and C# implementation of a cellular automata population simulation. The simulation models a population where each cell can exist in one of two states: **alive** or **dead**. The next generation of the population is calculated from the previous generation using the Moore neighborhood, where each cell checks the eight surrounding neighboring cells.

The project demonstrates object-oriented programming concepts by separating the simulation into multiple classes for cell behavior, population evolution, pattern placement, grid rendering, and game control. The final result is a visual grid-based simulation that shows how different starting patterns evolve over time.

## Main Objective

The main objective of this project is to build a software model that:

- represents a population as a two-dimensional grid of cells,
- applies cellular automata evolution rules generation by generation,
- visualizes live and dead cells using colored square objects,
- supports multiple predefined starting patterns,
- displays the current generation count,
- demonstrates the behavior of patterns at selected generations such as 1, 5, 10, 20, and 50.

## Concept

The project is based on a cellular automata system similar to Conway’s Game of Life. Each cell has eight possible neighbors around it:

- top-left,
- top,
- top-right,
- left,
- right,
- bottom-left,
- bottom,
- bottom-right.

Each generation is calculated from the state of the previous generation. The current generation must be fully evaluated before updating the next generation, so that the states are not mixed during the calculation.

## Evolution Rules

The simulation follows these rules:

| Current State | Condition | Next State | Explanation |
|---|---|---|---|
| Dead | Exactly 3 living neighbors | Alive | A new cell is born |
| Dead | Any other number of living neighbors | Dead | The cell remains dead |
| Alive | Fewer than 2 living neighbors | Dead | The cell dies due to loneliness |
| Alive | 2 or 3 living neighbors | Alive | The cell survives |
| Alive | More than 3 living neighbors | Dead | The cell dies due to overpopulation |

## Technologies Used

| Technology | Purpose |
|---|---|
| Unity | Simulation environment and visualization |
| C# | Main programming language |
| MonoBehaviour | Unity component-based scripting |
| SpriteRenderer | Displaying alive/dead cells visually |
| Unity UI Text | Displaying generation count |
| GL Lines | Drawing grid lines |

## Project Structure

A clean repository structure can be organized as follows:

```text
Object-Oriented-Modelling-and-Programming-in-Engineering/
│
├── Assets/
│   ├── Scripts/
│   │   ├── Game.cs
│   │   ├── Cell.cs
│   │   ├── GridLines.cs
│   │   ├── PatternManager.cs
│   │   └── Pattern.cs
│   │
│   └── Resources/
│       └── Prefabs/
│           └── Cell.prefab
│
├── Reports/
│   ├── OBJECT-ORIENTED MODELLING AND PROGRAMMING IN ENGINEERING 1 1.pdf
│   └── homework 2 Tasks.pdf
│
└── README.md
```

> Note: If the uploaded files are named `gridelines.cs` or `Pattern Maneger.cs`, rename them to `GridLines.cs` and `PatternManager.cs` before adding them to the Unity project. This makes the file names match the C# MonoBehaviour class names and avoids script attachment problems in Unity.

## Main Classes

### `Game.cs`

`Game.cs` is the main controller of the simulation. It manages the complete flow of the cellular automata system.

Main responsibilities:

- creates the 40 × 40 cell grid,
- instantiates cell prefabs,
- places the initial patterns,
- controls the simulation timer,
- counts living neighbors for each cell,
- applies the evolution rules,
- updates the generation number,
- updates the generation text in the Unity UI.

Important variables:

```csharp
private static int GridWidth = 40;
private static int GridHeight = 40;
public float SimulationSpeed = 0.1f;
public bool IsSimulationActive = false;
private Cell[,] CellGrid = new Cell[GridWidth, GridHeight];
```

The simulation runs only when `IsSimulationActive` is set to `true`. The update loop uses a timer so that the generations evolve at a controlled speed instead of updating every frame.

### `Cell.cs`

`Cell.cs` represents one cell in the population grid.

Main responsibilities:

- stores whether the cell is alive or dead,
- stores the number of living neighbors,
- updates the visual state of the cell,
- enables or disables the `SpriteRenderer` depending on the cell state.

Important logic:

```csharp
public void SetState(bool alive)
{
    isAlive = alive;
    UpdateSpriteRenderer();
}
```

When a cell becomes alive, its sprite is shown. When it becomes dead, the sprite is hidden.

### `PatternManager.cs`

`PatternManager.cs` stores predefined starting patterns. These patterns are placed on the 40 × 40 grid by setting selected cells to alive.

Available patterns:

- `Pattern1()`
- `Pattern2()`
- `Pattern3()`

Each pattern is created using a group of coordinate positions. The method `BatchSetAlive()` applies the alive state to all positions in the selected pattern.

Example:

```csharp
private static void BatchSetAlive(Cell[,] grid, int startX, int startY, params (int, int)[] positions)
{
    foreach (var (x, y) in positions)
    {
        grid[startX + x, startY + y].SetState(true);
    }
}
```

### `GridLines.cs`

`GridLines.cs` is responsible for drawing the visual grid. It uses Unity’s `GL.LINES` rendering approach to draw horizontal and vertical grid lines.

Main responsibilities:

- creates a line material,
- configures alpha blending,
- configures depth writing,
- disables backface culling,
- draws main grid lines,
- optionally draws smaller sub-grid lines.

This class helps make the simulation easier to understand visually because the cell positions are clearly separated in a 40 × 40 field.

### `Pattern.cs`

`Pattern.cs` is a simple data class containing a pattern string. It can be extended later if pattern loading from text or external files is added.

```csharp
public class Pattern
{
    public string patternString;
}
```

## Simulation Workflow

The complete simulation follows this workflow:

```text
Start Unity scene
        ↓
Create 40 × 40 grid
        ↓
Instantiate Cell prefabs
        ↓
Set all cells to dead
        ↓
Place selected starting pattern
        ↓
Start simulation
        ↓
Count living neighbors for every cell
        ↓
Apply evolution rules
        ↓
Update alive/dead cell states
        ↓
Increase generation count
        ↓
Repeat until simulation is stopped
```

## Algorithm Explanation

### 1. Grid Creation

The `PlaceCells()` method creates the full population field. It loops through all `x` and `y` positions and instantiates one `Cell` prefab for each position.

```csharp
for (int y = 0; y < GridHeight; y++)
{
    for (int x = 0; x < GridWidth; x++)
    {
        CellGrid[x, y] = InstantiateCell(x, y);
    }
}
```

### 2. Neighbor Counting

The `CountNeighbours()` method calculates the number of living neighbors for every cell. It checks all eight surrounding positions using nested loops from `-1` to `1` in both directions.

```csharp
for (int dx = -1; dx <= 1; dx++)
{
    for (int dy = -1; dy <= 1; dy++)
    {
        if (dx == 0 && dy == 0) continue;
        // check neighbor cell
    }
}
```

The method also checks whether the neighbor position is inside the grid boundaries before reading the cell state.

### 3. Population Update

The `PopulationControl()` method applies the cellular automata rules.

```csharp
CellGrid[x, y].SetState(
    isAlive ? (numNeighbours == 2 || numNeighbours == 3) : numNeighbours == 3
);
```

This one line represents the complete rule system:

- live cells survive only with 2 or 3 neighbors,
- dead cells become alive only with exactly 3 neighbors.

### 4. Visualization

Each cell is visualized by enabling or disabling its `SpriteRenderer`. Alive cells are visible. Dead cells are hidden. The grid lines are drawn separately by the `GridLines` class.

## Pattern Experiments

The report analyzes three starting patterns in a 40 × 40 field. Each pattern was observed at different generations:

- Generation 1,
- Generation 5,
- Generation 10,
- Generation 20,
- Generation 50.

### Pattern 1

Pattern 1 behaves like a stable block pattern. The cells remain in the same arrangement across the observed generations because each living cell has enough neighbors to survive and no extra cell is created around it.

Observed behavior:

- stable structure,
- no major movement,
- no expansion,
- same compact block shape over time.

### Pattern 2

Pattern 2 evolves into a moving and changing structure. The pattern changes its shape across generations and shifts position over time.

Observed behavior:

- visible structural changes,
- movement across the grid,
- different shape at generation 5, 10, 20, and 50,
- demonstrates dynamic cellular automata behavior.

### Pattern 3

Pattern 3 produces more complex growth behavior. It starts as a small group of live cells and gradually expands into a larger structure.

Observed behavior:

- early-stage local transformation,
- larger pattern formation by generation 20,
- clear expansion by generation 50,
- visually interesting long-term evolution.

## How to Run the Project

### 1. Install Unity

Install Unity Hub and create or open a Unity 2D project.

Recommended setup:

- Unity 2020 or newer,
- 2D project template,
- C# scripting support enabled.

### 2. Add the Scripts

Create a folder named `Scripts` inside the Unity `Assets` folder and add the following files:

```text
Game.cs
Cell.cs
GridLines.cs
PatternManager.cs
Pattern.cs
```

Make sure the filenames match the class names exactly.

### 3. Create the Cell Prefab

Create a cell prefab using these steps:

1. Create a small square object in the Unity scene.
2. Add a `SpriteRenderer` component.
3. Attach the `Cell.cs` script.
4. Convert the object into a prefab.
5. Save it under:

```text
Assets/Resources/Prefabs/Cell.prefab
```

This path is important because `Game.cs` loads the cell prefab using:

```csharp
Resources.Load<Cell>("Prefabs/Cell")
```

### 4. Create the Game Controller

1. Create an empty GameObject in the scene.
2. Name it `GameController`.
3. Attach the `Game.cs` script.
4. Assign a UI Text object to `GenerationText` if you want to display the generation count.
5. Set `IsSimulationActive` to `true` in the Inspector to start the simulation automatically.

### 5. Add Grid Visualization

1. Select the camera or another suitable GameObject.
2. Attach the `GridLines.cs` script.
3. Set the grid size to:

```text
gridSizeX = 40
gridSizeY = 40
```

4. Adjust the start position and step size according to the scene view.

### 6. Select One Pattern

Inside `Game.cs`, the `PlaceCells()` method currently calls multiple patterns:

```csharp
PatternManager.Pattern3(CellGrid, 0, 0);
PatternManager.Pattern1(CellGrid, 0, 0);
PatternManager.Pattern2(CellGrid, 0, 0);
```

For clean testing, keep only one pattern active at a time. Example:

```csharp
PatternManager.Pattern1(CellGrid, 0, 0);
// PatternManager.Pattern2(CellGrid, 0, 0);
// PatternManager.Pattern3(CellGrid, 0, 0);
```

Then run the simulation and observe the selected pattern.

### 7. Run the Scene

Press the **Play** button in Unity. The grid will be generated, the selected pattern will appear, and the population will evolve according to the rules.

## Expected Output

After running the project, the user should see:

- a 40 × 40 grid,
- visible live cells as colored squares,
- dead cells hidden or empty,
- generation count displayed in the UI or console,
- pattern evolution over time,
- clear changes in the grid at different generations.

## Results Summary

| Pattern | Main Behavior | Result |
|---|---|---|
| Pattern 1 | Stable block | Remains almost unchanged across generations |
| Pattern 2 | Dynamic movement | Shape changes and moves across the grid |
| Pattern 3 | Growth and expansion | Develops into larger complex structures |

## My Contribution

I was involved in the complete project workflow, including:

- understanding the cellular automata problem statement,
- analyzing the evolution rules,
- designing the software structure,
- implementing the C# scripts,
- creating the cell data model,
- implementing neighbor-counting logic,
- implementing population evolution control,
- creating predefined starting patterns,
- building the Unity grid visualization,
- testing the simulation output,
- observing pattern behavior across multiple generations,
- preparing the final documentation and diagrams.

## Skills Demonstrated

This project demonstrates practical skills in:

- object-oriented programming,
- C# scripting,
- Unity development,
- MonoBehaviour lifecycle methods,
- grid-based simulation,
- cellular automata algorithms,
- neighbor-search logic,
- 2D visualization,
- software structure design,
- debugging and testing,
- technical documentation.

## Possible Improvements

The current implementation works as a basic cellular automata simulator. Future improvements could include:

- adding UI buttons for Start, Pause, Reset, and Step,
- adding a dropdown menu to select Pattern 1, Pattern 2, or Pattern 3,
- allowing users to draw custom starting patterns with the mouse,
- adding camera zoom and pan controls,
- saving generation snapshots automatically,
- exporting population states to CSV,
- improving separation between model, view, and controller,
- supporting larger grid sizes,
- adding toroidal boundary conditions,
- adding unit tests for neighbor counting and rule application.

## Portfolio Summary

This repository presents a complete Unity and C# cellular automata simulation project. It shows the ability to translate a mathematical rule-based system into a working object-oriented software model, visualize the simulation in Unity, and document the design and results clearly. The project is suitable for demonstrating programming, simulation, and software design skills in a technical portfolio.
