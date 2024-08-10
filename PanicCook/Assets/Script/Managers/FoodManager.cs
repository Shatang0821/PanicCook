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
        �I�����C�X,
        Potato
    }

    int Max; // ���[�v���~�߂邽�߂̍ő�l
    food[] foodname; // �����̕��т�����z��
    [SerializeField] Image Menu1, Menu2, Menu3, Menu4, Menu5; // ������\������摜
    [SerializeField] Sprite Steak, Pizza, Sushi, �I�����C�X, Potato; // ���ꂼ��̗����̉摜

    // Start is called before the first frame update
    void Start()
    {
        Max = 5; // �ő�l��5
        foodname = new food[5]; // 5�g�̔z����쐬
        InitializeFoodTable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitializeFoodTable()
    {
        for (int i=0;i>=Max;i++) // Max=5�Ȃ̂�5��J��Ԃ�
        {
            //enum�^�̗v�f�����擾
            int maxCount = Enum.GetNames(typeof(food)).Length;

            //�����_���Ȑ������擾
            int number = UnityEngine.Random.Range(0, maxCount);

            //int�^����enum�^�֕ϊ�
            foodname[i] = (food)Enum.ToObject(typeof(food), number);
            if (foodname[0] == foodname[i]) // ���ꂽ���̂����̘g�Ɣ��Ȃ�J�E���g�����炵������x���I����
            {
                i--; // ���̂܂�continue���Ă�������x���[�v���邾���Ŕ�����܂�
                // �i��ł��܂��̂œ����g���Ē��I����
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
        // �z��̂��ꂼ��̘g�ɉ��������Ă��邩�ɉ����Ċe�摜�̃X�v���C�g��؂�ւ���
        #region �摜�ύX1
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
        else if (foodname[0] == food.�I�����C�X)
        {
            Menu1 = GetComponent<Image>();
            Menu1.sprite = �I�����C�X;
        }
        else if (foodname[0] == food.Potato)
        {
            Menu1 = GetComponent<Image>();
            Menu1.sprite = Potato;
        }
        #endregion
        #region �摜�ύX2
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
        else if (foodname[1] == food.�I�����C�X)
        {
            Menu2 = GetComponent<Image>();
            Menu2.sprite = �I�����C�X;
        }
        else if (foodname[1] == food.Potato)
        {
            Menu2 = GetComponent<Image>();
            Menu2.sprite = Potato;
        }
        #endregion
        #region �摜�ύX3
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
        else if (foodname[2] == food.�I�����C�X)
        {
            Menu3 = GetComponent<Image>();
            Menu3.sprite = �I�����C�X;
        }
        else if (foodname[2] == food.Potato)
        {
            Menu3 = GetComponent<Image>();
            Menu3.sprite = Potato;
        }
        #endregion
        #region �摜�ύX4
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
        else if (foodname[3] == food.�I�����C�X)
        {
            Menu4 = GetComponent<Image>();
            Menu4.sprite = �I�����C�X;
        }
        else if (foodname[3] == food.Potato)
        {
            Menu4 = GetComponent<Image>();
            Menu4.sprite = Potato;
        }
        #endregion
        #region �摜�ύX5
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
        else if (foodname[4] == food.�I�����C�X)
        {
            Menu5 = GetComponent<Image>();
            Menu5.sprite = �I�����C�X;
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
