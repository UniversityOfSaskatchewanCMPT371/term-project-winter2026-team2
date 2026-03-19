using NUnit.Framework;

/// <summary>
/// A coordinate reference to a targeted Panel.
/// </summary>
/// <remarks>
/// This class only exists because tuples in C# are non-nullable. This could be worse, I suppose.
/// </remarks>
class CoordinateRef
{
    private int x;
    private int y;

    public CoordinateRef(int x, int y)
    {
        Assert.IsTrue(x >= 0, "X coordinate must be greater than 0");
        Assert.IsTrue(x <= LogicGameModel.MAX_GRID_SIZE-1, "X coordinate must be less than the LogicGameModel's max grid size");
        Assert.IsTrue(y >= 0, "Y coordinate must be greater than 0");
        Assert.IsTrue(y <= LogicGameModel.MAX_GRID_SIZE-1, "Y coordinate must be less than the LogicGameModel's max grid size");
        this.x = x;
        this.y = y;
    }

    public int X
    {
        get
        {
            return x;
        }
        set
        {
            x = value;
        }
    }

    public int Y
    {
        get
        {
            return y;
        }
        set
        {
            y = value;
        }
    }
}