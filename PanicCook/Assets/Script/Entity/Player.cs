using System;
using System.Collections;
using System.Collections.Generic;
using FrameWork.EventCenter;
using UnityEngine;
using UnityEngine.InputSystem;

enum PlayerAction
{
    Move,
    Submit
}

public class Player : MonoBehaviour
{
    public Transform TableTransform;        //テーブルのRectTransform
    private List<RectTransform> _foodTransforms;  //料理のRectTransform
    private int _centerIndex;               //中心位置のインデックス
    private int _currentIndex;              //現在の位置インデックス
    [SerializeField] private float _moveSpeed;               //移動速度
    private float _stopThreshold = 0.1f;    // 移動停止の閾値
    
    private bool _canSubmit;                //料理選択可能か
    
    private PlayerInput input;              //入力変数

    private RectTransform _rectTransform;   //プレイヤのRectTransform
    
    private Coroutine _moveCoroutine;       //移動コルーチン

    [SerializeField] private int _maxStamina;                //最大スタミナ
    private int _currentStamina;                             //スタミナ
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        input = new PlayerInput();
        
        Initialize();
    }

    /// <summary>
    /// 変数などの初期化
    /// </summary>
    private void Initialize()
    {
        _foodTransforms = new List<RectTransform>();
        foreach (Transform child in TableTransform)
        {
            RectTransform rectTransform = child as RectTransform;
            if (rectTransform != null)
            {
                _foodTransforms.Add(rectTransform);
            }
        }
        //中心位置のインデックスを取得
        _centerIndex = _foodTransforms.Count / 2;
        //初期位置インデクスを中心位置に設定
        _currentIndex = _centerIndex;
        //スタミナの設定
        _currentStamina = _maxStamina;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(transform.name + "座標は" + _rectTransform.anchoredPosition + "です");
        foreach (var VARIABLE in _foodTransforms)
        {
            Debug.Log(VARIABLE.name + "座標は" + VARIABLE.anchoredPosition);
        }
        SetPosToCenter();
    }


    private void OnEnable()
    {
        input.OnEnable();
        EventCenter.AddListener<Vector2>(PlayerAction.Move, OnMove);
        EventCenter.AddListener(PlayerAction.Submit, OnSubmit);
    }
    
    private void OnDisable()
    {
        input.OnDisable();
        EventCenter.RemoveListener<Vector2>(PlayerAction.Move, OnMove);
        EventCenter.RemoveListener(PlayerAction.Submit, OnSubmit);
    }

    void OnMove(Vector2 axis)
    {
        //Debug.Log(axis);
        if(_moveCoroutine != null)
        {
            StopCoroutine(_moveCoroutine);
        }
        _moveCoroutine = StartCoroutine(MoveCoroutine(axis));
    }
    
    void OnSubmit()
    {
        if(_canSubmit)
        {
            Debug.Log("選択");
            //ゲーム状態の遷移
        }
    }

    private IEnumerator MoveCoroutine(Vector2 axis)
    {
        
        _canSubmit = false;
        Vector2 startPos = _rectTransform.anchoredPosition;

        if (_currentStamina > 0)
        {
            if(axis.x > 0 && _currentIndex < _foodTransforms.Count - 1)
            {
                //右に移動
                _currentIndex++;
                _currentStamina = Mathf.Clamp(_currentStamina - 1, 0, _maxStamina);
            }
            else if(axis.x < 0 && _currentIndex > 0)
            {
                //左に移動
                _currentIndex--;
                _currentStamina = Mathf.Clamp(_currentStamina - 1, 0, _maxStamina);
            }
        }
        Debug.Log(_currentIndex + "スタミナ:" + _currentStamina);
        //移動先のRectTransformの座標を取得
        var targetPos = new Vector2(_foodTransforms[_currentIndex].anchoredPosition.x, _rectTransform.anchoredPosition.y);
        
        // 移動を開始
        while (Vector2.Distance(_rectTransform.anchoredPosition, targetPos) > _stopThreshold)
        {
            // 現在の位置を更新
            _rectTransform.anchoredPosition = Vector2.MoveTowards(
                _rectTransform.anchoredPosition, 
                targetPos, 
                _moveSpeed * Time.deltaTime
            );

            yield return null;
        }
        
        //プレイヤの座標を移動先に設定
        _rectTransform.anchoredPosition = targetPos;
        _canSubmit = true;
    }
    
    /// <summary>
    /// プレイヤを中心位置に設定
    /// </summary>
    private void SetPosToCenter()
    {
        //中心位置のRectTransformの座標を取得
        var initPos = _foodTransforms[_centerIndex].anchoredPosition;
        var pos = new Vector2(initPos.x, _rectTransform.anchoredPosition.y);
        //プレイヤの座標を中心位置に設定
        _rectTransform.anchoredPosition = pos;
    }
}
