using UnityEngine;

namespace BlockBuilderGame
{
    /// <summary>
    /// Model portion of the reusable door module. Data is stored here
    /// </summary>
    /// <remarks>
    /// - doorId and targetDoorId, destinationSceneId must be set before calling Init(), targetDoorId must exist.
    /// targetSceneId must exist in SceneChangerModel service's path collection
    /// </remarks>
    public interface IBlockController
    {

        /// <summary>
        /// Constructor for the block model
        /// </summary>
        /// <returns>Block object </returns>
        /// <remarks>
        /// Preconditions:
        /// - none
        /// PostConditions:
        /// - A BlockModel instance created
        /// </remarks>
        public void placeBlock(Vector3Int position)
        {
            
        }

        /// <summary>
        /// Constructor for the block model
        /// </summary>
        /// <returns>Block object </returns>
        /// <remarks>
        /// Preconditions:
        /// - none
        /// PostConditions:
        /// - A BlockModel instance created
        /// </remarks>
        public void pickUpBlock(Vector3Int position)
        {
            
        }
        
        // Remove block takes block back to inventory populates inventory
        /// <summary>
        /// Constructor for the block model
        /// </summary>
        /// <returns>Block object </returns>
        /// <remarks>
        /// Preconditions:
        /// - none
        /// PostConditions:
        /// - A BlockModel instance created
        /// </remarks>
        public void removeBlock(Vector3Int position)
        {
            
        }

    }
}