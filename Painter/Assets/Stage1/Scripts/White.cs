using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class White : MonoBehaviour
{
    public GameObject colorObj;
    public Color color;

    // Update is called once per frame
    public void OnClickEvent()
    {
        color = Color.white;
        colorObj.GetComponent<Renderer>().material.color = color;

    }
}
