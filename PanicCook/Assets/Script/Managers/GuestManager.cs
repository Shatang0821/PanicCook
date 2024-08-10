using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestManager : MonoBehaviour
{
    private Guest _currentGuest;
    private float _waitTime = 5.0f;  //デフォルトで5秒待つ
    private Food _currentOrder;       //現在のオーダー

    private void Awake()
    {
        _currentGuest = new Guest();
    }

    public void SpawnGuest()
    {
        //ランダムで客を生成
        //オーダーをランダムで生成
        _currentOrder = new Food();
        
        
    }
}
