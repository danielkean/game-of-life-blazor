using System.Text.Json.Serialization;

namespace GameOfLife.Models
{
    public class Cell
    {
        public CellState CurrentState { get; private set; } = CellState.Dead;
        public CellState NextState { get; private set; } = CellState.Dead;

        public Cell(CellState currentState = CellState.Dead)
        {
            CurrentState = currentState;
            NextState = CellState.Dead;
        }

        [JsonConstructorAttribute]
        public Cell(CellState currentState, CellState nextState)
        {
            CurrentState = currentState;
            NextState = nextState;
        }

        public void Tick()
        {
            CurrentState = NextState;
            NextState = CellState.Dead;
        }

        public void ToggleState() => CurrentState = CurrentState switch
        {
            CellState.Alive => CellState.Dead,
            CellState.Dead => CellState.Alive,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
