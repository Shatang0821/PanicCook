using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Titlemaneger : MonoBehaviour
{
    [SerializeField] Text title, keypush, tutorial, keypush2, alart, nametext;
    public InputField inputfield;
    public string Restaurantname;
    public float step; // タイトルの進行がどれくらいか
    public bool wait; // 進行中にキーを押しても動かないようにするため
    [SerializeField] GameObject TutorialPanel, fieldobject;
    // Start is called before the first frame update
    void Start()
    {
        inputfield = inputfield.GetComponent<InputField>();
        step = 1;
        wait = false;
        title.enabled = true;
        keypush.enabled = true;
        alart.enabled = false;
        fieldobject.SetActive(false);
        inputfield.transform.position = new Vector3(0, -5, 0);
        TutorialPanel.transform.position = new Vector3(25, 0, 0);
        alart.text = "店名を入力しよう!!";
    }

    // Update is called once per frame
    void Update()
    {
        if (step == 1)
        {
            title.enabled = true;
            keypush.enabled = true;
        }
        else if (step == 2)
        {
            title.enabled = false;
            keypush.enabled = false;
            tutorial.text = "お客様の注文に沿った料理を提供して\n店のランクを上げよう!!";
            keypush.text = "SPACEキーで次へ";
            if (TutorialPanel.transform.position.x >= 0)
            {
                TutorialPanel.transform.position -= new Vector3(0.1f, 0, 0);
            }
        }
        else if (step == 3)
        {
            tutorial.text = "料理を提供するごとに\nポイントをゲット!!\nしかし料理の位置がランダムに\n移動してしまう!!";
        }
        else if (step == 4)
        {
            tutorial.text = "一人一人の待つ時間も\nランクが上がるにつれ\n減っていってしまう!!";
        }
        else if (step == 5)
        {
            fieldobject.SetActive(true);
            tutorial.text = "店名を決めよう!!\n";
            if (inputfield.transform.position.y <= 0)
            {
                inputfield.transform.position = new Vector3(0, 0, 0);
            }            
        }
        else if (step == 6)
        {
            alart.enabled = false;
            keypush2.text = "SPACEキーで営業開始!!";
            tutorial.text = "AとDキーで移動し\nSpaceキーで確定!!\n目指せ"+Restaurantname+"の評価星5!!";
        }
    }

    public void OnStart(InputAction.CallbackContext context)
    {
        if (wait == false&&step!=5)
        {
            wait = true;
            StartCoroutine(nextstep());
        }
        if (wait == false && step == 5)
        {
            if (string.IsNullOrEmpty(nametext.text))
            {
                alart.enabled = true;
            }
            else
            {
                wait = true;
                StartCoroutine (nextstep());
            }
        }
    }

    IEnumerator nextstep()
    {
        if (step == 6)
        {
            SceneManager.LoadScene("Sample"); // シーンの名前が分かったら書き換えます
        }
        if (step == 5)
        {
            Restaurantname = inputfield.text;
        }
            step++;
            yield return new WaitForSeconds(1.0f);
            wait = false;
    }
}
