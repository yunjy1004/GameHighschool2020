using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testdrop2 : MonoBehaviour
{
    public Text resulttxt;
    public Button okbtn;
    public Button cancelbtn;
    public Dropdown drop1;
    public GameObject panel1;

    private string str1;

    void Initalized()
    {
        drop1.options.Clear();
    }

    void Start()
    {
        Initalized();
        startwords();
        okbtn.onClick.AddListener(ptok);
        cancelbtn.onClick.AddListener(ptcan);
        drop1.onValueChanged.AddListener(dropwords);
    }

    public void ptok()
    {
        resulttxt.text = "당신은 " + str1 + "의로 여행을갔다이말이야";
    }

    public void ptcan()
    {
        resulttxt.text = "";
    }

    public void dropwords(int index)
    {
        str1 = drop1.options[index].text;

    }
    public void startwords()
    {
        Dropdown.OptionData a = new Dropdown.OptionData("서울");
        Dropdown.OptionData b = new Dropdown.OptionData("광주");
        Dropdown.OptionData c = new Dropdown.OptionData("대전");
        Dropdown.OptionData d = new Dropdown.OptionData("부산");
        Dropdown.OptionData e = new Dropdown.OptionData("전주");
        drop1.options.Add (a);
        drop1.options.Add (b);
        drop1.options.Add (c);
        drop1.options.Add (d);
        drop1.options.Add (e);
    }

    public void panelmove()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            panel1.transform.localPosition = new Vector3(0, 400, 0);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            panel1.transform.localPosition = new Vector3(-400, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            panel1.transform.localPosition = new Vector3(0, -400, 0);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            panel1.transform.localPosition = new Vector3(400, 0, 0);
        }
    }
    // Update is called once per frame
    void Update()
    {
        panelmove();

        if(panel1.transform.localPosition.x < 0)
        {
            panel1.transform.localPosition += new Vector3(200 * Time.deltaTime, 0, 0);
        }
        else if (panel1.transform.localPosition.x > 0)
        {
           panel1.transform.localPosition -= new Vector3(200 * Time.deltaTime, 0, 0);
        }
       else if (panel1.transform.localPosition.y < 0)
       {
            panel1.transform.localPosition += new Vector3(0, 200 * Time.deltaTime, 0);
       }
       else if (panel1.transform.localPosition.y > 0)
       {
          panel1.transform.localPosition -= new Vector3(0, 200 * Time.deltaTime, 0);
       }
   }
}

