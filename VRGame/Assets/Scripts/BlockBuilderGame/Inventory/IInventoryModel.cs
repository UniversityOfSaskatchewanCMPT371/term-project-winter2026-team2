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

    public interface IBlockInventoryModel
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
        public void init(){}
    }
}