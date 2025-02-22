using System;
using System.Collections.Generic;

[System.Serializable]
public class GameSession
{
    public int score;
    public int level;
    public float playTime;
    public string dateTime; // Thời gian chơi

    public GameSession(int score, int level, float playTime)
    {
        this.score = score;
        this.level = level;
        this.playTime = playTime;
        this.dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
}

[System.Serializable]
public class GameData
{
    public List<GameSession> gameSessions = new List<GameSession>();
}
