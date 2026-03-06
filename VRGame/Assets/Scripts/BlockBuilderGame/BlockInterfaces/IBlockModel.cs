using UnityEngine;
namespace BlockBuilderGame
{
    /// <summary>
    /// Interface for the Block Model. Defines the data contract all block models must fulfill.
    /// </summary>
    /// <remarks>
    /// - Any class implementing IBlockModel must expose all block state properties.
    /// - GridPosition and TargetPosition must be settable at runtime during placement.
    /// - IsCorrectlyPlaced() must compare GridPosition to TargetPosition.
    /// </remarks>
    public interface IBlockModel
    {
        
        
        /// <summary>
        /// Determines whether the block is currently positioned at one of its valid target locations.
        /// </summary>
        /// <returns>
        /// True if the block's current grid position matches one of the target positions;
        /// otherwise false.
        /// </returns>
        /// <remarks>
        /// Preconditions:
        /// - targetPositions must not be null.
        /// 
        /// Postconditions:
        /// - Returns true if gridPosition exists in targetPositions.
        /// - Returns false otherwise.
        /// </remarks>
        public bool IsCorrectlyPlaced();
    }
}