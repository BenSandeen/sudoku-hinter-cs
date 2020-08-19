using System;

namespace sudoku_hinter_cs {
	public class Cell {
		public int row { get; set; }
		public int col { get; set; }

		public Cell(int row, int col) {
			this.row = row;
			this.col = col;
		}

		/// <summary>
		/// Used for hashing to be used as the keys in the `AvailableChoices` field of the `SudokuBoard` class
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return Tuple.Create(row, col).GetHashCode();
		}
	}
}
