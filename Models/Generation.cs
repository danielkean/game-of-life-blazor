namespace GameOfLife.Models
{
    public class Generation
    {
        public Cell[,] Cells { get; }

        private int Rows { get; }
        private int Columns { get; }

        public Generation(int rows, int columns)
        {
            if(rows <= 0 || columns <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rows) + " " + nameof(columns), "The rows and columns of the generation cannot be 0 or less");
            }

            this.Rows = rows;
            this.Columns = columns;
            Cells = new Cell[rows, columns];

            for(var row = 0; row < rows; row++)
            {
                for(var col = 0; col < columns; col++)
                {
                   Cells[row, col] = new Cell();
                }
            }
        }

        public Generation(Cell[,] initialCells)
        {
            var savedRows = initialCells.GetLength(0);
            var savedColumns = initialCells.GetLength(1);

            if(savedRows <= 0 || savedColumns <= 0)
            {
                throw new ArgumentOutOfRangeException("One of the dimensions of the saved 2D array is 0 or less");
            }

            Cells = initialCells;
            Rows = savedRows;
            Columns = savedColumns;
        }

        public void ToggleCell(int row, int column)
        {
            if(row <= 0 || row >= Rows)
            {
                throw new ArgumentOutOfRangeException(nameof(row), row, "Row value is invalid");
            }

            if (column <= 0 || column >= Columns)
            {
                throw new ArgumentOutOfRangeException(nameof(column), column, "Column value is invalid");
            }

            Cells[row, column].ToggleState();
        }
    }
}
