namespace sudoku_hinter_cs {
	public class Cell {
		public int row { get; set; }
		public int col { get; set; }

		public Cell(int row, int col) {
			this.row = row;
			this.col = col;
		}
	}
}
