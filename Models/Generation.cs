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

            if(savedRows <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(savedRows), savedRows, "The number of rows of the saved 2D array is 0 or less");
            }

            if(savedColumns <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(savedColumns), savedColumns, "The number of columns of the saved 2D array is 0 or less");
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

        public void Tick()
        {
            for(var row = 0; row < Rows; row++)
            {
                for(var column = 0; column < Columns; column++)
                {
                    Cell currentCell = Cells[row, column];
                    List<Cell> currentNeighbours = GetCellNeighbours(row, column);
                    int aliveCellNeighbours = currentNeighbours.Count(cell => cell.CurrentState == CellState.Alive);

                    if(currentCell.CurrentState == CellState.Alive && (aliveCellNeighbours == 2 || aliveCellNeighbours == 3))
                    {
                        currentCell.NextState = CellState.Alive;
                    }
                    else if(currentCell.CurrentState == CellState.Dead && aliveCellNeighbours == 3)
                    {
                        currentCell.NextState = CellState.Alive;
                    }
                    else
                    {
                        currentCell.NextState = CellState.Dead;
                    }
                }
            }

            for(var row = 0; row < Rows; row++)
            {
                for(var column = 0; column< Columns; column++)
                {
                    Cells[row, column].Tick();
                }
            }
        }

        private List<Cell> GetCellNeighbours(int row, int column)
        {
            var neighbours = new List<Cell>(8);

            for (var rowOffset = -1; rowOffset <= 1; rowOffset++)
            {
                for (var columnOffset = -1; columnOffset <= 1; columnOffset++)
                {
                    if (rowOffset == 0 && columnOffset == 0) continue;

                    var neighbourRow = row + rowOffset;
                    var neighbourColumn = column + columnOffset;

                    if (neighbourRow < 0 || neighbourRow >= Rows) continue;
                    if (neighbourColumn < 0 || neighbourColumn >= Columns) continue;

                    neighbours.Add(Cells[neighbourRow, neighbourColumn]);
                }
            }

            return neighbours;
        }
    }
}
