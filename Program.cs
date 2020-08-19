using System;

namespace sudoku_hinter_cs
{
    class Program
    {
        static void Main(string[] args)
        {
			CsvReader csvReader = new CsvReader();
			SudokuBoard board = csvReader.ReadSudokuBoard(@"A:\Documents\Projects\c#\sudoku-hinter-cs\sample_puzzle.csv");
			board.Solve(new Cell(0, 0));
			board.Print();

            board = csvReader.ReadSudokuBoard(@"A:\Documents\Projects\c#\sudoku-hinter-cs\9x9_tough.csv");
            board.Solve(new Cell(0, 0));
            board.Print();

            board = csvReader.ReadSudokuBoard(@"A:\Documents\Projects\c#\sudoku-hinter-cs\16x16_sample_puzzle.csv");
            board.Solve(new Cell(0, 0));
            board.Print();

            board = csvReader.ReadSudokuBoard(@"A:\Documents\Projects\c#\sudoku-hinter-cs\16x16_another_puzzle.csv");
            board.Solve(new Cell(0, 0));
            board.Print();
        }
    }
}
