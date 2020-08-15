using System.Collections.Generic;
using System.IO;
using System;

namespace sudoku_hinter_cs {
	public class CsvReader {
	// public List<List<int>> ReadSudokuBoard(string FileName) {
		public int[,] ReadSudokuBoard(string FileName) {
			int[,] board;
			using(StreamReader reader = new StreamReader(FileName)) {
				int[] row;
				int currIdx = 0;

				while (!reader.EndOfStream) {
					string line = reader.ReadLine();
					List<string> values = new List<string>() {line.Split(",")};
					// row.AddRange(values.Foreach(x => Int32.Parse(x)));
					values.ForEach(x => Int32.Parse(x));
					row = new { values };
					board[currIdx] = row;
					currIdx++;
				}
			}
			Console.WriteLine($"board: {board}");
			return board;
		}
	}
}
