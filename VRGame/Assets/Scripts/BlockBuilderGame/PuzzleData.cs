using UnityEngine;
using System.Collections.Generic;

namespace BlockBuilderGame
{
    public class PuzzleData : IPuzzleData
    {
        public string PuzzleId;
        public Sprite ReferenceImage;
        public int GridWidth;
        public int GridHeight;


        public List<TargetBlock> targetBlocks = new();
        public List<BlockModel> blocksInPlay = new();   // this would include all the blocks available for the player to play

        
            
        public class TargetBlock
        {
            public Vector3Int gridPosition;
            public string blockType;
            public BlockColour color;
        }

    }
}
