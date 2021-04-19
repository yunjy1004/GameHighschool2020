using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testdrop : MonoBehaviour
{
    public Dropdown drop1;
    public Text txt1;
    public Button btn1;
    public Button btn2;
    private string st1;

    // Start is called before the first frame update
    void Start()
    {
        btn1.onClick.AddListener(result1);
        btn2.onClick.AddListener(cancel1);
        drop1.onValueChanged.AddListener(valuechanged1);

    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.localPosition.y < 0)
        {
            this.transform.localPosition += new Vector3(0, 350 * Time.deltaTime, 0);
        }
        if (this.transform.localPosition.y > 0)
        {
            this.transform.localPosition -= new Vector3(0, 350 * Time.deltaTime, 0);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, -350, 0);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, 350, 0);
        }
    }

    void valuechanged1(int index)
    {
        st1 = drop1.options[index].text;
    }

    void result1()
    {
        txt1.text = "당신이 간 곳은 " + st1 + " 입니다.";
    }

    void cancel1()
    {
        txt1.text = "";
    }
}
