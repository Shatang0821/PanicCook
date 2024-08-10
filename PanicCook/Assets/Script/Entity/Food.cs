using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Food
{
    //料理のImage
    [SerializeField]
    private Sprite _sprite;
    // 料理のタイプを表す変数
    [SerializeField]
    private FoodType _foodType;
    public Food(FoodType foodType,Sprite sprite)
    {
        _foodType = foodType;
        _sprite = sprite;
    }
    
    public Sprite GetSprite()
    {
        return _sprite;
    }
    
    public FoodType GetFoodType()
    {
        return _foodType;
    }
}
