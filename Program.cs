using System;

namespace sudoku_hinter_cs
{
    class Program
    {
        static void Main(string[] args)
        {
			CsvReader csvReader = new CsvReader();
			SudokuBoard board = csvReader.ReadSudokuBoard(@"A:\Documents\Projects\c#\sudoku-hinter-cs\sample_puzzle.csv");
			Console.WriteLine($"Board stuff: {board[0, 4]}");
			board.Solve(new Cell(0, 0));

			board.Print();
        }

		//public void solve(SudokuBoard board, Cell LastModifiedCell) {
		//	board.update_choices();
		//}

		// public void update_choices(SudokuBoard board) {
		// 	for (int ii = 0; ii < board.Count; ii++) {
		// 		for (int jj = 0; jj < board.Count; jj++) {
        //
		// 		}
		// 	}
        //
		// }

		// public List<int> get_nums_in_row(SudokuBoard board, Cell cell) {
		// 	// return board.Where()
		// }
    }
}
