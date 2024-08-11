using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using FrameWork.Utils;
using UnityEngine;

//今が持っている星の数
public enum StarNowHave
{
    Zero,    // 星が0個の時
    One,     // 星が1個の時
    Two,     // 星が2個の時
    Three,   // 星が3個の時
    Four,    // 星が4個の時
    Five     // 星が5個の時
}
public class ScoreManager : PersistentUnitySingleton<ScoreManager>
{
    #region SCORE DISPLAY
    public int Score => score;
    public int Star => star;

    private int score;
    
    [SerializeField]
    private string _restaurantName = "NO NAME";
    
    [SerializeField]
    private int star = 0;
    
    [SerializeField]
    private int currentScore;

    Vector3 scoreTextScale = new Vector3(1.2f, 1.2f, 1f);
    
    // StarNowHaveに対応するポイント数をマッピングするDictionary
    private Dictionary<StarNowHave, int> _starWithScore = new Dictionary<StarNowHave, int>()
    {
        { StarNowHave.Zero, 0 },    // 初期状態では0ポイント
        { StarNowHave.One, 100 },   // 1つ目のスター獲得に必要なポイント数
        { StarNowHave.Two, 500 },   // 2つ目のスター獲得に必要なポイント数
        { StarNowHave.Three, 1000 }, // 3つ目のスター獲得に必要なポイント数
        { StarNowHave.Four,  1500},  // 4つ目のスター獲得に必要なポイント数
        { StarNowHave.Five, 3000 }   // 5つ目のスター獲得に必要なポイント数
    };

    public void ResetScore()
    {
        star = 0;
        score = 0;
        currentScore = 0;
        ScoreDisplay.UpdateText(score);
    }

    public void AddScore(int scorePoint)
    {
        if (scorePoint <= 0)
        {
            Debug.LogWarning("scorePoint は正の値である必要があります。減少させるには DecreaseScore メソッドを使用してください。");
            return;
        }

        currentScore += scorePoint;
        CheckAndUpdateStar(currentScore); // 星の数をチェックして増加・減少を反映する
        StartCoroutine(UpdateScoreCoroutine(1));
    }

    public void DecreaseScore(int scorePoint)
    {
        if(star <= 3)
            return;
        if (scorePoint <= 0)
        {
            Debug.LogWarning("scorePoint は正の値である必要があります。増加させるには AddScore メソッドを使用してください。");
            return;
        }

        currentScore -= scorePoint;
        CheckAndUpdateStar(currentScore); // 星の数をチェックして増加・減少を反映する
        StartCoroutine(UpdateScoreCoroutine(-1));
    }

    /// <summary>
    /// 現在のスコアに基づいてスターを増減させる
    /// </summary>
    public void CheckAndUpdateStar(int currentScore)
    {
        int newStar = 0;

        foreach (var starThreshold in _starWithScore)
        {
            if (currentScore >= starThreshold.Value)
            {
                newStar = (int)starThreshold.Key;
            }
        }

        if (newStar != star)
        {
            star = newStar;
            Debug.Log($"スターの数が更新されました！現在のスター数: {star}");
        }
    }

    IEnumerator UpdateScoreCoroutine(int deltaScore)
    {
        ScoreDisplay.ScaleText(scoreTextScale);

        if (deltaScore > 0)  // ポイントの増加
        {
            while (score < currentScore)
            {
                score += deltaScore;
                ScoreDisplay.UpdateText(score);
                yield return null;
            }
        }
        else if (deltaScore < 0)  // ポイントの減少
        {
            while (score > currentScore)
            {
                score += deltaScore;
                ScoreDisplay.UpdateText(score);
                yield return null;
            }
        }

        ScoreDisplay.ScaleText(Vector3.one);
    }
    
    /// <summary>
    /// レストランの名前の設定
    /// </summary>
    /// <param name="name"></param>
    public void SetRestaurantName(string name)
    {
        Debug.Log("レストラン名を設定しました！");
        _restaurantName = name;
    }
    
    public string GetRestaurantName()
    {
        return _restaurantName;
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
