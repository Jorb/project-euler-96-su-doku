using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("Starting Sudoku Solver");

ISudokuPuzzleFactory puzzleFactory = new SudokuPuzzleFactory("sudoku.txt");
List<SudokuPuzzle> puzzleList = puzzleFactory.BuildPuzzleList();

ISudokuPuzzleSolver puzzleSolver = new BackTrackingSudokuPuzzleSolver();

puzzleSolver.SolvePuzzles(puzzleList);

