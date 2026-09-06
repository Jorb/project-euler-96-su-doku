// <copyright file="Program.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

using SudokuSolver.Common.Helper;

Console.WriteLine("Starting Sudoku Solver");

Console.WriteLine("Reading puzzle file");
var puzzleFactory = new SudokuPuzzleFactory();

Console.WriteLine("Building puzzle objects");
var puzzleList = puzzleFactory.BuildAllPuzzlesFromFile("sudoku.txt");

ISudokuPuzzleSolver puzzleSolver = new BackTrackingSudokuPuzzleSolver();

Console.WriteLine("Show live solving? (y/n)");
var showLive = Console.ReadKey().Key.Equals(ConsoleKey.Y);

Console.WriteLine("Solving puzzles");
puzzleSolver.SolvePuzzles(puzzleList, showLive, PrintPuzzle);

void PrintPuzzle(SudokuPuzzle puzzle)
{
    Console.Clear();
    var puzzleLines = PuzzleToStringListHelper.BuildFileLinesFromPuzzleRows(puzzle);
    foreach (var puzzleLine in puzzleLines) { Console.WriteLine(puzzleLine); }
}

var puzzlePrinter = new SudokuPuzzlePrinter();

var solvedFilePath = $"sudokuSolved_{Guid.NewGuid()}.txt";
Console.WriteLine($"Writing solved puzzles to file: {solvedFilePath}");
puzzlePrinter.PrintPuzzlesToFile(puzzleList, solvedFilePath);