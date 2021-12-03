using GameOfLife.Models;

namespace GameOfLife.Services;

public class GameOfLifeService
{
    public Generation Generation    { get; private set; }
    public int Rows                 { get; private set; } = 100;
    public int Columns              { get; private set; } = 100;
    public int GenerationCount      { get; private set; } = 0;
    public bool IsRunning           { get; private set; } = false;
    public int TargetTickRate       { get; } = 120;

    public event Action? OnGenerationTick;

    private readonly int tickDelay = 0;

    public GameOfLifeService()
    {
        Generation = new Generation(Rows, Columns);
        tickDelay = (int)(1000 * (1f / TargetTickRate));
    }

    public async Task Run()
    {
        IsRunning = true;
        while (IsRunning)
        {
            if (Generation.PopulationCount <= 0)
            {
                Stop();
                break;
            }

            Tick();
            await Task.Delay(tickDelay);
        }
    }

    public void Stop()
    {
        IsRunning = false;
    }

    public void Tick()
    {
        Generation.Tick();
        OnGenerationTick?.Invoke();
        GenerationCount++;
    }

    public void Reset()
    {
        Generation.Reset();
        GenerationCount = 0;
    }

    public void ToggleCell(int row, int column)
    {
        Generation.ToggleCell(row, column);
    }
}
