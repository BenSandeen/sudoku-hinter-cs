using System.Collections.Generic;
using System;

namespace sudoku_hinter_cs {
    public class SudokuBoard {
		// public int[][] board;
		// private List<List<int>> _board;
		// public List<List<int>> board {
		// 	get => _board;
		//    	set => _board = value;
	   	// }
		// public SudokuBoard board { get; set; }
		public List<List<int>> Board { get; set; }

		public SudokuBoard() {
			Board = new List<List<int>>();
		}
		public SudokuBoard(List<List<int>> new_board) {
			Board = new_board;
		}

		public void AddRow(List<int> row) {
			Board.Add(row);
		}

		public int this[int idx1, int idx2] {
			get {
				return Board[idx1][idx2];
			}
			set {
				Board[idx1][idx2] = value;
			}
		}

		public int Count { get => Board.Count; }
    }
}
