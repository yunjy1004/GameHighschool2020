using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 0;

    private void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");

        var rigidbody = GetComponent<Rigidbody2D>();

        rigidbody.transform.position += new Vector3(xAxis * Speed * Time.deltaTime, 0, 0);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.AddForce(400 * Vector2.up);
        }
    }
}
