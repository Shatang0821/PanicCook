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
    public bool IsSubmit;                   //料理を提供したか
    public int SubmitIndex { get; private set; }    //提供した料理のインデックス
    
    private PlayerInput input;              //入力変数

    private RectTransform _rectTransform;   //プレイヤのRectTransform
    
    private Coroutine _moveCoroutine;       //移動コルーチン
    private Coroutine _doMoveCoroutine;     //移動コルーチン
    private Coroutine _submitBufferCoroutine; //入力バッファコルーチン
    [SerializeField]
    private float submitBuffertime;         //入力バッファ時間
    private WaitForSeconds _submitBufferWait; //入力バッファ待機時間
    [SerializeField]
    private bool _hasSubmitBuffer = false;           //入力バッファ中か
    [SerializeField] private int _maxStamina;                //最大スタミナ
    private int _currentStamina;                             //スタミナ
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        input = new PlayerInput();
        
        Initialize();
        
        //入力バッファ待機時間の設定
        _submitBufferWait = new WaitForSeconds(submitBuffertime);
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
//        Debug.Log(transform.name + "座標は" + _rectTransform.anchoredPosition + "です");
        foreach (var VARIABLE in _foodTransforms)
        {
//            Debug.Log(VARIABLE.name + "座標は" + VARIABLE.anchoredPosition);
        }
        SetPosToCenter();
        
        input.EnableGameplayInput();
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

    private void Update()
    {
        if(GameManager.Instance.CurrentGameState == GameState.PlayerTurn)
        {
            if (_hasSubmitBuffer)
            {
                if(_canSubmit)
                {
                    IsSubmit = true;
                    SubmitIndex = _currentIndex;
                    Debug.Log("SubmitIndex:" + SubmitIndex);
                }
            }
        }
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
    
    private void OnSubmit()
    {
        if(GameManager.Instance.CurrentGameState != GameState.PlayerTurn)
            return;
        
        if(_submitBufferCoroutine!=null)
        {
            StopCoroutine(_submitBufferCoroutine);
        }
        _submitBufferCoroutine = StartCoroutine(SubmitBufferCoroutine());
        
        if(_canSubmit)
        {
            IsSubmit = true;
            SubmitIndex = _currentIndex;
            Debug.Log("SubmitIndex:" + SubmitIndex);
        }
    }
    
    private IEnumerator SubmitBufferCoroutine()
    {
        _hasSubmitBuffer = true;
        yield return _submitBufferWait;
        _hasSubmitBuffer = false;
    }

    private IEnumerator MoveCoroutine(Vector2 axis)
    {
        if(GameManager.Instance.CurrentGameState != GameState.PlayerTurn)
            yield break;
        
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

        //移動先のRectTransformの座標を取得
        var targetPos = new Vector2(_foodTransforms[_currentIndex].anchoredPosition.x, _rectTransform.anchoredPosition.y);
        if(_doMoveCoroutine != null)
            StopCoroutine(_doMoveCoroutine);
        _doMoveCoroutine = StartCoroutine(MoveToTargetPosCoroutine(targetPos));
        
    }
    
    /// <summary>
    /// プレイヤを中心位置に設定
    /// </summary>
    private void SetPosToCenter()
    {
        _currentIndex = _centerIndex;
        //中心位置のRectTransformの座標を取得
        var initPos = _foodTransforms[_currentIndex].anchoredPosition;
        var targetPos = new Vector2(initPos.x, _rectTransform.anchoredPosition.y);
        _rectTransform.anchoredPosition = targetPos;
    }

    public void Reset()
    {
        SetPosToCenter();
        _canSubmit = true;
        _currentStamina = _maxStamina;
        IsSubmit = false;
        SubmitIndex = -1;
        _hasSubmitBuffer = false;
    }

    /// <summary>
    /// 指定位置まで移動する
    /// </summary>
    /// <param name="targetPos">指定位置</param>
    private IEnumerator MoveToTargetPosCoroutine(Vector2 targetPos)
    {
        // 移動を開始
        while (Vector2.Distance(_rectTransform.anchoredPosition, targetPos) > _stopThreshold && GameManager.Instance.CurrentGameState == GameState.PlayerTurn)
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
}
