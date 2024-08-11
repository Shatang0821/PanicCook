using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Guest
{
    private Food _order;    //オーダー
    private int _orderIndex;    //オーダーのインデックス
    GameObject _guest;          //客のオブジェクト
    Image _guestImage;          //客の画像
    Image _orderImage;          //オーダーの画像
    RectTransform _guestTransform;   //客のRectTransform

    public bool IsOrdered = false;
    
    private Expression _expression;    //表情
    /// <summary>
    /// 初期化処理
    /// </summary>
    /// <param name="guest">客オブジェクト</param>
    public void Initialize(GameObject guest)
    {
        _guest = guest;
        _guestImage = guest.GetComponent<Image>();
        _guestTransform = guest.GetComponent<RectTransform>();
        
        Transform orderTransform = guest.transform.Find("OrderImage");
        if (orderTransform != null)
        {
            _orderImage = orderTransform.GetComponent<Image>();
            _orderImage.enabled = false;
        }
        
        _guest.SetActive(false);
    }

    public void Spawn(Sprite sprite,Vector3 position,float duration)
    {
        _guestImage.sprite = sprite;
        _guestTransform.anchoredPosition = position;
        _guest.SetActive(true);
        
        _guestTransform.DOAnchorPosX(position.x - 1100, duration).SetEase(Ease.Linear).onComplete = () =>
        {
            SetOrder();
            ShowOrder();
            IsOrdered = true;
        };
    }
    
    public void SetOrder()
    {
        _orderIndex = Random.Range(0, 5);
        _order = FoodManager.Instance.GetFoodAtIndex(_orderIndex);
    }

    public int GetOrderIndex() => _orderIndex;
    
    public void ShowOrder()
    {
       _orderImage.sprite = _order.GetSprite();
       _orderImage.enabled = true;
       
    }

    public void Exit(float duration)
    {
        
        _guestTransform.DOAnchorPosX(_guestTransform.anchoredPosition.x - 1100, duration).SetEase(Ease.Linear).onComplete = () =>
        {
            _guest.SetActive(false);
            _orderImage.enabled = false;
            IsOrdered = false;
        };
    }
    
}
