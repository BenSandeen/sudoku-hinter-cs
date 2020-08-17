using System.Collections.Generic;
using System;
using System.Linq;

namespace sudoku_hinter_cs {
    public class SudokuBoard {
		public List<List<int>> Board { get; set; }
		public int Count { get => Board.Count; }
		public List<int> AllPossibleNums { get; }
		public Dictionary<Cell, List<int>> AvailableChoices{ get; set; }
		public int SubSqSize { get; set; }

		public SudokuBoard() {
			Board = new List<List<int>>();
		}
		public SudokuBoard(List<List<int>> new_board) {
			Board = new_board;
			for (int ii = 1; ii <= Board.Count; ii++) {
				AllPossibleNums.Add(ii);
			}
			SubSqSize = (int)Math.Sqrt(Board.Count);
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

		/// Gets given row
		public List<int> this[int idx1] {
			get {
				return Board[idx1];
			}
			set {
				Board[idx1] = value;
			}
		}

		/// Uses `Cell` object to get stuff
		public int this[Cell cell] {
			get {
				return Board[cell.row][cell.col];
			}
			set {
				Board[cell.row][cell.col] = value;
			}
		}

		/// Gets non-zero values in the row indicated
		public List<int> GetRow(Cell cell) {
			return Board[cell.row].Where(x => x != 0).ToList();
		}

		/// Gets non-zero values in the column indicated
		public List<int> GetCol(Cell cell) {
			// return Board.ForEach(row => row[cell.col]).ToList();
			List<int> ValsInCol = new List<int>();
			foreach (var row in Board) {
				ValsInCol.Add(row[cell.col]);
			}
			return ValsInCol;
		}

		public List<int> GetNumsInSubsquare(Cell cell) {
			int SubSqRowIdx   = (int)Math.Floor((double)cell.row);
			int SubSqRowStart = SubSqRowIdx * SubSqSize;
			int SubSqRowEnd   = SubSqRowStart + SubSqSize;

			int SubSqColIdx   = (int)Math.Floor((double)cell.col);
			int SubSqColStart = SubSqRowIdx * SubSqSize;
			int SubSqColEnd   = SubSqRowStart + SubSqSize;

			List<int> SubSq = new List<int>();
			for (int row = SubSqRowStart; row < SubSqRowEnd; row++) {
				for (int col = SubSqRowStart; col < SubSqRowEnd; col++) {
					if (Board[row][col] != 0) {
						SubSq.Add(Board[row][col]);
					}
				}
			}
			return SubSq;
		}

		public Solve(Cell LastModifiedCell) {
			UpdateChoices();

			foreach (Cell cell in AvailableChoices.)
		}

		/// Updates dictionary that keeps track of which values are available for each cell
		public void UpdateChoices() {
			for (int ii = 0; ii < Board.Count; ii++) {
				for (int jj = 0; jj < Board.Count; jj++) {
					Cell cell = new Cell(ii, jj);

					var NumsInRow    = GetRow(cell);
					var NumsInCol    = GetCol(cell);
					var NumsInSubSq  = GetNumsInSubsquare(cell);
					var AllTakenNums = NumsInRow.Concat(NumsInCol).Concat(NumsInSubSq).ToList();

					List<int> ValidChoices = AllPossibleNums.Where(x => !AllTakenNums.Contains(x)).ToList();
					AvailableChoices[cell] = ValidChoices;
				}
			}
		}

		public bool CheckPuzzle() {
			foreach (var row in Board) {
				if (AllPossibleNums.Any(x => !row.Contains(x))) {
					return false;
				}
			}
			for (int jj = 0; jj < Board.Count; jj++) {
				if (AllPossibleNums.Any(x => !GetCol(new Cell(0, jj)).Contains(x))) {
					return false;
				}
			}
			for (int SubSqRowIdx = 0; SubSqRowIdx < Board.Count; SubSqRowIdx += SubSqSize) {
				for (int SubSqColIdx = 0; SubSqColIdx < Board.Count; SubSqColIdx += SubSqSize) {
					var SubSq = GetNumsInSubsquare(new Cell(SubSqRowIdx, SubSqColIdx));
					if (AllPossibleNums.Any(x => !SubSq.Contains(x))) {
						return false;
					}
				}
			}
			return true;
		}
    }
}
