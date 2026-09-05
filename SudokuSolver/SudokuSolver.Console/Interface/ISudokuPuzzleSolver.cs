// <copyright file="ISudokuPuzzleSolver.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

internal interface ISudokuPuzzleSolver
{
    void SolvePuzzles(List<SudokuPuzzle> puzzleList);
    void SolvePuzzle(SudokuPuzzle puzzle);
}