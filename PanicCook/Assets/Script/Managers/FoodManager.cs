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
    //�e�[�u����RectTransform
    public Transform TableTransform;        
    //�����̕\�����邽�߂�Image
    [SerializeField] private Image[] _foodImageTable;
    //�����̃��X�g
    [SerializeField] private List<Food> _foods = new List<Food>();
    //�e�[�u���̗������X�g
    [SerializeField] private List<Food> _foodsTable = new List<Food>();
    //�e�[�u���̐�
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
                Debug.Log("Image��������܂���ł���");
            }
        }
        
        Debug.Log(_tableCount);
    }

    /// <summary>
    /// �����ꗗ���烉���_���ɂT���o��
    /// </summary>
    public void TableShuffle()
    {
        if (_foods.Count >= _tableCount)
        {
            int index = 0;
            _foodsTable = _foods.OrderBy(x => Random.value).Take(_tableCount).ToList();
            foreach (var food in _foodsTable)
            {
                // �����̉摜���Z�b�g
                _foodImageTable[index].sprite = food.GetSprite();

                // ��]�A�j���[�V������ǉ��i360�x��]�j
                _foodImageTable[index].transform.DORotate(new Vector3(0, 360, 0), 0.7f, RotateMode.FastBeyond360)
                    .SetEase(Ease.OutQuad);

                index++;
            }
        }
    }

    /// <summary>
    /// �Y�����Ŏw�肵���������擾
    /// </summary>
    /// <param name="index">�Y����</param>
    /// <returns>�����N���X</returns>
    public Food GetFoodAtIndex(int index)
    {
        if (index < 0 || index >= _foods.Count)
        {
            return null;
        }    
        return _foodsTable[index];
    }

}
