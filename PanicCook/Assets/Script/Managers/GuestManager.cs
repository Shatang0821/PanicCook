using System;
using System.Collections;
using System.Collections.Generic;
using FrameWork.Utils;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GuestManager : UnitySingleton<GuestManager>
{
    //客インスタンスを入れる親のTransform
    [SerializeField]
    private Transform _guestParentTransform;
    //客のプレハブ
    [SerializeField]
    private GameObject _guestPrefab;
    //客の画像配列
    [SerializeField]
    private Sprite[] _guestSprites;
    
    [SerializeField]
    private List<Expression> _guestExpressions;
    //客クラスのリスト
    private List<Guest> _guests = new List<Guest>();
    //現在の客
    private Guest _currentGuest;
    //料理の待ち時間
    [SerializeField]
    private float _waitTime = 3.0f;
    //客のインデックス
    [SerializeField]
    private int _guestIndex = 0;
    //待ちコルーチン
    Coroutine _waitCoroutine;
    //移動にかかる時間
    [SerializeField] private float _moveDuration = 0.2f;
    
    //時間オーバーかどうか
    public bool IsTimeOver { get; private set; }
    
    // StarAtWaitTimeに対応する数字をマッピングするDictionary
    private Dictionary<StarNowHave, float> _starWaitTimeValues = new Dictionary<StarNowHave, float>()
    {
        { StarNowHave.Zero, 3.0f },
        { StarNowHave.One, 2.5f },
        { StarNowHave.Two, 2.0f },
        { StarNowHave.Three, 1.5f },
        { StarNowHave.Four, 1.2f },
        { StarNowHave.Five, 0.8f }
    };
    
    enum CorrectStreakMoveTime
    {
        ZeroCorrect = 0,    // 連続正解が0回の時
        TwoCorrect = 2,     // 連続正解が2回の時
        FourCorrect = 4,    // 連続正解が4回の時
        SixCorrect = 6      // 連続正解が6回の時
    }
    // CorrectStreakMoveTimeに対応する移動時間をマッピングするDictionary
    private Dictionary<CorrectStreakMoveTime, float> _correctStreakMoveTimeValues = new Dictionary<CorrectStreakMoveTime, float>()
    {
        { CorrectStreakMoveTime.ZeroCorrect, 1.0f },
        { CorrectStreakMoveTime.TwoCorrect, 0.8f },
        { CorrectStreakMoveTime.FourCorrect, 0.6f },
        { CorrectStreakMoveTime.SixCorrect, 0.4f }
    };
    
    protected override void Awake()
    {
        base.Awake();
        //客が２人インスタンスを生成
        _guests.Add(new Guest());
        _guests.Add(new Guest());

        foreach (var VARIABLE in _guests)
        {
            VARIABLE.Initialize(Instantiate(_guestPrefab,Vector2.zero,Quaternion.identity,_guestParentTransform));
        }
        
//        Debug.Log( _guestExpressions.Count);
    }

    /// <summary>
    /// 星変化によって待ち時間を減る
    /// </summary>
    public void DecreaseTime(int star)
    {
        // Starの整数値を列挙型にキャスト
        StarNowHave starAtWaitTime = (StarNowHave)star;

        // 対応する待ち時間を減らす
        if (_starWaitTimeValues.TryGetValue(starAtWaitTime, out float waitTimeDecrease))
        {
            Debug.Log($"星 {star} に対応する待ち時間の減少: {waitTimeDecrease}");
            _waitTime = waitTimeDecrease;
        }
        else
        {
            Debug.LogWarning($"無効な星の値: {star}");
        }
    }
    
    /// <summary>
    /// 連続正解数によって移動時間を変更する
    /// </summary>
    public void AdjustMoveTime(int correctStreak)
    {
        // 正解数を列挙型にキャスト
        CorrectStreakMoveTime correctStreakMoveTime = (CorrectStreakMoveTime)correctStreak;

        // 対応する移動時間を取得し設定
        if (_correctStreakMoveTimeValues.TryGetValue(correctStreakMoveTime, out float moveTime))
        {
//            Debug.Log($"連続正解 {correctStreak} 回に対応する移動時間は {moveTime} 秒です。");
            _moveDuration = moveTime;
        }
    }

    /// <summary>
    /// 客を生成する処理
    /// コルーチンを呼び出す
    /// </summary>
    public void SpawnGuest()
    {
        IsTimeOver = false;
        StartCoroutine(nameof(SpawnGuestCoroutine));
    }

    /// <summary>
    /// 客を生成する
    /// </summary>
    /// <returns></returns>
    public IEnumerator SpawnGuestCoroutine()
    {
        _currentGuest = _guests[_guestIndex];
        _currentGuest.Spawn(_guestExpressions[Random.Range(0, _guestExpressions.Count)], new Vector3(1100, 0, 0), _moveDuration);
        _guestIndex = (_guestIndex + 1) % _guests.Count;
        
        yield return new WaitUntil(GuestIsOrdered);
        
        _waitCoroutine = StartCoroutine(nameof(WaitFoodCoroutine));
    }
    
    /// <summary>
    /// 何番目の料理を注文しているか
    /// </summary>
    /// <returns>注文している料理のテーブル番号</returns>
    public int GetGuestOrderIndex()
    {
        return _currentGuest.GetOrderIndex();
    }
    
    /// <summary>
    /// オーダーが出されたか
    /// </summary>
    /// <returns>true オーダーを出す,false オーダーがまだ</returns>
    public bool GuestIsOrdered()
    {
        return _currentGuest.IsOrdered;
    }
    
    /// <summary>
    /// 客が料理を待ちコルーチン
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitFoodCoroutine()
    {
        yield return new WaitForSeconds(_waitTime);
        Exit(false);
        //時間オーバー
        IsTimeOver = true;
    }

    public void Exit(bool isCorrect)
    {
        if(_waitCoroutine != null)
            StopCoroutine(_waitCoroutine);
        _currentGuest.Exit(_moveDuration,isCorrect);
    }
}

[Serializable]
public class Expression
{
    public Sprite normalExpression;
    public Sprite happyExpression;
    public Sprite angryExpression;
}