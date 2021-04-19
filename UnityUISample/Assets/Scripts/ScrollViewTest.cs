using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalItem : MonoBehaviour
{

    public int m_index = 0;
    private bool m_bSelected = false;

    public static string[] animallist = { "토끼", "호랑이", "사자", "강아지", "고양이", "진영이", "북극이" };
    public Text m_txtanimal = null;
    public ScrollRect m_scrollrect = null;
    public Text m_resulttxt = null;
    public Button m_okbtn = null;
    public Button m_resetbtn = null;
    public GameObject m_prefabItem = null;

    private List<AnimalItem> m_listAnimal = new List<AnimalItem>();

    private int m_iSelectIndex = 0;

    public void initialize(int index, string name)
    {
        m_index = index;
        m_txtanimal.text = name;
    }

    AnimalItem CreateItem(int idx)
    {
        GameObject go = Instantiate(m_prefabItem, m_scrollrect.content) as GameObject;
        AnimalItem kAnimal = go.GetComponent<AnimalItem>();

        kAnimal.transform.localScale = new Vector3(1, 1, 1);
        kAnimal.initialize(idx, animallist[idx]);
        return kAnimal;
    }

    public void Start()
    {
        m_okbtn.onClick.AddListener(onClicked_okbtn);
        m_resetbtn.onClick.AddListener(onClicked_resetbtn);

        m_scrollrect.onValueChanged.AddListener((Vector2 value) =>
        {
            OnValueChanged_AnimalList(value);
        });
    }

    public void onClicked_okbtn()
    {

    }

    public void onClicked_resetbtn()
    {

    }
}
