// <copyright file="Program.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

using SudokuSolver.Common.Helper;
using System.Diagnostics;

Console.WriteLine("Starting Sudoku Solver");

Console.WriteLine("Reading puzzle file");
var puzzleFactory = new SudokuPuzzleFactory();

Console.WriteLine("Building puzzle objects");
var puzzleList = puzzleFactory.BuildAllPuzzlesFromFile("sudoku.txt");

ISudokuPuzzleSolver puzzleSolver = SelectSolver();

Console.WriteLine("Show live solving? (y/n)");
var showLive = Console.ReadKey().Key.Equals(ConsoleKey.Y);

var stopwatch = Stopwatch.StartNew();

Console.WriteLine("Solving puzzles");
puzzleSolver.SolvePuzzles(puzzleList, showLive, PrintPuzzle);

stopwatch.Stop();

Console.WriteLine($"Puzzles solved in {stopwatch.Elapsed.TotalMilliseconds} milliseconds.");

var puzzlePrinter = new SudokuPuzzlePrinter();

var solvedFilePath = $"sudokuSolved_{puzzleSolver.GetType().Name}_{Guid.NewGuid()}.txt";
Console.WriteLine($"Writing solved puzzles to file: {solvedFilePath}");
puzzlePrinter.PrintPuzzlesToFile(puzzleList, solvedFilePath);

void PrintPuzzle(SudokuPuzzle puzzle)
{
    Console.Clear();
    var puzzleLines = PuzzleToStringListHelper.BuildFileLinesFromPuzzleRows(puzzle);
    foreach (var puzzleLine in puzzleLines)
    {
        Console.WriteLine(puzzleLine);
    }
}

static ISudokuPuzzleSolver SelectSolver()
{
    ISudokuPuzzleSolver puzzleSolver;
    Console.WriteLine("Select solver algorithm");
    Console.WriteLine("1.Backtracking");
    Console.WriteLine("2.Constraint W/Backtracking");
    var solverKey = Console.ReadKey();
    if (solverKey.Key == ConsoleKey.D1)
    {
        puzzleSolver = new BackTrackingSudokuPuzzleSolver();
    }
    else if (solverKey.Key == ConsoleKey.D2)
    {
        puzzleSolver = new ConstraintSolverWithBacktracking();
    }
    else
    {
        throw new InvalidSudokuSolverSelected(solverKey);
    }

    return puzzleSolver;
}