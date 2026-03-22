using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// A coordinate reference to a targeted Panel.
/// </summary>
/// <remarks>
/// This class only exists because tuples in C# are non-nullable. This could be worse, I suppose.
/// </remarks>
class CoordinateRef
{
    /// <summary>
    /// The X-coordinate for a panel
    /// </summary>
    private int x;
    /// <summary>
    /// The Y-coordinate for a panel
    /// </summary>
    private int y;

    /// <summary>
    /// Creates a coordinate reference for a Panel
    /// </summary>
    /// <param name="x">The X-coordinate for a panel</param>
    /// <param name="y">The Y-coordinate for a panel</param>
    /// <remarks>
    /// preconditions:
    ///     - X and Y must be non-negative and be less than the LogicGameModel's grid size
    /// postconditions:
    ///     - The object is initialized
    /// </remarks>
    public CoordinateRef(int x, int y)
    {
        Assert.IsTrue(x >= 0, "X coordinate must be greater than 0");
        Assert.IsTrue(x <= LogicGameModel.MAX_GRID_SIZE-1, "X coordinate must be less than the LogicGameModel's max grid size");
        Assert.IsTrue(y >= 0, "Y coordinate must be greater than 0");
        Assert.IsTrue(y <= LogicGameModel.MAX_GRID_SIZE-1, "Y coordinate must be less than the LogicGameModel's max grid size");
        this.x = x;
        this.y = y;
    }

    /// <summary>
    /// Accessor for the X-coordinate
    /// </summary>
    /// <remarks>
    /// preconditions:
    ///     - when writing, x must be non-negative and less than the LogicGameModel's grid size
    /// postconditions:
    ///     - when getting, returns the X-coordinate
    /// </remarks>
    public int X
    {
        get
        {
            return x;
        }
        set
        {
            Assert.IsTrue(value >= 0, "X coordinate must be greater than 0");
            Assert.IsTrue(value <= LogicGameModel.MAX_GRID_SIZE-1, "X coordinate must be less than the LogicGameModel's max grid size");
            x = value;
        }
    }

    /// <summary>
    /// Accessor for the Y-coordinate
    /// </summary>
    /// <remarks>
    /// preconditions:
    ///     - when writing, y must be non-negative and less than the LogicGameModel's grid size
    /// postconditions:
    ///     - when getting, returns the Y-coordinate
    /// </remarks>
    public int Y
    {
        get
        {
            return y;
        }
        set
        {
            Assert.IsTrue(value >= 0, "Y coordinate must be greater than 0");
            Assert.IsTrue(value <= LogicGameModel.MAX_GRID_SIZE-1, "Y coordinate must be less than the LogicGameModel's max grid size");
            y = value;
        }
    }

    /// <summary>
    /// Gets a string representation of the coordinates
    /// </summary>
    /// <returns>A string representation of the coordinates</returns>
    /// <remarks>
    /// preconditions:
    ///     - none
    /// postconditions:
    ///     - A string representation of the coordinates is returned
    public override string ToString()
    {
        return $"({x},{y})";
    }
}