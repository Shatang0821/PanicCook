using System.Collections.Generic;

using FrameWork.Utils;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

using DG.Tweening;


public enum FoodType
{
    Steak,
    Pizza,
    Sushi,
    OmeletRice,
    Potato,
    Cake
}


public class FoodManager : UnitySingleton<FoodManager>
{
    //テーブルのRectTransform
    public Transform TableTransform;        
    //料理の表示するためのImage
    [SerializeField] private Image[] _foodImageTable;
    //料理のリスト
    [SerializeField] private List<Food> _foods = new List<Food>();
    //テーブルの料理リスト
    [SerializeField] private List<Food> _foodsTable = new List<Food>();
    //テーブルの数
    private int _tableCount = 0;    
    
    protected override void Awake()
    {
        base.Awake();
        
        _foodImageTable = new Image[5];
        
        foreach (Transform child in TableTransform)
        {
            Image image = child.GetComponent<Image>();
            if (image != null)
            {
                _foodImageTable[_tableCount] = image;
                _tableCount++;
            }
            else
            {
                Debug.Log("Imageが見つかりませんでした");
            }
        }
        
        Debug.Log(_tableCount);
    }

    /// <summary>
    /// 料理一覧からランダムに５個取り出す
    /// </summary>
    public void TableShuffle()
    {
        if (_foods.Count >= _tableCount)
        {
            int index = 0;
            _foodsTable = _foods.OrderBy(x => Random.value).Take(_tableCount).ToList();
            foreach (var food in _foodsTable)
            {
                // 料理の画像をセット
                _foodImageTable[index].sprite = food.GetSprite();

                // 回転アニメーションを追加（360度回転）
                _foodImageTable[index].transform.DORotate(new Vector3(0, 360, 0), 0.7f, RotateMode.FastBeyond360)
                    .SetEase(Ease.OutQuad);

                index++;
            }
        }
    }

    /// <summary>
    /// 添え字で指定した料理を取得
    /// </summary>
    /// <param name="index">添え字</param>
    /// <returns>料理クラス</returns>
    public Food GetFoodAtIndex(int index)
    {
        if (index < 0 || index >= _foods.Count)
        {
            return null;
        }    
        return _foodsTable[index];
    }

}
