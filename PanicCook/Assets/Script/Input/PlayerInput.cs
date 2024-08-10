using System.Collections;
using System.Collections.Generic;
using FrameWork.EventCenter;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput: InputActions.IGamePlayActions
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
    }
    
    public void OnDisable()
    {
        _inputActions.Disable();
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
}
