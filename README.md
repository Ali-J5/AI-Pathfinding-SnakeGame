# AI Pathfinding Snake Game

## Description
A classic Snake game built in C# with a .NET Windows Forms interface. This project features both manual user-controlled play and an AI agent that uses graph algorithms to navigate the board.

The project explores two different pathfinding strategies for the AI: a shortest-path algorithm (BFS) and a 'perfect play' algorithm (Hamiltonian Path).

## Implemented Algorithms

### 1. Breadth-First Search (BFS)
* **Purpose:** Used to find the shortest possible path from the snake's head to the food.
* **Status:** Fully implemented and functional. This algorithm is effective for basic, direct navigation.

### 2. Hamiltonian Path
* **Purpose:** A more advanced algorithm to find a single, space-filling path that visits every tile on the board exactly once. This is a common strategy for a 'perfect' Snake AI that will never trap itself.
* **Status:** Implemented to provide a 'perfect play' path for the AI to follow.

## Technologies Used
* **Language:** C#
* **Framework:** .NET (Windows Forms)
* **Key Data Structures:** Graphs (represented by a 2D array of `GameNode`), `Queue`, `Stack`, `Dictionary`.

## How to Run
1.  Clone the repository.
2.  Open the `KSU.CIS300.Snake.sln` solution file in Visual Studio.
3.  Build and run the project.
