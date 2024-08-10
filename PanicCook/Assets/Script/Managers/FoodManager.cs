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
    public Transform TableTransform;        //�e�[�u����RectTransform
    //�����̂��M
    [SerializeField] private Image[] _foodTable;
    
    //�����̃��X�g
    [SerializeField] private List<Food> _foods = new List<Food>();
    
    protected override void Awake()
    {
        base.Awake();
        
        _foodTable = new Image[5];

        int index = 0;
        foreach (Transform child in TableTransform)
        {
            Image image = child.GetComponent<Image>();
            if (image != null)
            {
                _foodTable[index] = image;
                index++;
            }
            else
            {
                Debug.Log("Image��������܂���ł���");
            }
        }
    }

    public void TableShuffle()
    {
        if (_foods.Count >= 5)
        {
            int index = 0;
            List<Food> randomFoods = _foods.OrderBy(x => Random.value).Take(5).ToList();
            foreach (var food in randomFoods)
            {
                // �����̉摜���Z�b�g
                _foodTable[index].sprite = food.GetSprite();

                // ��]�A�j���[�V������ǉ��i360�x��]�j
                _foodTable[index].transform.DORotate(new Vector3(0, 360, 0), 0.7f, RotateMode.FastBeyond360)
                    .SetEase(Ease.OutQuad);

                index++;
            }
        }
    }

    
    public Food GetFoodAtIndex(int index)
    {
        if (index < 0 || index >= _foods.Count)
        {
            return null;
        }    
        Debug.Log(index + "," +_foods[index].GetFoodType());
        return _foods[index];
    }

}
