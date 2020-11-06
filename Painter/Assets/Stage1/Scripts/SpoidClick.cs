using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpoidClick : MonoBehaviour
{
    Vector3 mpos;
    public GameObject colorObj;
    public Color color;

    public Texture2D rendera;
    public Texture2D texture_2d;

    // Update is called once per frame
    void Update()
    {
       

        mpos = Input.mousePosition;
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);
            if (hit.collider.gameObject.tag == "color")
            {
                StartCoroutine(ScreenShotAndSpoid());
            }
        }
    }

    IEnumerator ScreenShotAndSpoid()
    {
        Texture2D tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        yield return new WaitForEndOfFrame();
        tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        tex.Apply();

        color = tex.GetPixel((int)mpos.x, (int)mpos.y);
        colorObj.GetComponent<Renderer>().material.color = color;

    }
    
}
