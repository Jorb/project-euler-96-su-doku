using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("Starting Sudoku Solver");

Console.WriteLine("Reading puzzle file");
var puzzleFactory = new SudokuPuzzleFactory("sudoku.txt");

Console.WriteLine("Building puzzle objects");
var puzzleList = puzzleFactory.BuildPuzzleList();

ISudokuPuzzleSolver puzzleSolver = new BackTrackingSudokuPuzzleSolver();

Console.WriteLine("Solving puzzles");
puzzleSolver.SolvePuzzles(puzzleList);

var puzzlePrinter = new SudokuPuzzlePrinter();

Console.WriteLine("Writing solved puzzles to file");
puzzlePrinter.PrintPuzzlesToFile(puzzleList, "sudokuSolved.txt");