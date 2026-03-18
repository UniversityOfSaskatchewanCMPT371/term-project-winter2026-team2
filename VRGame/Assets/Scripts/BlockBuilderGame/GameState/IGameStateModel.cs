// TODO look at /VRGame/Assets/ScriptTemplate/Example.cs to see how to use this
// If you are making Model layer, inherit from IModel.
// Same goes for other layers. (IController/IView)

/// <summary>
/// TODO: Change the docstring to match your implementation.
/// </summary>
public interface IGameStateModel : IModel
{
    /// <summary>
    /// TODO: Change the docstring to match your implementation.
    /// </summary>
    new void Init();

    
    /// <summary>
    /// Current level in play
    /// </summary>
    int CurrentPuzzle
    { 
        /// <summary>
        /// Getter for Currentpuzzle
        /// </summary>
        /// <remarks>        
        /// pre-condition:
        ///     - none
        /// post-condition:
        ///     - Returns the current puzzle in game
        /// </remarks>
        get; 
        
        /// <summary>
        /// Setter for Current puzzle
        /// </summary>
        /// <remarks>
        /// pre-condition:
        ///     - Input value must be an integer >= 0
        /// post-condition:
        ///     - Sets the current puzzle to the provided value
        /// </remarks>
        set; }
        

    /// <summary>
    /// TotalPuzzles in game the player is able to get to.
    /// </summary>
    int TotalPuzzles
    { 
        /// <summary>
        /// Getter for TotalPuzzles in game
        /// </summary>
        /// <remarks>        
        /// pre-condition:
        ///     - none
        /// post-condition:
        ///     - Returns the totalPuzzles available in game
        /// </remarks>
        get; 
        
        /// <summary>
        /// Setter for TotalPuzzles in game
        /// </summary>
        /// <remarks>
        /// pre-condition:
        ///     - Input value must be an integer >= 0
        /// post-condition:
        ///     - Sets the totalPuzzles to the provided value
        /// </remarks>
        set; }


    /// <summary>
    /// Sets the brick prefabs available in play
    /// </summary>
    /// <remarks>
    /// pre-condition:
    ///     - Input array must not be null
    ///     - Input array must have exactly 4 elements
    /// post-condition:
    ///     - Sets the BrickPrefabs array to the provided value
    /// </remarks>
    void SetBrickPrefabsAvailable(int level, BlockShape[] shapesToInclude); 

}
