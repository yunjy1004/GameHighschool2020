using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_Q4 : MonoBehaviour
{
    public ToggleGroup tgall1;
    public Button btn1;

    // Start is called before the first frame update
    void Start()
    {
        btn1.onClick.AddListener(Click_Button);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Click_Button()
    {

    }
}
