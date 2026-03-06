using UnityEngine;
namespace BlockBuilderGame
{
    /// <inheritdoc/>
    public class BlockModel: IBlockModel
    {
        private string id
        {
            /// <summary>
            /// Access the DoorModel's Id
            /// </summary>
            /// <remarks>
            /// Preconditions:
            /// - None
            /// Postconditions:
            /// - DoorModel's id is returned
            get;
            /// <summary>
            /// Set the id of this DoorModel
            /// Note: This is for unit testing purposes - the instance variables of MonoBehaviour
            /// scripts are usually set in a GUI window within the Unity editor 
            /// </summary>
            /// <remarks>
            /// Precondintions:
            /// - value must be positive
            /// Postconditions:
            /// - DoorModel's `doorId` instance variable set to input value
            set;
        }
        

        private BlockColour colour;

        private int length
        {
            /// <summary>
            /// Access the DoorModel's Id
            /// </summary>
            /// <remarks>
            /// Preconditions:
            /// - None
            /// Postconditions:
            /// - DoorModel's id is returned
            get;
            /// <summary>
            /// Set the id of this DoorModel
            /// Note: This is for unit testing purposes - the instance variables of MonoBehaviour
            /// scripts are usually set in a GUI window within the Unity editor 
            /// </summary>
            /// <remarks>
            /// Precondintions:
            /// - value must be positive
            /// Postconditions:
            /// - DoorModel's `doorId` instance variable set to input value
            set;
        }
        

        private int width
        {
            /// <summary>
            /// Access the DoorModel's Id
            /// </summary>
            /// <remarks>
            /// Preconditions:
            /// - None
            /// Postconditions:
            /// - DoorModel's id is returned
            get;
            /// <summary>
            /// Set the id of this DoorModel
            /// Note: This is for unit testing purposes - the instance variables of MonoBehaviour
            /// scripts are usually set in a GUI window within the Unity editor 
            /// </summary>
            /// <remarks>
            /// Precondintions:
            /// - value must be positive
            /// Postconditions:
            /// - DoorModel's `doorId` instance variable set to input value
            set;
        }
        

        private BlockShape shape
        {
            /// <summary>
            /// Access the DoorModel's Id
            /// </summary>
            /// <remarks>
            /// Preconditions:
            /// - None
            /// Postconditions:
            /// - DoorModel's id is returned
            get;
            /// <summary>
            /// Set the id of this DoorModel
            /// Note: This is for unit testing purposes - the instance variables of MonoBehaviour
            /// scripts are usually set in a GUI window within the Unity editor 
            /// </summary>
            /// <remarks>
            /// Precondintions:
            /// - value must be positive
            /// Postconditions:
            /// - DoorModel's `doorId` instance variable set to input value
            set;
        }
        

        private Vector3Int gridPosition
        {
            /// <summary>
            /// Access the DoorModel's Id
            /// </summary>
            /// <remarks>
            /// Preconditions:
            /// - None
            /// Postconditions:
            /// - DoorModel's id is returned
            get;
            /// <summary>
            /// Set the id of this DoorModel
            /// Note: This is for unit testing purposes - the instance variables of MonoBehaviour
            /// scripts are usually set in a GUI window within the Unity editor 
            /// </summary>
            /// <remarks>
            /// Precondintions:
            /// - value must be positive
            /// Postconditions:
            /// - DoorModel's `doorId` instance variable set to input value
            set;
        }
        

        private Vector3Int targetPosition 
        {
            /// <summary>
            /// Access the DoorModel's Id
            /// </summary>
            /// <remarks>
            /// Preconditions:
            /// - None
            /// Postconditions:
            /// - DoorModel's id is returned
            get;
            /// <summary>
            /// Set the id of this DoorModel
            /// Note: This is for unit testing purposes - the instance variables of MonoBehaviour
            /// scripts are usually set in a GUI window within the Unity editor 
            /// </summary>
            /// <remarks>
            /// Precondintions:
            /// - value must be positive
            /// Postconditions:
            /// - DoorModel's `doorId` instance variable set to input value
            set;
        }
        
        private bool isPlaced 
        {
            /// <summary>
            /// Access the DoorModel's Id
            /// </summary>
            /// <remarks>
            /// Preconditions:
            /// - None
            /// Postconditions:
            /// - DoorModel's id is returned
            get;
        }

        private bool isGrabbed
        {
            /// <summary>
            /// Access the DoorModel's Id
            /// </summary>
            /// <remarks>
            /// Preconditions:
            /// - None
            /// Postconditions:
            /// - DoorModel's id is returned
            get;
        }
        

        bool isCorrectPostion
        {
            /// <summary>
            /// Access the DoorModel's Id
            /// </summary>
            /// <remarks>
            /// Preconditions:
            /// - None
            /// Postconditions:
            /// - DoorModel's id is returned
            get;
            /// <summary>
            /// Set the id of this DoorModel
            /// Note: This is for unit testing purposes - the instance variables of MonoBehaviour
            /// scripts are usually set in a GUI window within the Unity editor 
            /// </summary>
            /// <remarks>
            /// Precondintions:
            /// - value must be positive
            /// Postconditions:
            /// - DoorModel's `doorId` instance variable set to input value
            set;
        }
        

        

        /// <inheritdoc/>
        public BlockModel(string id, BlockColour c, int l, int w)
        {
            this.id = id;
            colour = c;
            length = l;
            width = w;
            isPlaced = false;
            isGrabbed = false;
        }



        /// <inheritdoc/>    
        public bool IsCorrectlyPlaced(){
            return (gridPosition == targetPosition);
            
        }

    }
}