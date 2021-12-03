namespace GameOfLife.Models
{
    public class Generation
    {
        public Cell[,] Cells { get; }
        public int PopulationCount { get; private set; }

        private int Rows { get; }
        private int Columns { get; }

        public Generation(int rows, int columns)
        {
            if (rows <= 0 || columns <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rows) + " " + nameof(columns), "The rows and columns of the generation cannot be 0 or less");
            }

            this.Rows = rows;
            this.Columns = columns;
            Cells = new Cell[rows, columns];

            for (var row = 0; row < rows; row++)
            {
                for (var col = 0; col < columns; col++)
                {
                    Cells[row, col] = new Cell();
                }
            }
        }

        public Generation(Cell[,] initialCells)
        {
            var savedRowCount = initialCells.GetLength(0);
            var savedColumnCount = initialCells.GetLength(1);

            if (savedRowCount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(savedRowCount), savedRowCount, "The number of rows of the saved 2D array is 0 or less");
            }

            if (savedColumnCount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(savedColumnCount), savedColumnCount, "The number of columns of the saved 2D array is 0 or less");
            }

            Cells = initialCells;
            Rows = savedRowCount;
            Columns = savedColumnCount;
        }

        public void ToggleCell(int row, int column)
        {
            if (row < 0 || row >= Rows)
            {
                throw new ArgumentOutOfRangeException(nameof(row), row, "Row value is invalid");
            }

            if (column < 0 || column >= Columns)
            {
                throw new ArgumentOutOfRangeException(nameof(column), column, "Column value is invalid");
            }

            Cell cell = Cells[row, column];
            cell.ToggleState();

            if (cell.CurrentState == CellState.Alive) PopulationCount++;
            else if (cell.CurrentState == CellState.Dead) PopulationCount--;
        }

        public void Tick()
        {
            PopulationCount = 0;

            for (var row = 0; row < Rows; row++)
            {
                for (var column = 0; column < Columns; column++)
                {
                    Cell currentCell = Cells[row, column];
                    List<Cell> currentNeighbours = GetCellNeighbours(row, column);

                    int aliveCellNeighbours = 0;
                    foreach (var cell in currentNeighbours)
                    {
                        if (cell.CurrentState == CellState.Alive) aliveCellNeighbours++;
                    }

                    if (currentCell.CurrentState == CellState.Alive && (aliveCellNeighbours == 2 || aliveCellNeighbours == 3))
                    {
                        currentCell.NextState = CellState.Alive;
                        PopulationCount++;
                    }
                    else if (currentCell.CurrentState == CellState.Dead && aliveCellNeighbours == 3)
                    {
                        currentCell.NextState = CellState.Alive;
                        PopulationCount++;
                    }
                    else
                    {
                        currentCell.NextState = CellState.Dead;
                    }
                }
            }

            for (var row = 0; row < Rows; row++)
            {
                for (var column = 0; column < Columns; column++)
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

        public void Reset()
        {
            if(PopulationCount > 0)
            {
                for (var row = 0; row < Rows; row++)
                {
                    for (var column = 0; column < Columns; column++)
                    {
                        Cell cell = Cells[row, column];
                        if (cell.CurrentState == CellState.Alive) cell.ToggleState();
                    }
                }
            }

            PopulationCount = 0;
        }
    }
}
