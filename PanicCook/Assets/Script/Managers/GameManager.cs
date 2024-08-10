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
    
    End
}

public class GameManager : UnitySingleton<GameManager>
{
    [SerializeField]
    private Player _player;
    
    public GameState CurrentGameState;
    
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
                GameEnd();;
                break;
        }
        //Debug.Log("currentGameState:" + CurrentGameState);
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
//        Debug.Log(GuestManager.Instance.GetGuestOrderIndex() + "を求める" + _player.SubmitIndex + "を提出");
        if(GuestManager.Instance.GetGuestOrderIndex() == _player.SubmitIndex)
        {
            Debug.Log("正解");
            GuestManager.Instance.Exit();
            ScoreManager.Instance.AddScore(100);
        }
        else
        {
//            Debug.Log("不正解");
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
        if(GuestManager.Instance.GuestIsOrdered())
        {
            CurrentGameState = GameState.PlayerTurn;
        }
    }

    private void InitState()
    {
        //料理のランダム表示
        FoodManager.Instance.TableShuffle();
        //プレイヤのリセット
        _player.Reset();
        GuestManager.Instance.SpawnGuest();
        CurrentGameState = GameState.WaitGuest;
    }
}
