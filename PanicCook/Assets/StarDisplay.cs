using System.Collections;
using System.Collections.Generic;
using FrameWork.Utils;
using UnityEngine;

public class StarDisplay : UnitySingleton<StarDisplay>
{
    [SerializeField] 
    private List<GameObject> _stars; // 現在表示されている星のリスト

    protected override void Awake()
    {
        base.Awake();
        for(int i = 0; i < _stars.Count; i++)
        {
            _stars[i].SetActive(false); // 全ての星を非アクティブにする
        }
    }

    /// <summary>
    /// 星の数を更新し、必要に応じて星をアクティブまたは非アクティブにする
    /// </summary>
    /// <param name="starCount">現在の星の数</param>
    public void UpdateStar(int starCount)
    {
        // 全ての星をループして処理
        for (int i = 0; i < _stars.Count; i++)
        {
            // iがstarCount未満ならアクティブ、それ以外は非アクティブ
            if (i < starCount)
            {
                _stars[i].SetActive(true); // 星をアクティブにする
            }
            else
            {
                _stars[i].SetActive(false); // 星を非アクティブにする
            }
        }
    }
}
