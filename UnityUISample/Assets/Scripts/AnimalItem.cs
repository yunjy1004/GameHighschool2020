using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewTest : MonoBehaviour
{

    public int m_index = 0;
    private bool m_bSelected = false;
    public Text m_txtanimal = null;

    public void initialize(int index, string name)
    {
        m_index = index;
        m_txtanimal.text = name;
    }

    public void SetSelect(bool bSelect)
    {
        m_bSelected = bSelect;

        if (bSelect)
        {
            m_txtanimal.color = (new Color32(255, 90, 235, 255));
        }

    }
}
