using Microsoft.AspNetCore.Components;

namespace GameOfLife.Components.Icons;

public class SvgImage: ComponentBase
{
    [Parameter] public int Size { get; set; } = 24;
    [Parameter] public float StrokeWidth { get; set; } = 1.5f;
}
