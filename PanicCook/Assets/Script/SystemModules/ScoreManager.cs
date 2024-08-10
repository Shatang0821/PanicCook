using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using FrameWork.Utils;
using UnityEngine;

public class ScoreManager : PersistentUnitySingleton<ScoreManager>
{
    #region SCORE DISPLAY
    public int Score => score;

    private int score;
    private int currentScore;

    Vector3 scoreTextScale = new Vector3(1.2f, 1.2f, 1f);

    public void ResetScore()
    {
        score = 0;
        currentScore = 0;
        ScoreDisplay.UpdateText(score);
    }

    public void AddScore(int scorePoint)
    {
        currentScore += scorePoint;
        StartCoroutine(nameof(AddScoreCoroutine));
    }

    IEnumerator AddScoreCoroutine()
    {
        ScoreDisplay.ScaleText(scoreTextScale);
        while(score<currentScore)
        {
            score += 1;
            ScoreDisplay.UpdateText(score);

            yield return null;
        }
        ScoreDisplay.ScaleText(Vector3.one);
    }
    #endregion

    #region HIGH SCORE SYSTEM

    /// <summary>
    /// スコアデータ
    /// </summary>
    [System.Serializable] 
    public class PlayerScore
    {
        public int score;

        public string playerName;

        public PlayerScore(int score, string playerName)
        {
            this.score = score;
            this.playerName = playerName;
        }
    }

    /// <summary>
    /// ハイスコア並べ替え為のlist
    /// </summary>
    [System.Serializable] public class PlayerScoreData
    {
        public List<PlayerScore> list = new List<PlayerScore>();
    }

    readonly string SaveFileName = "player_score.json";
    string playerName = "NO Name";//デフォルトの名前

    //一番小さいスコアより高いときハイスコアありと判断する
    public bool HasNewHighScore => score > LoadPlayerScoreData().list[9].score;

    public void SetPlayerName(string newName)
    {
        playerName = newName;
    }

    public void SavePlayerScoreData()
    {
        var playerScoreData = LoadPlayerScoreData();

        playerScoreData.list.Add(new PlayerScore(score, playerName));
        playerScoreData.list.Sort((x, y) => y.score.CompareTo(x.score));

        SaveSystem.Save(SaveFileName, playerScoreData);
    }

    public PlayerScoreData LoadPlayerScoreData()
    {
        var playerScoreData = new PlayerScoreData();

        if(SaveSystem.SaveFileExists(SaveFileName))
        {
            playerScoreData = SaveSystem.Load<PlayerScoreData>(SaveFileName);
        }
        else
        {
            while(playerScoreData.list.Count < 10)
            {
                playerScoreData.list.Add(new PlayerScore(0, playerName));
            }

            SaveSystem.Save(SaveFileName, playerScoreData);
        }

        return playerScoreData;
    }

    #endregion
}
