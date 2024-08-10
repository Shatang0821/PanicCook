using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkerText : MonoBehaviour
{
    public float speed = 2.0f;
    private float time;
    private Text text;


    void Start()
    {
        text = this.gameObject.GetComponent<Text>();
    }

    void FixedUpdate()
    {
        text.color = GetTextColorAlpha(text.color);
    }

    Color GetTextColorAlpha(Color color)
    {
        time += Time.deltaTime * speed * 5.0f;
        color.a = Mathf.Sin(time);

        return color;
    }
}
