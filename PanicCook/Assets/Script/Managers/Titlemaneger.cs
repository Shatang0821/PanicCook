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
    public float step; // �^�C�g���̐i�s���ǂꂭ�炢��
    public bool wait; // �i�s���ɃL�[�������Ă������Ȃ��悤�ɂ��邽��
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
        alart.text = "�X������͂��悤!!";
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
            tutorial.text = "���q�l�̒����ɉ�����������񋟂���\n�X�̃����N���グ�悤!!";
            keypush.text = "SPACE�L�[�Ŏ���";
            if (TutorialPanel.transform.position.x >= 0)
            {
                TutorialPanel.transform.position -= new Vector3(0.1f, 0, 0);
            }
        }
        else if (step == 3)
        {
            tutorial.text = "������񋟂��邲�Ƃ�\n�|�C���g���Q�b�g!!\n�����������̈ʒu�������_����\n�ړ����Ă��܂�!!";
        }
        else if (step == 4)
        {
            tutorial.text = "��l��l�̑҂��Ԃ�\n�����N���オ��ɂ�\n�����Ă����Ă��܂�!!";
        }
        else if (step == 5)
        {
            fieldobject.SetActive(true);
            tutorial.text = "�X�������߂悤!!\n";
            if (inputfield.transform.position.y <= 0)
            {
                inputfield.transform.position = new Vector3(0, 0, 0);
            }            
        }
        else if (step == 6)
        {
            alart.enabled = false;
            keypush2.text = "SPACE�L�[�ŉc�ƊJ�n!!";
            tutorial.text = "A��D�L�[�ňړ���\nSpace�L�[�Ŋm��!!\n�ڎw��"+Restaurantname+"�̕]����5!!";
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
            SceneManager.LoadScene("Sample"); // �V�[���̖��O�����������珑�������܂�
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
