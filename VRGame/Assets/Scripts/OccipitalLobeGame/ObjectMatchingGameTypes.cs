namespace ObjectMatchGame
{
    // Enum to represent the different states of the game
    public enum GameState
    {
        playing,
        levelComplete,
        levelFailed,
        readyToStart,
        tutorial,
        complete
    }

    // Struct to hold the data for each level
    [System.Serializable]
    internal struct levelData
    {
        public int levelNumber;
        public string CorrectObjectID;
        public string[] AllObjectIDs;
        public levelData(int levelNumber, string CorrectObjectID, string[] AllObjectIDs)
        {
            this.levelNumber = levelNumber;
            this.CorrectObjectID = CorrectObjectID;
            this.AllObjectIDs = AllObjectIDs;
        }
    }
}
