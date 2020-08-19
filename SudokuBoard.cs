using System.Collections.Generic;
using System;
using System.Linq;

namespace sudoku_hinter_cs {
	public class SudokuBoard {
		public List<List<int>> Board { get; set; }
		public int Count { get => Board.Count; }
		private List<int> AllPossibleNums { get; }
		private List<KeyValuePair<Cell, List<int>>> AvailableChoices;
		private int SubSqSize { get; set; }
		private static Random Rand = new Random(DateTime.Now.ToString().GetHashCode());

		public SudokuBoard() {
			Board = new List<List<int>>();
			Random Rand = new Random(DateTime.Now.ToString().GetHashCode());
			AllPossibleNums = new List<int>();
            AvailableChoices = new List<KeyValuePair<Cell, List<int>>>();
        }
		public SudokuBoard(List<List<int>> new_board) {
			Board = new_board;
			for (int ii = 1; ii <= Board.Count; ii++) {
				AllPossibleNums.Add(ii);
			}
			SubSqSize = (int)Math.Sqrt(Board.Count);
			Random Rand = new Random(DateTime.Now.ToString().GetHashCode());
            AllPossibleNums = new List<int>();
			AvailableChoices = new List<KeyValuePair<Cell, List<int>>>();
			UpdateChoices();
		}

		public void AddRow(List<int> row) {
			Board.Add(row);
			if (AllPossibleNums.Count == 0) {
				for (int ii = 1; ii <= row.Count; ii++)
				{
					AllPossibleNums.Add(ii);
				}
			}
			if (SubSqSize == 0)
            {
				SubSqSize = (int)Math.Sqrt(row.Count);
            }
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
			int SubSqRowIdx   = (int)Math.Floor((double)cell.row / SubSqSize);
			int SubSqRowStart = SubSqRowIdx * SubSqSize;
			int SubSqRowEnd   = SubSqRowStart + SubSqSize;

			int SubSqColIdx   = (int)Math.Floor((double)cell.col / SubSqSize);
			int SubSqColStart = SubSqColIdx * SubSqSize;
			int SubSqColEnd   = SubSqColStart + SubSqSize;

			List<int> SubSq = new List<int>();
			for (int row = SubSqRowStart; row < SubSqRowEnd; row++) {
				for (int col = SubSqColStart; col < SubSqColEnd; col++) {
					if (Board[row][col] != 0) {
						SubSq.Add(Board[row][col]);
					}
				}
			}
			return SubSq;
		}

		public void Solve(Cell LastModifiedCell) {
			UpdateChoices();

			int iterIdx = 0;
			while (iterIdx < AvailableChoices.Count)
            {
                (Cell cell, List<int> choices) = AvailableChoices[iterIdx];
				iterIdx++;
				if (cell == null)
                {
					Board[LastModifiedCell.row][LastModifiedCell.col] = 0;
					UpdateChoices();
					return;
				}

				if (CheckPuzzle())
                {
					return;
                }

                while (Board[cell.row][cell.col] == 0)
                {
                    // Backtrack if we can go no further down this branch
                    if (choices.Count == 0)
                    {
                        Board[cell.row][cell.col] = 0;
                        Board[LastModifiedCell.row][LastModifiedCell.col] = 0;
                        UpdateChoices();
                        return;
                    }

                    int idx = Rand.Next(0, choices.Count);
                    int temp = choices.ElementAt(idx);
                    choices.RemoveAt(idx);

                    Board[cell.row][cell.col] = temp;
                    Solve(cell);
                }
				if (CheckPuzzle())
				{
					return;
				}
			}
		}

		/// Updates dictionary that keeps track of which values are available for each cell
		public void UpdateChoices() {
			// Unfortunately, since we can't use a dict ordered by length of the lists being used as the values we have
			// to do this to prevent `AvailableChoices` from accumulating its entire history of key-value pairs
			AvailableChoices.Clear();
			for (int ii = 0; ii < Board.Count; ii++) {
				for (int jj = 0; jj < Board.Count; jj++) {
					Cell cell = new Cell(ii, jj);

					var NumsInRow    = GetRow(cell);
					var NumsInCol    = GetCol(cell);
					var NumsInSubSq  = GetNumsInSubsquare(cell);
					var AllTakenNums = NumsInRow.Concat(NumsInCol).Concat(NumsInSubSq).ToList();

					if (AllTakenNums.Count > 0)
					{
						List<int> validChoices = AllPossibleNums.Where(x => !AllTakenNums.Contains(x)).ToList();
						AvailableChoices.Add(new KeyValuePair<Cell, List<int>>(cell, validChoices));
					}
				}
			}
			AvailableChoices.Sort((x, y) => x.Value.Count.CompareTo(y.Value.Count));
			if (AvailableChoices.Count > Board.Count * Board.Count)
            {
				throw new ArgumentException("Should never have more entries than cells on the board!");
            }
		}

        public bool CheckPuzzle()
        {
            foreach (var row in Board)
            {
                if (AllPossibleNums.Any(x => !row.Contains(x)))
                {
                    return false;
                }
            }
            for (int jj = 0; jj < Board.Count; jj++)
            {
                if (AllPossibleNums.Any(x => !GetCol(new Cell(0, jj)).Contains(x)))
                {
                    return false;
                }
            }
            for (int SubSqRowIdx = 0; SubSqRowIdx < Board.Count; SubSqRowIdx += SubSqSize)
            {
                for (int SubSqColIdx = 0; SubSqColIdx < Board.Count; SubSqColIdx += SubSqSize)
                {
                    var SubSq = GetNumsInSubsquare(new Cell(SubSqRowIdx, SubSqColIdx));
                    if (AllPossibleNums.Any(x => !SubSq.Contains(x)))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

		/// <summary>
		/// Just prints board
		/// </summary>
		public void Print()
        {
			foreach (var row in Board)
            {
				foreach (int val in row)
                {
					Console.Write($"{val}");
                }
				Console.WriteLine();
            }
        }
    }
}
