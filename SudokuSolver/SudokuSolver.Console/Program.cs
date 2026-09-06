// <copyright file="Program.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

Console.WriteLine("Starting Sudoku Solver");

Console.WriteLine("Reading puzzle file");
var puzzleFactory = new SudokuPuzzleFactory();

Console.WriteLine("Building puzzle objects");
var puzzleList = puzzleFactory.BuildAllPuzzlesFromFile("sudoku.txt");

ISudokuPuzzleSolver puzzleSolver = new BackTrackingSudokuPuzzleSolver();

Console.WriteLine("Show live solving? (y/n)");
var showLive = Console.ReadKey().Key.Equals(ConsoleKey.Y);

Console.WriteLine("Solving puzzles");
puzzleSolver.SolvePuzzles(puzzleList, showLive);

var puzzlePrinter = new SudokuPuzzlePrinter();

var solvedFilePath = $"sudokuSolved_{Guid.NewGuid()}.txt";
Console.WriteLine($"Writing solved puzzles to file: {solvedFilePath}");
puzzlePrinter.PrintPuzzlesToFile(puzzleList, solvedFilePath);