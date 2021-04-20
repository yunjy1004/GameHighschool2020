using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_0420_Q1 : MonoBehaviour
{
    public InputField inputtxt;
    public Button addbtn;
    public Button delbtn;
    public Dropdown drop1;
    private int st1;
    private string st2;

    void initialized()
    {
        drop1.options.Clear();
        inputtxt.text = "";
    }
    // Start is called before the first frame update
    void Start()
    {
        initialized();
        addbtn.onClick.AddListener(AddDropdown);
        delbtn.onClick.AddListener(RemoveDropdown);
        drop1.onValueChanged.AddListener(valuechanged1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void valuechanged1(int index)
    {
        st1 = index;
    }
    void AddDropdown()
    {
        Dropdown.OptionData a = new Dropdown.OptionData();
        if (inputtxt.text != "")
        {
            a.text = inputtxt.text;
        }

        if (drop1.options.Contains(a) == false)
        {
            drop1.options.Add(a);
            drop1.captionText.text = a.text;
        }
    }

    void RemoveDropdown()
    {
        drop1.options.Remove(drop1.options[st1]);
        drop1.RefreshShownValue();
    }
}
