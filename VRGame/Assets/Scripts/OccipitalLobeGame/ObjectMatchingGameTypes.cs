namespace ObjectMatchGame
{
    public enum GameState
    {
        playing,
        levelComplete,
        levelFailed,
        readyToStart,
        tutorial,
        complete
    }

    [System.Serializable]
    internal struct levelData
    {
        public int levelNumber;
        public string CorrectObjectID;
        public string[] AllObjectIDs;
        public int Score;
        public int failedGuesses;
        public levelData(int levelNumber, string CorrectObjectID, string[] AllObjectIDs)
        {
            this.levelNumber = levelNumber;
            this.CorrectObjectID = CorrectObjectID;
            this.AllObjectIDs = AllObjectIDs;
            this.Score = 0;
            this.failedGuesses = 0;
        }
    }
}
