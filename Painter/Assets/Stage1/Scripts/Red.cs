using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red : MonoBehaviour
{
    public GameObject colorObj;
    public Color color;

    // Update is called once per frame
    public void OnClickEvent()
    {
        color = Color.red;
        colorObj.GetComponent<Renderer>().material.color = color;

    }
}
