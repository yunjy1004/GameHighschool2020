using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Green : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject colorObj;
    public Color color;

    // Update is called once per frame
    public void OnClickEvent()
    {
        color = Color.green;
        colorObj.GetComponent<Renderer>().material.color = color;

    }
}
