using UnityEngine;
using System.Collections.Generic;
namespace BlockBuilderGame
{
    /// <inheritdoc/>
    public class BlockModel: IBlockModel
    {
        /// <summary>
        /// Users can select block colours and those colours which can be selected are stored here.
        /// Enums chosen to designate block colour.
        /// </summary>
        public enum BlockColour
        {
            red,
            brown,
            grey,
            blue,
            orange,
            green,
            purple,
            pink,
            yellow,
            black,
            white
        }
        
        /// <summary>
        /// Gets or sets the unique identifier for this Block.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - None
        /// Postconditions:
        /// - When accessed, returns the identifier of this BlockModel.
        /// - When set, updates the identifier for this BlockModel.
        /// </remarks>
        private string id
        {
            get;
        set;
        }
        

        private BlockColour colour{
            /// <summary>
            /// Access the Block's Colour
            /// </summary>
            /// <remarks>
            /// Preconditions:
            /// - None
            /// Postconditions:
            /// - None
            get;
            /// <summary>
            /// Set the Block Colour of this BlockMode
            /// </summary>
            /// <remarks>
            /// Precondintions:
            /// - None
            /// Postconditions:
            /// - BlockModel's `colour` instance variable set to input value
            set;
        }
        

        private BlockShape shape
        {
            /// <summary>
            /// Access the BlockShape. This give information about the length width etc
            /// </summary>
            /// <remarks>
            /// Preconditions:
            /// - None
            /// Postconditions:
            /// - None
            get;
            /// <summary>
            /// Set the shape of this BlockModel
            /// </summary>
            /// <remarks>
            /// Precondintions:
            /// - None
            /// Postconditions:
            /// - BlockModel's `shape` instance variable set to input value
            set;
        }
        

        private Vector3Int gridPosition
        {
            /// <summary>
            /// Access the position of BlockModel
            /// </summary>
            /// <remarks>
            /// Preconditions:
            /// - None
            /// Postconditions:
            /// - None
            get;
            /// <summary>
            /// Set the positon of the Block
            /// <remarks>
            /// Precondintions:
            /// - value must be positive
            /// Postconditions:
            /// - BlockModel's `gridPosition` instance variable set to input value
            set;
        }
        

        private List<Vector3Int> targetPositions
        {
          /// <summary>
            /// Access the position of BlockModel
            /// </summary>
            /// <remarks>
            /// Preconditions:
            /// - None
            /// Postconditions:
            /// - None
            get;
            /// <summary>
            /// Set the positon of the Block
            /// <remarks>
            /// Precondintions:
            /// - value must be positive
            /// Postconditions:
            /// - BlockModel's `targetPosition` instance variable set to input value
            set;
        }
        
        private bool isPlaced 
        {
            /// <summary>
            /// Determines if block is placed on grid
            /// </summary>
            /// <remarks>
            /// Preconditions:
            /// - None
            /// Postconditions:
            /// - BlockModel's isPlaced state is returned
            get;
            /// <summary>
            /// Set the placed state of the Block
            /// <remarks>
            /// Precondintions:
            /// - None
            /// Postconditions:
            /// - None
            set;
        }

        private bool isGrabbed
        {
            /// <summary>
            /// Determines if block in player hand
            /// </summary>
            /// <remarks>
            /// Preconditions:
            /// - None
            /// Postconditions:
            /// - BlockModel's grab state is returned
            get;
            /// <summary>
            /// Set the grab state of the Block
            /// <remarks>
            /// Precondintions:
            /// - None
            /// Postconditions:
            /// - None
            set;
        }
        

        /// <summary>
        /// Initializes a new instance of the BlockModel class.
        /// </summary>
        /// <param name="id">The unique identifier for the block.</param>
        /// <param name="c">The colour of the block.</param>
        /// <param name="s">The shape of the block.</param>
        /// <param name="l">The length dimension of the block.</param>
        /// <param name="w">The width dimension of the block.</param>
        /// <remarks>
        /// Preconditions:
        /// - id must not be null or empty.
        /// - l and w should be positive values.
        /// 
        /// Postconditions:
        /// - A new BlockModel instance is created with the provided attributes.
        /// - The block is initialized as not placed and not grabbed.
        /// </remarks>
        public BlockModel(string id, BlockColour c, BlockShape s, int l, int w)
        {
            this.id = id;
            colour = c;
            shape = s;
            isPlaced = false;
            isGrabbed = false;
        }


        /// <inheritdoc/>    
        public bool IsCorrectlyPlaced(){
            return (targetPositions.Contains(gridPosition));
        }
    }
}