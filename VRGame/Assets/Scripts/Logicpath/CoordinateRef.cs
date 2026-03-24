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
    ///     - X and Y are non-negative
    ///     - X and Y are less than the LogicGameModel's grid size
    /// postconditions:
    ///     - The object is initialized
    /// </remarks>
    public CoordinateRef(int x, int y)
    {
        if(x < 0)
        {
            Debug.LogError("CoordinateRef cannot have a negative X-coordinate");
        }
        Assert.IsTrue(x >= 0, "X coordinate must be greater than 0");
        if(x >= LogicGameModel.MAX_GRID_SIZE)
        {
            Debug.LogError("CoordinateRef cannot have an X-coordinate larger than the LogicGameModel's max grid size");
        }
        Assert.IsTrue(x <= LogicGameModel.MAX_GRID_SIZE - 1, "X coordinate must be less than the LogicGameModel's max grid size");
        if(y < 0)
        {
            Debug.LogError("CoordinateRef cannot have a negative Y-coordinate");
        }
        Assert.IsTrue(y >= 0, "Y coordinate must be greater than 0");
        if(y >= LogicGameModel.MAX_GRID_SIZE)
        {
            Debug.LogError("CoordinateRef cannot have an Y-coordinate larger than the LogicGameModel's max grid size");
        }
        Assert.IsTrue(y <= LogicGameModel.MAX_GRID_SIZE - 1, "Y coordinate must be less than the LogicGameModel's max grid size");
        this.x = x;
        this.y = y;
    }

    /// <summary>
    /// Accessor for the X-coordinate
    /// </summary>
    public int X
    {
        /// <summary>
        /// Getter for the X-coordinate
        /// </summary>
        /// <returns>The X-coordinate reference</returns>
        /// <remarks>
        /// preconditions:
        ///     - None
        /// postconditions:
        ///     - The X-coordinate is returned
        /// </remarks>
        get
        {
            return x;
        }

        /// <summary>
        /// Setter for the X-coordinate
        /// </summary>
        /// <remarks>
        /// preconditions:
        ///     - X is non-negative
        ///     - X is than the LogicGameModel's grid size
        /// postconditions:
        ///     - the X-coordinate is updated to the new value
        /// </remarks>
        set
        {
            if(value < 0)
            {
                Debug.LogError("CoordinateRef cannot have a negative X-coordinate");
            }
            Assert.IsTrue(value >= 0, "X coordinate must be greater than 0");
            if(value >= LogicGameModel.MAX_GRID_SIZE)
            {
                Debug.LogError("CoordinateRef cannot have an X-coordinate larger than the LogicGameModel's max grid size");
            }
            Assert.IsTrue(value <= LogicGameModel.MAX_GRID_SIZE - 1, "X coordinate must be less than the LogicGameModel's max grid size");
            x = value;
        }
    }

    /// <summary>
    /// Accessor for the Y-coordinate
    /// </summary>
    public int Y
    {
        /// <summary>
        /// Getter for the Y-coordinate
        /// </summary>
        /// <returns>The Y-coordinate reference</returns>
        /// <remarks>
        /// preconditions:
        ///     - None
        /// postconditions:
        ///     - The Y-coordinate is returned
        /// </remarks>
        get
        {
            return y;
        }

        /// <summary>
        /// Setter for the Y-coordinate
        /// </summary>
        /// <remarks>
        /// preconditions:
        ///     - Y is non-negative
        ///     - Y is less than the LogicGameModel's grid size
        /// postconditions:
        ///     - the Y-coordinate is updated to the new value
        /// </remarks>
        set
        {
            if(value < 0)
            {
                Debug.LogError("CoordinateRef cannot have a negative Y-coordinate");
            }
            Assert.IsTrue(value >= 0, "Y coordinate must be greater than 0");
            if(value >= LogicGameModel.MAX_GRID_SIZE)
            {
                Debug.LogError("CoordinateRef cannot have an Y-coordinate larger than the LogicGameModel's max grid size");
            }
            Assert.IsTrue(value <= LogicGameModel.MAX_GRID_SIZE - 1, "Y coordinate must be less than the LogicGameModel's max grid size");
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