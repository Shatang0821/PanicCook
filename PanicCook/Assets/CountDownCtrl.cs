using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownCtrl : MonoBehaviour
{
    [SerializeField]
    Text text;
    private int count = 3;

    private void Start()
    {
        gameObject.SetActive(true);
    }

    public void SetText()
    {
        count = Mathf.Clamp(count - 1, 0, 3);
        text.text = count.ToString();
    }

    public void StartGame()
    {
        gameObject.SetActive(false);
        GameManager.Instance.StartGame();
    }
}
