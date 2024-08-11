using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock_Timer : MonoBehaviour
{
    //制限時間用の変数
    [SerializeField] float TimerLimit;

    //経過時間を保持する変数
    float seconds = 0f;

    //ClockをInspector上から設定するため
    [SerializeField] Clock clock;
    
    // Update is called once per frame
    void Update()
    {
        //ClockのUpdateClock関数を呼び出す
        //引数は_updateTimer()のtimerの値
        if (GameManager.Instance.CurrentGameState != GameState.Default)
        {
            Debug.Log("Clock_Timer");
            clock.UpdateClock(_updateTimer());
        }
        
    }

    float _updateTimer()
    {
        //経過時間を取得
        seconds += Time.deltaTime;

        //経過時間を制限時間で割る
        float timer = seconds / TimerLimit;

        //確認用
//        Debug.Log(timer);

        //float型のtimerを返す
        return timer;
    }
}