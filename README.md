# project-euler-96-su-doku
My attempt at solving the project Euler Su Doku problem: https://projecteuler.net/problem=96

# My Method
1. Looked up basic SuDoku algorithms: https://en.wikipedia.org/wiki/Sudoku_solving_algorithms
- Decided on brute force search for simplicity
2. Chose C# .NET Console app to implement.
3. Decided on success metric.
- Validate each puzzle
- Print solved puzzles to the console
4. First pass basic types
- Puzzle Classes: SudokuPuzzle, SudokuPuzzleRow, SudokuPuzzleColumn, SudokuPuzzleBox
- Solving Algorithm Interfaces: ISolvingAlgorithm (in case we want to implement other solvers)
- Solving Algorithm Classes: BruteForceSolvingAlgorithm, BruteForceRowSolver, BruteForceColumnSolver, BruteForceBoxSolver.
- Validator Classes: SudokuPuzzleValidator, SudokuRowValidator, SudokuColumnValidator, SudokuBoxValidator.
5. Started implementing the file parser and deserializing the txt file into in-memory objects (SudokuPuzzle class)
