using System.Collections;
using System.Collections.Generic;
using FrameWork.EventCenter;
using UnityEngine;
using UnityEngine.InputSystem;

public enum InputActionEnum
{
    EnableGamePlayInput,
    EnablePauseMenuInput
}

public class PlayerInput: InputActions.IGamePlayActions,InputActions.IPauseActions
{
    #region 変数定義

    private InputActions _inputActions;
    
    //private InputDevice _currentDevice; //現在デバイス
    //public InputDevice CurrentDevice;
    
    //移動
    public Vector2 Axis => _inputActions.GamePlay.Axis.ReadValue<Vector2>();
    
    #endregion
    
    public PlayerInput()        
    {
        _inputActions = new InputActions();
    }
    
    public void OnEnable()
    {
        _inputActions.Enable();
        _inputActions.GamePlay.SetCallbacks(this);
        _inputActions.Pause.SetCallbacks(this);
    }
    
    public void OnDisable()
    {
        _inputActions.Disable();
    }
    
    /// <summary>
    /// ゲーム内でキャラクターを操作する時に入力を有効化するメソッド。
    /// </summary>
    public void EnableGameplayInput() => SwitchActionMap(_inputActions.GamePlay, false);

    /// <summary>
    /// 一時停止画面内の入力を有効化するメソッド
    /// </summary>
    public void EnablePauseMenuiInput() => SwitchActionMap(_inputActions.Pause, true);
    
    /// <summary>
    /// 有効actionmapを変わり
    /// </summary>
    /// <param name="actionMap">変えたいactionMap</param>
    /// <param name="isUIInput">UIの選択か</param>
    void SwitchActionMap(InputActionMap actionMap, bool isUIInput)
    {
        _inputActions.Disable();
        actionMap.Enable();

        if(isUIInput)
        {
            Cursor.visible = true;                     // マウスカーソルを可視にします。
            Cursor.lockState = CursorLockMode.None;    // マウスカーソルをロックしない。
        }
        else
        {
            Cursor.visible = false;                     // マウスカーソルを不可視にします。
            Cursor.lockState = CursorLockMode.Locked;   // マウスカーソルをロックする。
        }
    }

    public void OnAxis(InputAction.CallbackContext context)
    {
        //Debug.Log("aaa");
        if (context.performed)
        {
            EventCenter.TriggerEvent(PlayerAction.Move, context.ReadValue<Vector2>());
        }
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            EventCenter.TriggerEvent(PlayerAction.Submit);
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Debug.Log("Pause");
            EnablePauseMenuiInput();
            EventCenter.TriggerEvent(GameAction.Pause);
        }
    }

    public void OnUnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("UnPause");
            EnableGameplayInput();
            EventCenter.TriggerEvent(GameAction.UnPause);
        }
    }
}
