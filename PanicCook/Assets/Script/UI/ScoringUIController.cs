using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class ScoringUIController : MonoBehaviour
{
    [Header("==== SCORING SCREEN ====")]
    [SerializeField] Canvas scoringScreenCanvas;

    [SerializeField] Text playerScoreText;
    [SerializeField] Button buttonMainMenu;
    [SerializeField] Button buttonRetry;
    
    //Score Containerの子オブジェクトを取得するための
    [SerializeField] Transform highScoreLeaderboardContainer;

    [Header("==== HIGHT SCORE SCREEN ====")]
    [SerializeField] Canvas newHighScoreScreenCanvas;
    [SerializeField] Button buttonCancel;
    [SerializeField] Button buttonSubmit;

    [SerializeField] InputField playerNameInputField;

    private void Start()
    {
        //マウスカーソル表示
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (ScoreManager.Instance.HasNewHighScore)
        {
            ShowNewHighScoreScreen();
        }
        else
        {
            ShowScoringScreen();
        }

        ButtonPressedBehavior.buttonFunctionTable.Add(buttonMainMenu.gameObject.name, OnButtonMainMenuClicked);
        ButtonPressedBehavior.buttonFunctionTable.Add(buttonSubmit.gameObject.name, OnButtonSubmitClicked);
        ButtonPressedBehavior.buttonFunctionTable.Add(buttonCancel.gameObject.name, HideNewHighScoreScreen);
        ButtonPressedBehavior.buttonFunctionTable.Add(buttonRetry.gameObject.name, Retry);
        
    }

    /// <summary>
    /// 名前入力画面を表示する
    /// </summary>
    void ShowNewHighScoreScreen()
    {
        newHighScoreScreenCanvas.enabled = true;
    }

    /// <summary>
    /// 名前入力画面を閉じる
    /// </summary>
    void HideNewHighScoreScreen()
    {
        newHighScoreScreenCanvas.enabled = false;
        ScoreManager.Instance.SavePlayerScoreData();
        ShowScoringScreen();
    }

    private void OnDisable()
    {
        ButtonPressedBehavior.buttonFunctionTable.Clear();
    }

    private void Retry()
    {
        //シーンロード
    }

    /// <summary>
    /// スコア画面を表示する
    /// </summary>
    void ShowScoringScreen()
    {
        scoringScreenCanvas.enabled = true;
        playerScoreText.text = ScoreManager.Instance.Score.ToString();
        UpdateHighScoreLeaderboard();
    }

    /// <summary>
    /// スコアランキングをアップデート
    /// </summary>
    void UpdateHighScoreLeaderboard()
    {
        var playerScoreList = ScoreManager.Instance.LoadPlayerScoreData().list;

        for(int i = 0;i<highScoreLeaderboardContainer.childCount;i++)
        {
            var child = highScoreLeaderboardContainer.GetChild(i);

            child.Find("Star").GetComponent<Text>().text = (i + 1).ToString();
            child.Find("Score").GetComponent<Text>().text = playerScoreList[i].score.ToString();
            child.Find("Name").GetComponent<Text>().text = playerScoreList[i].playerName;

        }
    }

    /// <summary>
    /// メインメニューに戻る
    /// </summary>
    void OnButtonMainMenuClicked()
    {
        Debug.Log("メインメニューに戻る");
        scoringScreenCanvas.enabled = false;
        //SceneLoader.Instance.LoadMainMenuScene();
    }

    /// <summary>
    /// 新しい名前とスコアを提出する
    /// </summary>
    void OnButtonSubmitClicked()
    {
        if(!string.IsNullOrEmpty(playerNameInputField.text))
        {
            ScoreManager.Instance.SetPlayerName(playerNameInputField.text);
        }
        HideNewHighScoreScreen();
    }

    
}
