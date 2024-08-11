using System;
using System.Collections;
using DG.Tweening;
using FrameWork.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameAction
{
    Pause,
    UnPause,
}

public enum GameState
{
    Default,        // デフォルト
    InitState,      // 料理のリセット、主人公のリセット、客のリセット
    WaitGuest,      // 客が出てきて、オーダーを表示
    PlayerTurn,     // プレイヤの移動、料理の提供
    ScoreState,     // スコアの計算
    End             // ゲーム終了
}

public class GameManager : UnitySingleton<GameManager>
{
    [SerializeField]
    private AudioData _bgm;
    [SerializeField]
    private AudioData CorrectSE;
    [SerializeField]
    private AudioData IncorrectSE;

    [SerializeField]
    private GameObject _counter;
    //プレイヤインスタンス
    [SerializeField]
    private Player _player;
    [SerializeField]
    //ゲームの状態
    private GameState _currentGameState;

    //連続正解数
    private int _correctStreakCount = 0;
    
    public GameState CurrentGameState
    {
        get => _currentGameState;
        set
        {
            if (_currentGameState != value)
            {
                _currentGameState = value;
                //状態変更したときに呼び出す
                OnEnterState(_currentGameState);
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();
        AudioManager.Instance.ChangeBGM(_bgm);
        _counter.SetActive(false);
        CurrentGameState = GameState.Default;
        
        //Debug.Log(Application.persistentDataPath);
    }

    public void StartGame()
    {
        _counter.SetActive(true);
        CurrentGameState = GameState.InitState;
    }

    private void Update()
    {
        switch (CurrentGameState)
        {
            case GameState.InitState:
                InitState();
                break;
            case GameState.WaitGuest:
                WaitGuest();
                break;
            case GameState.PlayerTurn:
                PlayerTurn();
                break;
            case GameState.ScoreState:
                ScoreState();
                break;
            case GameState.End:
                GameEnd();
                break;
        }
    }
    
    private void OnEnterState(GameState state)
    {
        switch (state)
        {
            case GameState.InitState:
                // InitStateの初期化処理
                //Debug.Log("Entering InitState");
                break;
            case GameState.WaitGuest:
                // WaitGuestの初期化処理
                //Debug.Log("Entering WaitGuest");
                break;
            case GameState.PlayerTurn:
                // PlayerTurnの初期化処理
                //Debug.Log("Entering PlayerTurn");
                break;
            case GameState.ScoreState:
                // ScoreStateの初期化処理
                //Debug.Log("Entering ScoreState");
                break;
            case GameState.End:
                // Endの初期化処理
                //Debug.Log("Entering End");
                break;
        }
    }

    private void GameEnd()
    {
        _player.Reset();
        Debug.Log("ゲーム終了");
        DOTween.KillAll();  // すべてのアニメーションを停止する
        SceneManager.LoadScene("Scoring");
    }

    private void OnDestroy()
    {
        DOTween.KillAll();  // すべてのアニメーションを停止する
    }

    private void ScoreState()
    {
        if (GuestManager.Instance.GetGuestOrderIndex() == _player.SubmitIndex)
        {
            Debug.Log("正解");
            AudioManager.Instance.PlaySFX(CorrectSE);
            GuestManager.Instance.Exit(true);
            ScoreManager.Instance.AddScore(100);
            _correctStreakCount = Mathf.Clamp(_correctStreakCount + 1 , 0, 6);
        }
        else
        {
            AudioManager.Instance.PlaySFX(IncorrectSE);
            GuestManager.Instance.Exit(false);
            _correctStreakCount = Mathf.Clamp(_correctStreakCount - 2 , 0, 6);
            ScoreManager.Instance.DecreaseScore(100);
        }

        GuestManager.Instance.AdjustMoveTime(_correctStreakCount);

        CurrentGameState = GameState.InitState;
    }

    private void PlayerTurn()
    {
        if (_player.IsSubmit)
        {
            CurrentGameState = GameState.ScoreState;
        }
        else if (GuestManager.Instance.IsTimeOver)
        {
            CurrentGameState = GameState.ScoreState;
        }
    }

    private void WaitGuest()
    {
        if (GuestManager.Instance.GuestIsOrdered())
        {
            CurrentGameState = GameState.PlayerTurn;
        }
    }

    private void InitState()
    {
        FoodManager.Instance.TableShuffle();
        GuestManager.Instance.SpawnGuest();
        _player.Reset();
        CurrentGameState = GameState.WaitGuest;
    }
}
