using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class FoodManager : MonoBehaviour
{
    enum food
    {
        Steak,
        Pizza,
        Sushi,
        オムライス,
        Potato
    }

    int Max; // ループを止めるための最大値
    food[] foodname; // 料理の並びを入れる配列
    [SerializeField] Image Menu1, Menu2, Menu3, Menu4, Menu5; // 料理を表示する画像
    [SerializeField] Sprite Steak, Pizza, Sushi, オムライス, Potato; // それぞれの料理の画像

    // Start is called before the first frame update
    void Start()
    {
        Max = 5; // 最大値を5
        foodname = new food[5]; // 5枠の配列を作成
        InitializeFoodTable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitializeFoodTable()
    {
        for (int i=0;i>=Max;i++) // Max=5なので5回繰り返す
        {
            //enum型の要素数を取得
            int maxCount = Enum.GetNames(typeof(food)).Length;

            //ランダムな整数を取得
            int number = UnityEngine.Random.Range(0, maxCount);

            //int型からenum型へ変換
            foodname[i] = (food)Enum.ToObject(typeof(food), number);
            if (foodname[0] == foodname[i]) // 入れたものが他の枠と被るならカウントを減らしもう一度抽選する
            {
                i--; // そのままcontinueしてももう一度ループするだけで被ったまま
                // 進んでしまうので同じ枠を再抽選する
                continue;
            }
            else if (foodname[1] == foodname[i])
            {
                i--;
                continue;
            }
            else if (foodname[2] == foodname[i])
            {
                i--;
                continue;
            }
            else if (foodname[3] == foodname[i])
            {
                i--;
                continue;
            }
            else if (foodname[4] == foodname[i])
            {
                i--;
                continue;
            }
        }
        // 配列のそれぞれの枠に何が入っているかに応じて各画像のスプライトを切り替える
        #region 画像変更1
        if (foodname[0] == food.Steak)
        {
            Menu1 = GetComponent<Image>();
            Menu1.sprite = Steak;
        }
        else if (foodname[0] == food.Pizza)
        {
            Menu1 = GetComponent<Image>();
            Menu1.sprite = Pizza;
        }
        else if (foodname[0] == food.Sushi)
        {
            Menu1 = GetComponent<Image>();
            Menu1.sprite = Sushi;
        }
        else if (foodname[0] == food.オムライス)
        {
            Menu1 = GetComponent<Image>();
            Menu1.sprite = オムライス;
        }
        else if (foodname[0] == food.Potato)
        {
            Menu1 = GetComponent<Image>();
            Menu1.sprite = Potato;
        }
        #endregion
        #region 画像変更2
        if (foodname[1] == food.Steak)
        {
            Menu2 = GetComponent<Image>();
            Menu2.sprite = Steak;
        }
        else if (foodname[1] == food.Pizza)
        {
            Menu2 = GetComponent<Image>();
            Menu2.sprite = Pizza;
        }
        else if (foodname[1] == food.Sushi)
        {
            Menu2 = GetComponent<Image>();
            Menu2.sprite = Sushi;
        }
        else if (foodname[1] == food.オムライス)
        {
            Menu2 = GetComponent<Image>();
            Menu2.sprite = オムライス;
        }
        else if (foodname[1] == food.Potato)
        {
            Menu2 = GetComponent<Image>();
            Menu2.sprite = Potato;
        }
        #endregion
        #region 画像変更3
        if (foodname[2] == food.Steak)
        {
            Menu3 = GetComponent<Image>();
            Menu3.sprite = Steak;
        }
        else if (foodname[2] == food.Pizza)
        {
            Menu3 = GetComponent<Image>();
            Menu3.sprite = Pizza;
        }
        else if (foodname[2] == food.Sushi)
        {
            Menu3 = GetComponent<Image>();
            Menu3.sprite = Sushi;
        }
        else if (foodname[2] == food.オムライス)
        {
            Menu3 = GetComponent<Image>();
            Menu3.sprite = オムライス;
        }
        else if (foodname[2] == food.Potato)
        {
            Menu3 = GetComponent<Image>();
            Menu3.sprite = Potato;
        }
        #endregion
        #region 画像変更4
        if (foodname[3] == food.Steak)
        {
            Menu4 = GetComponent<Image>();
            Menu4.sprite = Steak;
        }
        else if (foodname[3] == food.Pizza)
        {
            Menu4 = GetComponent<Image>();
            Menu4.sprite = Pizza;
        }
        else if (foodname[3] == food.Sushi)
        {
            Menu4 = GetComponent<Image>();
            Menu4.sprite = Sushi;
        }
        else if (foodname[3] == food.オムライス)
        {
            Menu4 = GetComponent<Image>();
            Menu4.sprite = オムライス;
        }
        else if (foodname[3] == food.Potato)
        {
            Menu4 = GetComponent<Image>();
            Menu4.sprite = Potato;
        }
        #endregion
        #region 画像変更5
        if (foodname[4] == food.Steak)
        {
            Menu5 = GetComponent<Image>();
            Menu5.sprite = Steak;
        }
        else if (foodname[4] == food.Pizza)
        {
            Menu5 = GetComponent<Image>();
            Menu5.sprite = Pizza;
        }
        else if (foodname[4] == food.Sushi)
        {
            Menu5 = GetComponent<Image>();
            Menu5.sprite = Sushi;
        }
        else if (foodname[4] == food.オムライス)
        {
            Menu5 = GetComponent<Image>();
            Menu5.sprite = オムライス;
        }
        else if (foodname[4] == food.Potato)
        {
            Menu5 = GetComponent<Image>();
            Menu5.sprite = Potato;
        }
        #endregion
    }

    /*
    int GetfoodAtPosition(int index )
    {

    }
    */
}
