using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [SerializeField] Text Timetext;
    public float limit = 60.0f;

    // Update is called once per frame
    void Update()
    {
        Timetext.text = limit.ToString("f0");

        if (GameManager.Instance.CurrentGameState != GameState.End)
        {
            limit -= Time.deltaTime;
            if (limit <= 0)
            {
                limit = 0;
                GameManager.Instance.CurrentGameState = GameState.End;
            }
        }
        
    }
}
