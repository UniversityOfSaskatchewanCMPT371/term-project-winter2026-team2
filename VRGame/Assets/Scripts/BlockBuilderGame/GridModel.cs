using UnityEngine;
using System.Collections.Generic;
namespace BlockBuilderGame
{
    public class GridModel
    {
        public int width;
        public int height;
        public float CellSize;
        public Dictionary<Vector3Int, BlockModel> placedBlocks;
    }
}
