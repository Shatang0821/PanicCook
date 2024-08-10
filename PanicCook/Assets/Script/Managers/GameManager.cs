using System.Collections;
using DG.Tweening;
using FrameWork.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    InitState,      // 料理のリセット、主人公のリセット、客のリセット
    WaitGuest,      // 客が出てきて、オーダーを表示
    PlayerTurn,     // プレイヤの移動、料理の提供
    ScoreState,     // スコアの計算
    End             // ゲーム終了
}

public class GameManager : UnitySingleton<GameManager>
{
    [SerializeField]
    private Player _player;
    
    private GameState _currentGameState;

    public GameState CurrentGameState
    {
        get => _currentGameState;
        set
        {
            if (_currentGameState != value)
            {
                _currentGameState = value;
                OnEnterState(_currentGameState);
            }
        }
    }

    private IEnumerator Start()
    {
        CurrentGameState = GameState.InitState;
        yield return new WaitForSeconds(1.5f);
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
                Debug.Log("Entering InitState");
                break;
            case GameState.WaitGuest:
                // WaitGuestの初期化処理
                Debug.Log("Entering WaitGuest");
                break;
            case GameState.PlayerTurn:
                // PlayerTurnの初期化処理
                Debug.Log("Entering PlayerTurn");
                break;
            case GameState.ScoreState:
                // ScoreStateの初期化処理
                Debug.Log("Entering ScoreState");
                break;
            case GameState.End:
                // Endの初期化処理
                Debug.Log("Entering End");
                break;
        }
    }

    private void GameEnd()
    {
        _player.Reset();
        Debug.Log("ゲーム終了");
        DOTween.KillAll();  // すべてのアニメーションを停止する場合
        SceneManager.LoadScene("Scoring");
    }

    private void ScoreState()
    {
        if (GuestManager.Instance.GetGuestOrderIndex() == _player.SubmitIndex)
        {
            Debug.Log("正解");
            GuestManager.Instance.Exit();
            ScoreManager.Instance.AddScore(100);
        }
        else
        {
            GuestManager.Instance.Exit();
        }

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
