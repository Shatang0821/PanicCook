using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [SerializeField] Text Timetext;
    public float limit;
    // Start is called before the first frame update
    void Start()
    {
        limit = 60;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Timetext.text = "Žc‚è" + limit.ToString("f0") + "•b";

        limit -= Time.deltaTime;
        if (limit <= 0)
        {
            limit = 0;
        }
    }
}
