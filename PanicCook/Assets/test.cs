using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private List<RectTransform> _foodList = new List<RectTransform>();
    // Start is called before the first frame update
    void Start()
    {
        //listに子オブジェクトを追加
        foreach (RectTransform child in transform)
        {
            _foodList.Add(child);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
