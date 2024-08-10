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
    [SerializeField]
    private Transform _guestParentTransform;
    [SerializeField]
    private GameObject _guestPrefab;
    [SerializeField]
    private Sprite[] _guestSprites;    //客の画像
    
    private List<Guest> _guests = new List<Guest>();
    private Guest _currentGuest;
    [SerializeField]
    private float _waitTime = 5.0f;  //デフォルトで5秒待つ
    
    [SerializeField]
    private int _guestIndex = 0;
    
    Coroutine _waitCoroutine;

    [SerializeField] private float _moveDuration = 0.2f;
    
    public bool IsTimeOver { get; private set; }
    private void Awake()
    {
        _guests.Add(new Guest());
        _guests.Add(new Guest());

        foreach (var VARIABLE in _guests)
        {
            VARIABLE.Initialize(Instantiate(_guestPrefab,Vector2.zero,Quaternion.identity,_guestParentTransform));
        }
    }
    

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
        _currentGuest.Spawn(_guestSprites[Random.Range(0, _guestSprites.Length)], new Vector3(1100, 0, 0), _moveDuration);
        _guestIndex = (_guestIndex + 1) % _guests.Count;
        
        yield return new WaitUntil(GuestIsOrdered);
        _waitCoroutine = StartCoroutine(nameof(Wait));
    }
    
    public int GetGuestOrderIndex()
    {
        return _currentGuest.GetOrderIndex();
    }
    
    public bool GuestIsOrdered()
    {
        return _currentGuest.IsOrdered;
    }
    
    /// <summary>
    /// 客の待ち時間
    /// </summary>
    /// <returns></returns>
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(_waitTime);
        Exit();
        //時間オーバー
        IsTimeOver = true;
    }

    public void Exit()
    {
        if(_waitCoroutine != null)
            StopCoroutine(_waitCoroutine);
        _currentGuest.Exit(_moveDuration);
    }
}
