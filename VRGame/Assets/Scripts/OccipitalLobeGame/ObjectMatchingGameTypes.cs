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

    public struct levelData
    {
        public int levelNumber;
        public int score;
        public int failedGuesses;
        public string CorrectObjectID;
        public string[] IncorrectObjectIDs;
        public levelData(int levelNumber, int score, int failedGuesses, string CorrectObjectID, string[] IncorrectObjectIDs)
        {
            this.levelNumber = levelNumber;
            this.score = score;
            this.failedGuesses = failedGuesses;
            this.CorrectObjectID = CorrectObjectID;
            this.IncorrectObjectIDs = IncorrectObjectIDs;
        }
    }
}
