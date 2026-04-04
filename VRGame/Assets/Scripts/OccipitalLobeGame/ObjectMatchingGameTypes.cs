using System.Linq;
using UnityEngine;

namespace ObjectMatchGame
{
    /// <summary>
    /// Used to determine the current state of the game. Will determine which actions
    /// are valid to do based on which of these states the model is in
    /// </summary>
    public enum GameState
    {
        playing,
        levelComplete,
        levelFailed,
        readyToStart,
        tutorial,
        complete
    }

    /// <summary>
    /// Data for each level of the game. Serializable so the items can easily be set up
    /// in Unity Editor
    /// </summary>
    [System.Serializable]
    internal struct levelData
    {
        public int levelNumber;
        public string CorrectObjectID;
        public string[] AllObjectIDs;

        /// <summary>
        /// Creates a new instance of levelData
        /// </summary>
        /// <remarks>
        /// Precondtitions:
        /// - levelNumber is a positive integer of at least 1
        /// - CorrectObjectID is a string that exists in AllObjectIDs
        /// - AllObjectIDs is non-null and non-empty
        /// Postconditions:
        /// - this.levelNumber = levelNumber
        /// - this.CorrectObjectID = CorrectObjectID;
        /// - this.AllObjectIDs = AllObjectIDs;
        /// </remarks>
        public levelData(int levelNumber, string CorrectObjectID, string[] AllObjectIDs)
        {
            if (levelNumber <= 0)
            {
                Debug.LogError("Attempt to create level with negative level number");
            }
            if (CorrectObjectID == null || !AllObjectIDs.Contains(CorrectObjectID))
            {
                Debug.LogError("Attempt to create level where the CorrectObjectID is" +
                    "not the ID of one of the Objects given");
            }
            if (AllObjectIDs == null || AllObjectIDs.Length == 0)
            {
                Debug.LogError("Attempt to create level with null or empty AllObjectIDs");
            }
            this.levelNumber = levelNumber;
            this.CorrectObjectID = CorrectObjectID;
            this.AllObjectIDs = AllObjectIDs;
        }
    }
}
