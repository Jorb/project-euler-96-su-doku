# project-euler-96-su-doku
My attempt at solving the project Euler Su Doku problem: https://projecteuler.net/problem=96

# My Method
First pass, I am going to try to write it by hand. I have never really played SuDoku so I want to use this exercise to help myself understand the game better.

1. Looked up basic SuDoku algorithms: https://en.wikipedia.org/wiki/Sudoku_solving_algorithms
- Decided on backtracking brute force search for simplicity
2. Chose C# .NET Console app to implement.
3. Decided on success metric.
- Validate each puzzle
- Print solved puzzles to the console
4. First pass basic types
- Puzzle Classes: SudokuPuzzle, SudokuPuzzleRow, SudokuPuzzleColumn, SudokuPuzzleBox
- Solving Algorithm Interfaces: ISolvingAlgorithm (in case we want to implement other solvers)
- Solving Algorithm Classes: BacktrackingSolver.
5. Implement puzzle classes.
6. Create validations to ensure the puzzles are all deserialized properly.
7. Implement the base backtracking solver.
8. Saw some opportunity for improvement and added live constraint checking to the backtracking solver.
