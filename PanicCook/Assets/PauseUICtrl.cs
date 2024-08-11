using System;
using System.Collections;
using System.Collections.Generic;
using FrameWork.EventCenter;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUICtrl : MonoBehaviour
{
    [SerializeField] GameObject pauseUI;
    [SerializeField] Button buttonMainMenu;
    [SerializeField] Button buttonRetry;
    
    
    
    private void Start()
    {
//        Debug.Log(buttonMainMenu.gameObject.name);
        ButtonPressedBehavior.AddButtonFunction(buttonMainMenu.gameObject.name, OnButtonMainMenuClicked);
//        Debug.Log(buttonRetry.gameObject.name);
        ButtonPressedBehavior.AddButtonFunction(buttonRetry.gameObject.name, Retry);
        
//        Debug.Log("Start");
        pauseUI.SetActive(false);
    }
    

    private void OnEnable()
    {
        EventCenter.AddListener(GameAction.Pause, Pause);
        EventCenter.AddListener(GameAction.UnPause, UnPause);
    }

    

    private void OnDisable()
    {
        EventCenter.RemoveListener(GameAction.Pause, Pause);
        EventCenter.RemoveListener(GameAction.UnPause, UnPause);
        
        ButtonPressedBehavior.buttonFunctionTable.Clear();
    }
    
    private void Pause()
    {
        pauseUI.SetActive(true);
    }

    private void UnPause()
    {
        pauseUI.SetActive(false);
    }

    /// <summary>
    /// メインメニューに戻る
    /// </summary>
    void OnButtonMainMenuClicked()
    {
        Debug.Log("メインメニューに戻る");
        SceneManager.LoadScene("Title");
        //SceneLoader.Instance.LoadMainMenuScene();
    }
    
    private void Retry()
    {
        //シーンロード
        SceneManager.LoadScene("Sato_");
    }
}
