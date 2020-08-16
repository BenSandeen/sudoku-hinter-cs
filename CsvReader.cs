using System.Collections.Generic;
using System.IO;
using System;

namespace sudoku_hinter_cs {
	public class CsvReader {
	// public List<List<int>> ReadSudokuBoard(string FileName) {
		public SudokuBoard ReadSudokuBoard(string FileName) {
			var lineCount = File.ReadAllLines(FileName).Length;
			// int[,] board;
			// List<List<int>> board = new List<List<int>>();
			SudokuBoard board = new SudokuBoard();

			using(StreamReader reader = new StreamReader(FileName)) {
				// int[] row;
				List<int> row = new List<int>();
				int currIdx = 0;

				while (!reader.EndOfStream) {
					row = new List<int>();
					string line = reader.ReadLine();
					List<string> values = new List<string>(line.Split(","));
					values.ForEach(x => row.Add(Int32.Parse(x)));
					board.AddRow(row);
				}
			}
			for (int row = 0; row < board.Count; row++) {
				for (int col = 0; col < board.Count; col++) {
					Console.Write($"{board[row, col]}");
				}
				Console.WriteLine();
			}
			return board;
		}
	}
}
